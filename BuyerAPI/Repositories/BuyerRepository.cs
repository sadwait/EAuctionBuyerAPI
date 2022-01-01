using AccountsAPI.Models;
using Microsoft.Azure.Cosmos;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Azure.Documents.Client;

namespace AccountsAPI.Repositories
{
    public class BuyerRepository : IBuyerRepository
    {
        private Container container;
        public BuyerRepository(CosmosClient client, string databaseName, string containerName)
        {
            container = client.GetContainer(databaseName, containerName);
        }

        public async Task<List<Buyer>> GetAllBuyers()
        {
            IQueryable<Buyer> queryable = container.GetItemLinqQueryable<Buyer>(true);
            return await Task.FromResult(queryable.ToList());
        }

        public async Task PlaceBid(Buyer buyer)
        {
            await container.CreateItemAsync(buyer, new PartitionKey(buyer.Id));
        }

        public async Task UpdateBid(Buyer buyer)
        {
            await container.UpsertItemAsync(buyer, new PartitionKey(buyer.Id));
        }
    }
}
