﻿using ESourcing.Core.Repositories;
using ESourcing.Core.ResultModels;
using ESourcing.UI.Clients;
using ESourcing.UI.VievModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESourcing.UI.Controllers
{
    public class AuctionController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ProductClient _productClient;
        private readonly AuctionClient _auctionClient;
        private readonly BidClient _bidClient;

        public AuctionController(IUserRepository userRepository, ProductClient productClient, AuctionClient auctionClient, BidClient bidClient)
        {
            _userRepository = userRepository;
            _productClient = productClient;
            _auctionClient = auctionClient;
            _bidClient = bidClient;
        }

        public async Task<IActionResult> Index()
        {
            var auictionList = await _auctionClient.GetAuctions();
            return View(auictionList.Data);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var productList = await _productClient.GetProducts();
            if (productList.IsSuccess)
                ViewBag.ProductList = productList.Data; 
            var userList = await _userRepository.GetAllAsync();
            ViewBag.UserList = userList;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(AuctionVM model)
        {
            model.Status = default(int);
            model.CreatedAt = DateTime.Now;
            var createAuction = await _auctionClient.CreateAuction(model);
            if (createAuction.IsSuccess)
                return RedirectToAction("Index");
            return View();
        }
        public async Task<IActionResult> Detail(string id)
        {
            AuctionBidsVM model = new AuctionBidsVM();

            var auctionResponse = await _auctionClient.GetAuctionById(id);
            var bidsReponse = await _bidClient.GetAllBidsByAuctionId(id);

            model.SellerUserName = HttpContext.User?.Identity.Name;
            model.AuctionId = auctionResponse.Data.Id;
            model.ProductId = auctionResponse.Data.ProductId;
            model.Bids = bidsReponse.Data;
            var isAdmin = HttpContext.Session.GetString("IsAdmin");
            model.IsAdmin = Convert.ToBoolean(isAdmin);
            return View(model);
        }
        [HttpPost]
        public async Task<Result<string>> SendBid(BidVM model)
        {
            model.CreatedAt = DateTime.Now;
            var sendBidResponse = await _bidClient.SendBid(model);
            return sendBidResponse;
        }
        [HttpPost]
        public async Task<Result<string>> CompleteBid(string id)
        {
            var sendBidResponse = await _auctionClient.CompleteBid(id);
            return sendBidResponse;
        }
    }
    
}
