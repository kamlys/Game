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

namespace Game.Service
{
    public class BuildingHelper : IBuildingHelper
    {
        private IRepository<Buildings> _buildings;
        private IRepository<UserBuildings> _userBuildings;
        private IRepository<Maps> _maps;
        private IRepository<Dolars> _dolars;
        private IRepository<Users> _users;

        private IUnitOfWork _unitOfWork;

        public BuildingHelper(
            IRepository<Buildings> buildings,
            IRepository<UserBuildings> userBuildings,
            IRepository<Maps> maps,
        IRepository<Dolars> dolars,
        IRepository<Users> users,
        IUnitOfWork unitOfWork)
        {
            _buildings = buildings;
            _userBuildings = userBuildings;
            _maps = maps;
            _dolars = dolars;
            _users = users;
            _unitOfWork = unitOfWork;
        }

        public List<BuildingDto> GetBuildings()
        {
            List<BuildingDto> buildingsDto = new List<BuildingDto>();
            foreach (var item in _buildings.GetAll())
            {
                buildingsDto.Add(new BuildingDto
                {
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


        public List<UserBuildingDto> GetBuildings(string User)
        {
            var buildings = _userBuildings.GetAll().Where(a => a.Users.Login == User);
            List<UserBuildingDto> list = new List<UserBuildingDto>();
            foreach (var a in buildings)
            {
                list.Add(
                    new UserBuildingDto
                    {
                        Building_ID = a.Building_ID,
                        ID = a.ID,
                        Lvl = a.Lvl,
                        User_ID = a.User_ID,
                        X_pos = a.X_pos,
                        Y_pos = a.Y_pos
                    }
                );
            }
            return list;
        }


        public bool BuildingValidation(int id, int col, int row, string user)
        {
            int uID = _users.GetAll().First(u => u.Login == user).ID;
            int buildingHeight = _buildings.GetAll().First(b => b.ID == id).Height;
            int buildingWidth = _buildings.GetAll().First(b => b.ID == id).Width;
            int mapHeight = _maps.GetAll().First(u => u.User_ID == uID).Height;
            int mapWidth = _maps.GetAll().First(u => u.User_ID == uID).Width;
            int userDolars = _dolars.GetAll().First(u => u.User_ID == uID).Value;
            int buildPrice = _buildings.GetAll().First(b => b.ID == id).Price;
            bool freePlace = true;

            foreach (var item in _userBuildings.GetAll().Where(u => u.User_ID == uID))
            {
                if (item.X_pos == row && item.Y_pos == col)
                {
                    freePlace = false;
                }
                else if (freePlace && ((row + buildingWidth) <= mapWidth) && ((col + buildingHeight) <= mapHeight) && (userDolars >= buildPrice))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            if (freePlace && ((row + buildingWidth) <= mapWidth) && ((col + buildingHeight) <= mapHeight) && (userDolars >= buildPrice))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Dictionary<int, int> AddProductValue(int uID, int pID)
        {
            Dictionary<int, int> AddProduct = new Dictionary<int, int>();

            foreach (var item in _buildings.GetAll())
            {
                int buildingCount = _userBuildings.GetAll().Where(u => u.User_ID == uID).Count();

                int Product_per_lvl = _buildings.GetAll().First(p => p.Product_ID == pID).Product_per_sec;
                int Percent_per_lvl = _buildings.GetAll().First(p => p.Product_ID == pID).Percent_product_per_lvl;
                int BuildLvl = _userBuildings.GetAll().First(b => b.Buildings.Product_ID == pID).Lvl;

                AddProduct.Add(item.Product_ID, (int)((Product_per_lvl * (Percent_per_lvl * 0.01) * BuildLvl)*buildingCount));
            }
            return AddProduct;
        }
    }
}
