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
        private IRepository<UserProducts> _userProducts;
        private IRepository<Maps> _maps;
        private IRepository<Dolars> _dolars;
        private IRepository<Users> _users;
        //private IProductService _productService;
        private IUnitOfWork _unitOfWork;

        public BuildingHelper(
            IRepository<Buildings> buildings,
            IRepository<UserBuildings> userBuildings,
            IRepository<UserProducts> userProducts,
            IRepository<Maps> maps,
        IRepository<Dolars> dolars,
        IRepository<Users> users,
        //IProductService productService,
        IUnitOfWork unitOfWork)
        {
            _buildings = buildings;
            _userBuildings = userBuildings;
            _userProducts = userProducts;
            _maps = maps;
            _dolars = dolars;
            _users = users;
            //_productService = productService;
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

        public int[][] AddProductValue(string User)
        {
            //_productService.UpdateUserProduct(User);

            List<int[]> AddProduct = new List<int[]>();

            int uID = _users.GetAll().First(u => u.Login == User).ID;

            Dictionary<int, int> tab2 = new Dictionary<int, int>();


            foreach (var item in _userBuildings.GetAll().Where(u=> u.User_ID == uID))
            {
                int BuildLvl = item.Lvl;
                int Product_per_sec = _buildings.GetAll().First(b => b.Product_ID == item.Buildings.Product_ID).Product_per_sec;
                int Percent_per_lvl = _buildings.GetAll().First(b => b.Product_ID == item.Buildings.Product_ID).Percent_product_per_lvl / 100;

                if (tab2.Keys.Contains(item.Buildings.Product_ID))
                {
                    tab2[item.Buildings.Product_ID] += Product_per_sec * Percent_per_lvl * BuildLvl;
                }
                else
                {
                    tab2[item.Buildings.Product_ID] = Product_per_sec * Percent_per_lvl * BuildLvl;
                }
            }

            foreach (var item in _userProducts.GetAll().Where(p => (!tab2.Keys.Contains(p.Products.ID) && p.User_ID == uID)))
            {
                AddProduct.Add(new int[] { item.Product_ID, 0, item.Value});
            }

            foreach (var item in tab2)
            {
                var productValue = _userProducts.GetAll().First(u => u.Products.ID == item.Key && u.User_ID == uID).Value;
                AddProduct.Add(new int[] { item.Key, item.Value, productValue});
            }

            return AddProduct.ToArray<int[]>();
        }
    }
}
