using ESourcing.Core.Common;
using ESourcing.Core.ResultModels;
using ESourcing.UI.VievModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ESourcing.UI.Clients
{
    public class AuctionClient
    {
        public HttpClient _client { get;}
        public AuctionClient(HttpClient Client)
        {
            _client = Client;
            _client.BaseAddress = new Uri(CommonInfo.BaseAddress);
        }
        public async Task<Result<AuctionVM>> CreateAuction(AuctionVM auctionVievModel)
        {
            var dataAsString = JsonConvert.SerializeObject(auctionVievModel);
            var content = new StringContent(dataAsString);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await _client.PostAsync("/Auction", content);
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<AuctionVM>(responseData);
                if (result is not null)
                    return new Result<AuctionVM>(true, ResultConstant.RecordCreatedSuccessfully, result);
                else
                    return new Result<AuctionVM>(false, ResultConstant.RecordNotCreatSuccessfully);
            }
            return new Result<AuctionVM>(false, ResultConstant.RecordNotCreatSuccessfully);
        }
        public async Task<Result<List<AuctionVM>>> GetAuctions()
        {
            var response = await _client.GetAsync("Auction");
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<AuctionVM>>(responseData);
                if (result.Any())
                    return new Result<List<AuctionVM>>(true,ResultConstant.RecordFound,result.ToList());
                return new Result<List<AuctionVM>>(false, ResultConstant.RecordNotFound);
            }
            return new Result<List<AuctionVM>>(false, ResultConstant.RecordNotFound);
        }
        public async Task<Result<AuctionVM>> GetAuctionById(string id)
        {
            var response = await _client.GetAsync("/Auction"+id);
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<AuctionVM>(responseData);
                if (result is not null)
                    return new Result<AuctionVM>(true, ResultConstant.RecordFound, result);
                return new Result<AuctionVM>(false, ResultConstant.RecordNotFound);
            }
            return new Result<AuctionVM>(false, ResultConstant.RecordNotFound);
        }
        public async Task<Result<string>> CompleteBid(string id)
        {
            var dataAsString = JsonConvert.SerializeObject(id);
            var content = new StringContent(dataAsString);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await _client.PostAsync("/Auction/CompleteAuction", content);
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                return new Result<string>(true, ResultConstant.RecordCreatedSuccessfully, responseData);
            }
            return new Result<string>(false, ResultConstant.RecordNotCreatSuccessfully);
        }
    }
}
