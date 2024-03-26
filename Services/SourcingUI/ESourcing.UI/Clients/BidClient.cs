using ESourcing.Core.Common;
using ESourcing.Core.ResultModels;
using ESourcing.UI.VievModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ESourcing.UI.Clients
{
    public class BidClient
    {
        public HttpClient _client { get;}
        public BidClient(HttpClient client)
        {
            _client = client;
            client.BaseAddress = new Uri(CommonInfo.LocalAuctionBaseAddress);
        }
        public async Task<Result<List<BidVM>>> GetAllBidsByAuctionId(string id)
        {
            var response = await _client.GetAsync("/api/v1/Bid/GetBidByAuctionId?id=" + id);
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<BidVM>>(responseData);
                if (result.Any())
                    return new Result<List<BidVM>>(true, ResultConstant.RecordFound, result.ToList());
                return new Result<List<BidVM>>(false, ResultConstant.RecordNotFound);
            }
            return new Result<List<BidVM>>(false, ResultConstant.RecordNotFound);
        }
    }
}
