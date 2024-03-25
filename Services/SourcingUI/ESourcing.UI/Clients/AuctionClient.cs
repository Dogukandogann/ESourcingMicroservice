﻿using ESourcing.Core.Common;
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
            _client.BaseAddress = new Uri(CommonInfo.LocalAuctionBaseAddress);
        }
        public async Task<Result<AuctionVM>> CreateAuction(AuctionVM auctionVievModel)
        {
            var dataAsString = JsonConvert.SerializeObject(auctionVievModel);
            var content = new StringContent(dataAsString);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await _client.PostAsync("/api/v1/Auction", content);
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
        public async Task<Result<List<AuctionVM>>> GetAuctions(AuctionVM auctionVievModel)
        {
            var response = await _client.GetAsync("/api/v1/Auction");
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
    }
}
