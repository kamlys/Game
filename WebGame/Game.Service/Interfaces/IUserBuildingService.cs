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
        void Add(UserBuildingDto userBuilding);
        void Update(UserBuildingDto userBuilding, int id);
        void Delete(int id);
    }
}
