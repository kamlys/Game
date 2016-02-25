using Game.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Service.Interfaces
{
    public interface IDealService
    {
        List<DealDto> GetDeals();
        List<DealDto> GetUserDeals(string User);
        bool AddDealAdmin(DealDto dealDto);
        bool DeleteDealAdmin(int id);
        bool UpdateDealAdmin(DealDto dealDto);
        int[] AddDeal(DealDto dealDto);
        bool AcceptDeal(int ID, string user);
        void CancelDeal(int ID);
        void CancelRerun(int ID);
        void RerunDeal(int ID, string user);
        List<DealBuildingDto> GetDealBuildings();
        bool AddDealBuildingAdmin(DealBuildingDto dealDto);
        bool DeleteDealBuildingAdmin(int id);
        bool UpdateDealBuildingAdmin(DealBuildingDto dealDto);

    }
}
