using AccountsAPI.Models;
using System.Threading.Tasks;

namespace AccountsAPI.Services
{
    public interface IBuyerService
    {
        Task<Buyer> GetProduct(string productId);
        Task AddProduct(Buyer product);
        Task DeleteProduct(string productId);
    }
}
