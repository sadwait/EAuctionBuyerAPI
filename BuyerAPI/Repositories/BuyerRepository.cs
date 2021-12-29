using AccountsAPI.Models;
using Microsoft.Azure.Cosmos;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace AccountsAPI.Repositories
{
    public class BuyerRepository : IBuyerRepository
    {
        private Container container;
        public BuyerRepository(CosmosClient client, string databaseName, string containerName)
        {
            container = client.GetContainer(databaseName, containerName);
        }
        public async Task AddProduct(Buyer product)
        {
            await container.CreateItemAsync(product, new PartitionKey(product.ProductId));
        }

        public async Task DeleteProduct(string productId)
        {
            await container.DeleteItemAsync<Buyer>(productId, new PartitionKey(productId));
        }

        public async Task<Buyer> GetProduct(string productId)
        {
            // var response=   await container.ReadItemAsync<Product>("863ab1c4-5385-499f-b78e-183c9874ea1f", new PartitionKey(productId));
            //  return response.Resource;

            IQueryable<Buyer> queryable = container.GetItemLinqQueryable<Buyer>(true);
            queryable = queryable.Where(item => item.ProductId == productId);
            return await Task.FromResult(queryable.ToArray().FirstOrDefault());
        }
    }
}
