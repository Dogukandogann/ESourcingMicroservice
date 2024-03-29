using System.Collections.Generic;

namespace ESourcing.UI.VievModel
{
    public class AuctionBidsVM
    {
        public string AuctionId { get; set; }
        public string ProductId { get; set; }
        public string SellerUserName { get; set; }
        public bool IsAdmin { get; set; }
        public List<BidVM> Bids { get; set; }
    }
}
