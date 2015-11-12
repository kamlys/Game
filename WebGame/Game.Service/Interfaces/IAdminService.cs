using Game.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Service.Interfaces
{
    public interface IAdminService
    {
        bool ifAdmin(string User);
        void adminMethod(int id, string table, string user);
        List<UserDto> GetUsers();
        List<AdminDto> GetAdmin();
        List<BanDto> GetBans();
        List<BuildingDto> GetBuildings();
        List<BuildingQueueDto> GetQueues();
        List<DolarDto> GetDolars();
        List<MapDto> GetMaps();
        List<ProductDto> GetProducts();
        List<UserBuildingDto> GetUserBuildings();
        List<UserProductDto> GetUserProducts();


    }
}
