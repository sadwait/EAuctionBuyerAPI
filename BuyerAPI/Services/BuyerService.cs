using AccountsAPI.Models;
using AccountsAPI.Repositories;
using System.Threading.Tasks;

namespace AccountsAPI.Services
{
    public class BuyerService : IBuyerService
    {
        private readonly IBuyerRepository _repository;
        public BuyerService(IBuyerRepository productRepository)
        {
            _repository = productRepository;
        }
        public async Task AddProduct(Buyer product)
        {
            await _repository.AddProduct(product);
        }

        public async Task DeleteProduct(string productId)
        {
            await _repository.DeleteProduct(productId);
        }

        public Task<Buyer> GetProduct(string productId)
        {
            return _repository.GetProduct(productId);
        }
    }
}
