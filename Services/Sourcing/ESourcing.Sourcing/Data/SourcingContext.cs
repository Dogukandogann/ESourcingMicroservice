using ESourcing.Sourcing.Entities;
using ESourcing.Sourcing.Settings;
using MongoDB.Driver;

namespace ESourcing.Sourcing.Data
{
    public class SourcingContext : ISourcingContext
    {
        public SourcingContext(ISourcingDbSettings _sourcingDbSettings)
        {
            var client = new MongoClient(_sourcingDbSettings.ConnectionString);
            var database = client.GetDatabase(_sourcingDbSettings.DatabaseName);

            Auctions = database.GetCollection<Auction>(nameof(Auction));
            Bids = database.GetCollection<Bid>(nameof(Bid));
            SourcingContextSeed.SeedData(Auctions);
        }
        public IMongoCollection<Auction> Auctions { get; }
        public IMongoCollection<Bid> Bids { get; }
    }
}
