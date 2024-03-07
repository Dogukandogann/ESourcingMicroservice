using Amazon.Runtime.Internal.Util;
using ESourcing.Sourcing.Entities;
using ESourcing.Sourcing.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace ESourcing.Sourcing.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuctionController : ControllerBase
    {
        private readonly IAuctionRepository _auctionRepository;
        private readonly ILogger<AuctionController> _logger;

        public AuctionController(IAuctionRepository auctionRepository, ILogger<AuctionController> logger)
        {
            _auctionRepository = auctionRepository;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Auction>),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Auction>>> GetAuctions() 
        {
            var auctions = await _auctionRepository.GetAuctions();
            return Ok(auctions);
        }

        [HttpGet("{id:length(24)}",Name ="GetAuction")]
        [ProducesResponseType(typeof(Auction),(int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<Auction>> GetAuction(string id)
        {
            var auction = await _auctionRepository.GetAuction(id);
            if(auction is null)
            {
                _logger.LogError($"Auction with id:{id} hasn't found");
                return NotFound();
            }

            return Ok(auction);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Auction), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<Auction>> Create([FromBody] Auction auction)
        {
           await _auctionRepository.Create(auction);
            return CreatedAtRoute("GetAuction",new {id=auction.Id},auction);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Auction), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Auction>> Update([FromBody] Auction auction)
        {

            return Ok(await _auctionRepository.Update(auction));
        }

        [HttpPost("{id:length(24)}")]
        [ProducesResponseType(typeof(Auction), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Auction>> Delete(string id)
        {

            return Ok(await _auctionRepository.Delete(id));
        }
    }
}
