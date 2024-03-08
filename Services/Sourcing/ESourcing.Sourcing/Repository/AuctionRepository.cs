using ESourcing.Sourcing.Data;
using ESourcing.Sourcing.Entities;
using ESourcing.Sourcing.Repository.Interfaces;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ESourcing.Sourcing.Repository
{
    public class AuctionRepository : IAuctionRepository
    {
        private readonly ISourcingContext _sourcingContext;

        public AuctionRepository(ISourcingContext sourcingContext)
        {
            _sourcingContext = sourcingContext;
        }

        public async Task Create(Auction auction)
        {
           await _sourcingContext.Auctions.InsertOneAsync(auction);
        }

        public async Task<bool> Delete(string id)
        {
            FilterDefinition<Auction> filter = Builders<Auction>.Filter.Eq(x => x.Id,id);
            DeleteResult delete = await _sourcingContext.Auctions.DeleteOneAsync(filter);
            return delete.IsAcknowledged && delete.DeletedCount> 0;
        }

        public async Task<Auction> GetAuction(string id)
        {
            return await _sourcingContext.Auctions.Find(x => x.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public async Task<Auction> GetAuctionByName(string name)
        {
            FilterDefinition<Auction> filter = Builders<Auction>.Filter.Eq(x => x.Name, name);
            return await _sourcingContext.Auctions.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Auction>> GetAuctions()
        {
            return await _sourcingContext.Auctions.Find(x => true).ToListAsync();
        }

        public async Task<bool> Update(Auction auction)
        {
            var updateResult = await _sourcingContext.Auctions.ReplaceOneAsync(x=>x.Id.Equals(auction.Id),auction);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount> 0;
        }
    }
}
