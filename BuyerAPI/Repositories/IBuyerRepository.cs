using AccountsAPI.Models;
using System.Threading.Tasks;

namespace AccountsAPI.Repositories
{
    public interface IBuyerRepository
    {
        Task<Buyer> GetProduct(string productId);
        Task AddProduct(Buyer product);
        Task DeleteProduct(string productId);
    }
}
