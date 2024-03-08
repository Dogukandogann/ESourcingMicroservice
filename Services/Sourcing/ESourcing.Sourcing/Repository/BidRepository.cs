using ESourcing.Sourcing.Data;
using ESourcing.Sourcing.Entities;
using ESourcing.Sourcing.Repository.Interfaces;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESourcing.Sourcing.Repository
{
    public class BidRepository : IBidRepository
    {
        private readonly ISourcingContext _context;

        public BidRepository(ISourcingContext context)
        {
            _context = context;
        }

        public async Task<List<Bid>> GetBidsByAuctionId(string id)
        {
            FilterDefinition<Bid> filterDefinition = Builders<Bid>.Filter.Eq(x=>x.AuctionId,id);
            List<Bid> bids = await _context.Bids.Find(filterDefinition).ToListAsync();
            bids = bids.OrderByDescending(a => a.CreatedAt).GroupBy(a => a.SellerUserName).Select(a=>new Bid
            {
                AuctionId=a.FirstOrDefault().AuctionId,
                Price =a.FirstOrDefault().Price,
                CreatedAt =a.FirstOrDefault().CreatedAt,
                SellerUserName=a.FirstOrDefault().SellerUserName,
                ProductId=a.FirstOrDefault().ProductId,
                Id=a.FirstOrDefault().Id
            }).ToList();

            return bids;
        }

        public async Task<Bid> GetWinnerBid(string id)
        {
            List<Bid> bids = await GetBidsByAuctionId(id);

            return bids.OrderByDescending(x => x.Price).FirstOrDefault();
        }

        public async Task SendBid(Bid bid)
        {
           await _context.Bids.InsertOneAsync(bid);
        }
    }
}
