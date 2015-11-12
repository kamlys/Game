using Game.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Core.DTO;
using Game.Dal.Repository;
using Game.Dal.Model;
using Game.Dal;

namespace Game.Service
{
    public class AdminService : IAdminService
    {
        private IRepository<Users> _users;
        private IRepository<Admins> _admins;
        private IRepository<Bans> _bans;
        private IRepository<BuildingQueue> _buildingQueue;
        private IRepository<Buildings> _buildings;
        private IRepository<Dolars> _dolars;
        private IRepository<Maps> _maps;
        private IRepository<Products> _products;
        private IRepository<UserBuildings> _userBuildings;
        private IRepository<UserProducts> _userProducts;

        private IUnitOfWork _unitOfWork;
        private IHashPass _hassPass;

        public AdminService(
            IRepository<Users> users,
            IRepository<Admins> admins,
            IRepository<Bans> bans,
            IRepository<BuildingQueue> buildingQueue,
            IRepository<Buildings> buildings,
            IRepository<Dolars> dolars,
            IRepository<Maps> maps,
            IRepository<Products> products,
            IRepository<UserBuildings> userBuildings,
            IRepository<UserProducts> userProducts,
        IHashPass hassPass,
            IUnitOfWork unitOfWork)
        {
            _users = users;
            _admins = admins;
            _bans = bans;
            _buildingQueue = buildingQueue;
            _buildings = buildings;
            _dolars = dolars;
            _maps = maps;
            _products = products;
            _userBuildings = userBuildings;
            _userProducts = userProducts;
            _hassPass = hassPass;
            _unitOfWork = unitOfWork;
        }


        public void adminMethod(int id, string table, string user)
        {
            int uID = _users.GetAll().First(u => u.Login == user).ID;
            switch (table)
            {
                case "admin":
                    _admins.Add(new Admins
                    {
                        User_ID =63
                    });
                    _unitOfWork.Commit();
                break;
                default:
                    break;
            }
        }

        public bool ifAdmin(string User)
        {
            int uID = _users.GetAll().First(u => u.Login == User).ID;

            foreach (var item in _admins.GetAll())
            {
                if (item.User_ID == uID)
                {
                    return true;
                }
            }
            return false;
        }


        public List<UserDto> GetUsers()
        {
            List<UserDto> userDto = new List<UserDto>();
            foreach (var item in _users.GetAll())
            {
                try
                {
                    userDto.Add(new UserDto
                    {
                        ID = item.ID,
                        Login = item.Login,
                        Password = item.Password,
                        Email = item.Email,
                        Last_Log = (DateTime)item.Last_Log,
                        Last_Update = (DateTime)item.Last_Update,
                        Registration_Date = (DateTime)item.Registration_Date

                    });
                }
                catch (Exception)
                {
                }

            }
            return userDto;
        }

        public List<AdminDto> GetAdmin()
        {
            List<AdminDto> adminDto = new List<AdminDto>();
            foreach (var item in _admins.GetAll())
            {
                try
                {
                    adminDto.Add(new AdminDto
                    {
                        ID = item.ID,
                        User_ID = item.User_ID
                    });
                }
                catch (Exception)
                {
                }
            }
            return adminDto;
        }

        public List<BanDto> GetBans()
        {
            List<BanDto> banDto = new List<BanDto>();
            foreach (var item in _bans.GetAll())
            {
                try
                {
                    banDto.Add(new BanDto
                    {
                        ID = item.ID,
                        User_ID = item.User_ID,
                        Description = item.Description,
                        Start_Date = item.Start_Date,
                        Finish_Date = item.Finish_Date
                    });
                }
                catch (Exception)
                {
                }
            }
            return banDto;
        }

