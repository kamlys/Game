using Game.Core.DTO;
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
        bool BuyOffer(MarketDto market, string User);
        bool AddOffer(MarketDto offer);
        void Add(MarketDto market);
        void Update(MarketDto market);
        void Delete(int id);

    }
}
