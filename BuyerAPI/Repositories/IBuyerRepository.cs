using AccountsAPI.Models;
using BuyerAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountsAPI.Repositories
{
    public interface IBuyerRepository
    {
        Task<List<Buyer>> GetAllBuyers();

        Task PlaceBid(Buyer buyer);

        Task UpdateBid(Buyer buyer);

        Task<ProductInfo> GetProductById(string productId);
    }
}
