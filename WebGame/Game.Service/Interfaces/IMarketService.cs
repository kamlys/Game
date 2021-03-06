﻿using Game.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Service.Interfaces
{
    public interface IMarketService
    {
        List<MarketDto> GetOffer();
        List<MarketDto> GetSellOffer();
        List<MarketDto> GetBuyOffer();
        bool BuyOffer(MarketDto market, string User);
        bool AddOffer(MarketDto offer);
        bool Add(MarketDto market);
        bool Update(MarketDto market);
        bool Delete(int id);
        bool SellProduct(int productID, int value, string user);


    }
}
