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

        [HttpGet]        
        public async Task<IActionResult> Get()
        {
            await _buyerService.Publish();
            return Ok();
           
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
        public async Task Put(string productId,string buyerEmailld, double newBidAmount)
        {
            await _buyerService.UpdateBid(productId, buyerEmailld, newBidAmount);
        }
    }
}
