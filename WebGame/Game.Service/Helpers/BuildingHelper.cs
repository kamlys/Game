using Game.Core.DTO;
using Game.Dal;
using Game.Dal.Model;
using Game.Dal.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Service.Interfaces;
using Game.Core.DTO.Helpers;

namespace Game.Service
{
    public class BuildingHelper : IBuildingHelper
    {
        private IRepository<Buildings> _buildings;
        private IRepository<UserBuildings> _userBuildings;

        private IUnitOfWork _unitOfWork;

        public BuildingHelper(
            IRepository<Buildings> buildings,
            IRepository<UserBuildings> userBuildings, 
            IUnitOfWork unitOfWork)
        {
            _buildings = buildings;
            _userBuildings = userBuildings;
            _unitOfWork = unitOfWork;
        }

        public List<BuildingDto> GetBuildings()
        {
            List<BuildingDto> buildingsDto = new List<BuildingDto>();
            foreach (var item in _buildings.GetAll())
            {
                buildingsDto.Add(new BuildingDto { 
                    Name = item.Name, 
                    Price = item.Price, 
                    ID = item.ID, 
                    Height = item.Height, 
                    Width = item.Width, 
                    Dest_price = item.Dest_price, 
                    Percent_price_per_lvl = item.Percent_price_per_lvl, 
                    Percent_product_per_lvl = item.Percent_product_per_lvl,
                    Product_ID = item.Product_ID, 
                    Product_per_sec = item.Product_per_sec,
                    Alias = item.Alias
                });
            }
            return buildingsDto;
        }


        public List<UserBuildingDtoHelper> GetUserBuildings(string User)
        {
            var buildings = _userBuildings.GetAll().Where(a => a.Users.Login == User);
            List<UserBuildingDtoHelper> list = new List<UserBuildingDtoHelper>();
            foreach (var a in buildings)
            {
                list.Add(
                    new UserBuildingDtoHelper
                    {
                        BuildingName = a.Buildings.Alias,
                        Lvl = a.Lvl,
                        Building_ID = a.Building_ID,
                        X_pos = a.X_pos,
                        Y_pos = a.Y_pos
                    }
                );
            }
            return list;
        }
    }
}
