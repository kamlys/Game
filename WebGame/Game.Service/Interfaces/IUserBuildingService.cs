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
        bool Build(int id, int col, int row, string user);
        void Destroy(string user, int ID);
        List<UserBuildingDto> GetUserBuilding();
        List<UserBuildingDto> GetUserBuildingList(string User);
        void Add(UserBuildingDto userBuilding);
        void Update(UserBuildingDto userBuilding);
        void Delete(int id);
        bool ifLvlUp(int id, string User);
        bool LvlUp(int id, string User);
    }
}