        public List<BuildingDto> GetBuildings()
        {
            List<BuildingDto> buildingDto = new List<BuildingDto>();
            foreach (var item in _buildings.GetAll())
            {
                try
                {
                    buildingDto.Add(new BuildingDto
                    {
                        ID = item.ID,
                        Name = item.Name,
                        Price = item.Price,
                        Percent_price_per_lvl = item.Percent_price_per_lvl,
                        Width = item.Width,
                        Height = item.Height,
                        Product_per_sec = item.Product_per_sec,
                        Dest_price = item.Dest_price,
                        Percent_product_per_lvl = item.Percent_product_per_lvl,
                        Product_ID = item.Product_ID,
                        Alias = item.Alias,
                        BuildingTime = item.BuildingTime,
                        DestructionTime = item.DestructionTime
                    });
                }
                catch (Exception)
                {
                }
            }
            return buildingDto;
        }

        public List<BuildingQueueDto> GetQueues()
        {
            List<BuildingQueueDto> buildingQueueDto = new List<BuildingQueueDto>();
            foreach (var item in _buildingQueue.GetAll())
            {
                try
                {
                    buildingQueueDto.Add(new BuildingQueueDto
                    {
                        ID = item.ID,
                        User_ID = item.User_ID,
                        UserBuilding_ID = item.UserBuilding_ID,
                        FinishTime = (DateTime)item.FinishTime,
                        NewStatus = item.NewStatus
                    });
                }
                catch (Exception)
                {
                }
            }
            return buildingQueueDto;
        }

        public List<DolarDto> GetDolars()
        {
            List<DolarDto> dolarDto = new List<DolarDto>();
            foreach (var item in _dolars.GetAll())
            {
                try
                {
                    dolarDto.Add(new DolarDto
                    {
                        ID = item.ID,
                        User_ID = item.User_ID,
                        Value = item.Value
                    });
                }
                catch (Exception)
                {
                }
            }
            return dolarDto;
        }

        public List<MapDto> GetMaps()
        {
            List<MapDto> mapDto = new List<MapDto>();
            foreach (var item in _maps.GetAll())
            {
                try
                {
                    mapDto.Add(new MapDto
                    {
                        Map_ID = item.Map_ID,
                        User_ID = item.User_ID,
                        Width = item.Width,
                        Height = item.Height
                    });
                }
                catch (Exception)
                {
                }
            }
            return mapDto;
        }

        public List<ProductDto> GetProducts()
        {
            List<ProductDto> productDto = new List<ProductDto>();
            foreach (var item in _products.GetAll())
            {
                try
                {
                    productDto.Add(new ProductDto
                    {
                        ID = item.ID,
                        Name = item.Name,
                        Price_per_unit = item.Price_per_unit,
                        Unit = item.Unit,
                        Alias = item.Alias
                    });
                }
                catch (Exception)
                {
                }
            }
            return productDto;
        }

        public List<UserBuildingDto> GetUserBuildings()
        {
            List<UserBuildingDto> userBuildingsDto = new List<UserBuildingDto>();
            foreach (var item in _userBuildings.GetAll())
            {
                try
                {
                    userBuildingsDto.Add(new UserBuildingDto
                    {
                        ID = item.ID,
                        User_ID = item.User_ID,
                        X_pos = item.X_pos,
                        Y_pos = item.Y_pos,
                        Lvl = item.Lvl,
                        Building_ID = item.Building_ID,
                        Status = item.Status
                    });
                }
                catch (Exception)
                {
                }
            }
            return userBuildingsDto;
        }

        public List<UserProductDto> GetUserProducts()
        {
            List<UserProductDto> userProductsDto = new List<UserProductDto>();
            foreach (var item in _userProducts.GetAll())
            {
                try
                {
                    userProductsDto.Add(new UserProductDto
                    {
                        ID = item.ID,
                        User_ID = item.User_ID,
                        Product_Name = item.Product_Name,
                        Value = item.Value,
                        Product_ID = item.Product_ID
                    });
                }
                catch (Exception)
                {
                }
            }
            return userProductsDto;
        }
    }
}
