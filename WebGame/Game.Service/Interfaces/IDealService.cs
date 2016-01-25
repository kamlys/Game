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
        void AddDealAdmin(DealDto dealDto);
        void DeleteDealAdmin(int id);
        void UpdateDealAdmin(DealDto dealDto);
        int[] AddDeal(DealDto dealDto);
        bool AcceptDeal(int ID, string user);
        void CancelDeal(int ID);
        void RerunDeal(int ID, string user);
        List<DealBuildingDto> GetDealBuildings();
        void AddDealBuildingAdmin(DealBuildingDto dealDto);
        void DeleteDealBuildingAdmin(int id);
        void UpdateDealBuildingAdmin(DealBuildingDto dealDto);

    }
}
