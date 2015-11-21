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
        void AddOffer(MarketDto offer);
        void Add(MarketDto admin);
        void Update(MarketDto admin, int id);
        void Delete(int id);

    }
}
