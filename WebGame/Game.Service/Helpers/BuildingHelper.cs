﻿using Game.Core.DTO;
using Game.Dal;
using Game.Dal.Model;
using Game.Dal.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Service.Interfaces;
using System.Dynamic;

namespace Game.Service
{
    public class BuildingHelper : IBuildingHelper
    {
        private IRepository<Buildings> _buildings;
        private IRepository<UserBuildings> _userBuildings;
        private IRepository<UserProducts> _userProducts;
        private IRepository<BuildingQueue> _buildingQueue;
        private IRepository<Maps> _maps;
        private IRepository<Dolars> _dolars;
        private IRepository<Users> _users;
        //private IProductService _productService;
        private IUnitOfWork _unitOfWork;

        public BuildingHelper(
            IRepository<Buildings> buildings,
            IRepository<UserBuildings> userBuildings,
            IRepository<UserProducts> userProducts,
            IRepository<BuildingQueue> buildingQueue,
            IRepository<Maps> maps,
            IRepository<Dolars> dolars,
            IRepository<Users> users,
            //IProductService productService,
            IUnitOfWork unitOfWork)
        {
            _buildings = buildings;
            _userBuildings = userBuildings;
            _userProducts = userProducts;
            _buildingQueue = buildingQueue;
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
                    Alias = item.Alias,
                    BuildingTime = item.BuildingTime,
                    DestructionTime = item.DestructionTime
                });
            }
            return buildingsDto;
        }


        public List<UserBuildingDto> GetBuildings(string User)
        {
            var buildings = _userBuildings.GetAll().Where(a => a.Users.Login == User);
            int uID = _users.GetAll().First(i => i.Login == User).ID;
            int dolars = _dolars.GetAll().First(i => i.User_ID == uID).Value;
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
                        Y_pos = a.Y_pos,
                        Status = a.Status,
                        Building_Name = a.Buildings.Name,
                        Owner = a.Owner,
                        Color = a.Color,
                        DestPrice = (int)_buildings.Get(a.Building_ID).Dest_price,
                        CanDelete = (((int)dolars - (int)_buildings.Get(a.Building_ID).Dest_price) > 0 && a.DealID==null) ? true : false,
                    }
                );
            }
            return list;
        }


        public bool BuildingValidation(int id, int col, int row, string user, int dealid)
        {
            int uID = _users.GetAll().First(u => u.Login == user).ID;
            int buildingHeight = _buildings.GetAll().First(b => b.ID == id).Height;
            int buildingWidth = _buildings.GetAll().First(b => b.ID == id).Width;
            int mapHeight = _maps.GetAll().First(u => u.User_ID == uID).Height;
            int mapWidth = _maps.GetAll().First(u => u.User_ID == uID).Width;
            int userDolars = _dolars.GetAll().First(u => u.User_ID == uID).Value;
            int buildPrice = _buildings.GetAll().First(b => b.ID == id).Price;

            foreach (var item in _userBuildings.GetAll().Where(u => u.User_ID == uID))
            {
                if(!item.Owner)
                {
                    continue;
                }
                // wychodzi poza mape
                if (((row + buildingWidth) > mapWidth) || ((col + buildingHeight) > mapHeight))
                {
                    return false;
                }
                if (dealid == 0)
                {
                    // za mało funduszy
                    if (userDolars < buildPrice)
                    {
                        return false;
                    }
                }
                // zajęte miejsce
                var bWidth = item.Buildings.Width;
                var bHeight = item.Buildings.Height;
                
                // lewy górny róg stawianego leży wewnątrz istniejącego
                if (item.X_pos <= col && item.X_pos + bWidth > col && item.Y_pos <= row && item.Y_pos + bHeight > row)
                {
                    return false;
                }
                // lewy górny róg istniejącego leży wewnątrz stawianego
                if(col <= item.X_pos && col + buildingWidth > item.X_pos && row <= item.Y_pos && item.Y_pos + buildingHeight > row)
                {
                    return false;
                }
            }
            return true;
        }
    

        public string[][] ProductNames(string User)
        {
            List<string[]> names = new List<string[]>();
            foreach(var a in _userProducts.GetAll().Where(a => a.Users.Login == User))
            {
                names.Add(new string[]{ a.Product_ID.ToString(), a.Products.Alias});
            }

            return names.ToArray();

        }

        public int[][] AddProductValue(string User)
        {
            List<int[]> AddProduct = new List<int[]>();

            int uID = _users.GetAll().First(u => u.Login == User).ID;

            Dictionary<int, int> tab2 = new Dictionary<int, int>();

            foreach (var item in _userProducts.GetAll().Where(p => p.User_ID == uID/* && !tab2.Keys.Contains(p.Products.ID))*/))
            {
                AddProduct.Add(new int[] { item.Product_ID, 0, item.Value });
            }

            return AddProduct.ToArray<int[]>();
        }

        public void ChangeStatus(string User)
        {
            int uID = _users.GetAll().First(u => u.Login == User).ID;

            foreach (var item in _userBuildings.GetAll().Where(u => u.User_ID == uID))
            {
                if (item.Status.Contains("budowa") && item.BuildingQueue.All(i => i.UserBuilding_ID == item.ID && DateTime.Now.CompareTo(i.FinishTime)>0))
                {
                    item.Status = item.BuildingQueue.First(i => i.UserBuilding_ID == item.ID).NewStatus;
                    _buildingQueue.Delete(i => i.UserBuilding_ID == item.ID);
                    _unitOfWork.Commit();
                }
                else if(item.Status.Contains("burzenie") && item.BuildingQueue.All(i => i.UserBuilding_ID == item.ID && DateTime.Now.CompareTo(i.FinishTime) > 0))
                {
                    _buildingQueue.Delete(i => i.UserBuilding_ID == item.ID);
                    _userBuildings.Delete(u => u.User_ID == uID && u.ID == item.ID);
                    _unitOfWork.Commit();
                }
                else if(item.Status.Contains("burzenie") && item.BuildingQueue.All(i => i.UserBuilding_ID == item.ID && DateTime.Now.CompareTo(i.FinishTime) > 0))
                {
                    item.Status = item.BuildingQueue.First(i => i.UserBuilding_ID == item.ID).NewStatus;
                    _buildingQueue.Delete(i => i.UserBuilding_ID == item.ID);
                    _unitOfWork.Commit();
                }
            }
        }

        public int BuildingTimeLeft(string User, int building_id)
        {
            var ub = _userBuildings.GetAll().Where(a => a.ID == building_id).First();
            if(ub.Status == "gotowy")
            {
                return 0;
            }
            var b = ub.Buildings;

            var fTime = _buildingQueue.GetAll().Where(a => a.UserBuilding_ID == ub.ID).First().FinishTime;
            return (int)(fTime - DateTime.Now).GetValueOrDefault().TotalSeconds;

        }
    }
}
