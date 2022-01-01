using AccountsAPI.Models;
using AccountsAPI.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AccountsAPI.Services
{
    public class BuyerService : IBuyerService
    {
        private readonly IBuyerRepository _repository;
        public BuyerService(IBuyerRepository buyerRepository)
        {
            _repository = buyerRepository;
        }

        public async Task PlaceBid(Buyer buyer)
        {
            buyer.Id = Guid.NewGuid().ToString();
            await _repository.PlaceBid(buyer);
        }

        public async Task UpdateBid(string productId, string email, double amount)
        {
            Buyer buyer = new Buyer();
            var buyersList = await _repository.GetAllBuyers();
            if (buyersList != null && buyersList.Count > 0)
            {
                buyer = buyersList.Where(x => x.ProductId == productId && x.Email == email).FirstOrDefault();
                if (buyer != null)
                {
                    buyer.BidAmount = amount;
                }
            }

            await _repository.UpdateBid(buyer);
        }
    }
}