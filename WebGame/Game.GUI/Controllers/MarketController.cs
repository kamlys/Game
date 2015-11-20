﻿using Game.Core.DTO;
using Game.GUI.ViewModels.Market;
using Game.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Game.GUI.Controllers
{
    public class MarketController : Controller
    {
        private IMarketService _marketService;

        public MarketController(IMarketService marketService)
        {
            _marketService = marketService;
        }

        // GET: Market
        public ActionResult Index()
        {
            return View(GetOfferList());
        }


        public ListMarketViewModel GetOfferList()
        {
            ListMarketViewModel marketList = new ListMarketViewModel();
            marketList.marketList = new List<MarketViewModel>();

            foreach (var item in _marketService.GetOffer())
            {
                marketList.marketList.Add(new MarketViewModel
                {
                    Login = item.Login,
                    Product_Name = item.Product_Name,
                    Number = item.Number,
                    Price = item.Price
                });
            }
            return marketList;
        }




        [HttpPost]
        public ActionResult AddOffer(ListMarketViewModel marketList)
        {
            MarketDto _marketDto = new MarketDto();

            _marketDto.Login = User.Identity.Name;
            _marketDto.Product_Name = marketList.marketView.Product_Name;
            _marketDto.Number = marketList.marketView.Number;
            _marketDto.Price = marketList.marketView.Price;

            _marketService.AddOffer(_marketDto);

            return View("~/Views/Market/Index.cshtml", GetOfferList());
        }


        public ActionResult BuyOffer(ListMarketViewModel marketList)
        {
            int a = marketList.marketView.Number;
            return View("~/Views/Market/Index.cshtml", GetOfferList());
        }

    }
}