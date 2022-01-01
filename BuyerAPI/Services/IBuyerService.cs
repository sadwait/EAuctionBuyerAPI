using AccountsAPI.Models;
using System.Threading.Tasks;

namespace AccountsAPI.Services
{
    public interface IBuyerService
    {
        Task PlaceBid(Buyer buyer);

        Task UpdateBid(string productId, string email, double amount);
    }
}
