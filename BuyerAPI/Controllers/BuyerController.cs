using AccountsAPI.Models;
using AccountsAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AccountsAPI.Controllers
{
    [Route("e-auction/api/v1/[controller]")]
    [ApiController]
    public class BuyerController : ControllerBase
    {
        private readonly IBuyerService _buyerService;
        public BuyerController(IBuyerService buyerService)
        {
            _buyerService = buyerService;
        }

        [HttpPost]
        [Route("place-bid")]
        public async Task<IActionResult> Post([FromBody] Buyer buyer)
        {
            await _buyerService.PlaceBid(buyer);
            return Created("place-bid", buyer);
        }
        
        [HttpPut]
        [Route("{productId}/{buyerEmailld}/{newBidAmount}")]
        public void Put(string productId,string buyerEmailld, double newBidAmount)
        {
            _buyerService.UpdateBid(productId, buyerEmailld, newBidAmount);
        }
    }
}
