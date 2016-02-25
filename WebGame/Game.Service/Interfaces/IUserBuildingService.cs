using Game.Core.DTO;
using Game.Dal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Service.Interfaces
{
    public interface IUserBuildingService
    {
        bool Build(int id, int col, int row, int dealID, string user);
        void Destroy(string user, int ID);
        List<UserBuildingDto> GetUserBuilding();
        List<UserBuildingDto> GetUserBuildingList(string User);
        bool Add(UserBuildingDto userBuilding);
        bool Update(UserBuildingDto userBuilding);
        bool Delete(int id);
        bool ifLvlUp(int id, string User);
        bool LvlUp(int id, string User);
        List<DealBuildingDto> GetDealBuildingList(string user);
        void ChangeColor(string color, int id, string user);
    }
}
