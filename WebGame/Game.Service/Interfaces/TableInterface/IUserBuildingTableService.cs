using Game.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Service.Interfaces.TableInterface
{
    public interface IUserBuildingTableService
    {
        List<UserBuildingDto> GetUserBuilding();
        void Add(UserBuildingDto userBuilding);
        void Update(UserBuildingDto userBuilding, int id);
        void Delete(int id);
    }
}
