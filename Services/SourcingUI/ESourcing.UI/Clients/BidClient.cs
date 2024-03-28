using ESourcing.Core.Common;
using ESourcing.Core.ResultModels;
using ESourcing.UI.VievModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
        public async Task<Result<string>> SendBid(BidVM model)
        {
            var dataAsString = JsonConvert.SerializeObject(model);
            var content = new StringContent(dataAsString);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await _client.PostAsync("/Bid", content);
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                return new Result<string>(true, ResultConstant.RecordCreatedSuccessfully, responseData);
            }
            return new Result<string>(false, ResultConstant.RecordNotCreatSuccessfully);
        }
    }
}
