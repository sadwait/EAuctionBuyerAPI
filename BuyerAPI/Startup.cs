using AccountsAPI.Repositories;
using AccountsAPI.Services;
using BuyerAPI.MessageBroker;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AccountsAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BuyerAPI", Version = "v1" });
            });

            services.AddScoped<IRabbitMqProducer, RabbitMqProducer>();

            services.AddSingleton(service => {
                var _config = Configuration.GetSection("RabbitMQ");
                return new ConnectionFactory()
                {
                    HostName = _config["HostName"],
                    UserName = _config["UserName"],
                    Password = _config["Password"],
                    Port = Convert.ToInt32(_config["Port"]),
                    VirtualHost = _config["VirtualHost"],
                };
            });

            services.AddScoped<IBuyerService, BuyerService>();
            services.AddSingleton<IBuyerRepository>(InitializeCosmosClientIntance(Configuration.GetSection("CosmosDb")).GetAwaiter().GetResult());
            services.AddCors(c => { c.AddPolicy("AllowOrigin", option => option.AllowAnyOrigin()); }) ;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();                
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BuyerAPI v1"));

            app.UseRouting();

            app.UseAuthorization();
            app.UseCors(cors=>cors.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static async Task<IBuyerRepository> InitializeCosmosClientIntance(IConfigurationSection configurationSection)
        {
            var account = configurationSection["Account"];
            var key = configurationSection["Key"];
            var databaseName = configurationSection["DatabaseName"];
            var containerName = configurationSection["ContainerName"];

            CosmosClientOptions cosmosClientOptions = new CosmosClientOptions()
            {
                HttpClientFactory = () =>
                {
                    HttpMessageHandler httpMessageHandler = new HttpClientHandler()
                    {
                        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                    };

                    return new HttpClient(httpMessageHandler);
                },
                ConnectionMode = ConnectionMode.Gateway
            };

            var cosmosClient = new CosmosClient(account, key, cosmosClientOptions);
            var db = await cosmosClient.CreateDatabaseIfNotExistsAsync(databaseName);
            await db.Database.CreateContainerIfNotExistsAsync(containerName, "/id");
            var buyerRepository = new BuyerRepository(cosmosClient, databaseName, containerName);
            return buyerRepository;
        }
    }
}
