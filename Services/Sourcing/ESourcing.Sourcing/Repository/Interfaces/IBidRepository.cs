using ESourcing.Sourcing.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESourcing.Sourcing.Repository.Interfaces
{
    public interface IBidRepository
    {
        Task SendBid(Bid bid);
        Task<List<Bid>> GetBidsByAuctionId(string id);
        Task<Bid> GetWinnerBid(string id);
    }
}
