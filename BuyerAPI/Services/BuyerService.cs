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
            var productinfo = await _repository.GetProductById(buyer.ProductId);
            if(productinfo.BidEndDate <DateTime.Now)
            {
                throw new ArgumentException("Bid cannot be placed after the bid end date");
            }
            var buyersList = await _repository.GetAllBuyers();
            if (buyersList != null && buyersList.Count > 0)
            {
                bool isBidExists = buyersList.Any(x => x.ProductId == buyer.ProductId && x.Email == buyer.Email);
                if (isBidExists)
                {
                    throw new ArgumentException("More than one bid on a product by same user is not allowed");
                }
            }

            buyer.Id = Guid.NewGuid().ToString();
            await _repository.PlaceBid(buyer);
        }

        public async Task UpdateBid(string productId, string email, double amount)
        {
            var productinfo = await _repository.GetProductById(productId);
            if (productinfo.BidEndDate < DateTime.Now)
            {
                throw new ArgumentException("Bid Amount cannot be updated after the bid end date");
            }

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