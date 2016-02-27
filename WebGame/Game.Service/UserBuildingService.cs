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
    public class UserBuildingService : IUserBuildingService
    {
        private IRepository<UserBuildings> _userBuildings;
        private IRepository<UserProducts> _userProducts;
        private IRepository<Products> _products;
        private IRepository<Dolars> _dolars;
        private IRepository<Buildings> _buildings;
        private IRepository<Users> _users;
        private IRepository<BuildingQueue> _buildingQueue;
        private IRepository<DealsBuildings> _dealBuilgind;
        private IRepository<Deals> _deal;
        private IBuildingHelper _buildingHelper;
        private IUnitOfWork _unitOfWork;

        public UserBuildingService(
            IRepository<UserBuildings> userBuildings,
            IRepository<UserProducts> userProducts,
            IRepository<Users> users,
            IRepository<Buildings> buildings,
            IRepository<Products> products,
            IRepository<Dolars> dolars,
            IRepository<Deals> deal,
        IRepository<BuildingQueue> buildingQueue,
        IRepository<DealsBuildings> dealBuilding,
        IBuildingHelper buildingHelper,
            IUnitOfWork unitOfWork)
        {
            _userBuildings = userBuildings;
            _unitOfWork = unitOfWork;
            _users = users;
            _buildings = buildings;
            _products = products;
            _userProducts = userProducts;
            _buildingQueue = buildingQueue;
            _dolars = dolars;
            _deal = deal;
            _dealBuilgind = dealBuilding;
            _buildingHelper = buildingHelper;
        }


        public bool Build(int id, int col, int row, int dealID, string user)
        {
            int uID = _users.GetAll().First(a => a.Login == user).ID;
            int buildPrice = _buildings.GetAll().First(b => b.ID == id).Price;
            var dolarsAccount = _dolars.GetAll().First(u => u.User_ID == uID).Value;
            int idProduct = _buildings.GetAll().First(b => b.ID == id).Product_ID;
            bool create = true;
            int percent = 100;
            int? deal_id = null;

            if (dealID != 0)
            {
                if (uID == _deal.Get(dealID).User1_ID)
                {
                    percent = _deal.Get(dealID).Percent_User1;
                }
                else
                {
                    percent = _deal.Get(dealID).Percent_User2;
                }

                deal_id = dealID;
            }
            _userBuildings.Add(new UserBuildings
            {
                Building_ID = id,
                Lvl = 1,
                X_pos = col,
                Y_pos = row,
                User_ID = uID,
                Status = "budowa",
                Percent_product = percent,
                Owner = true,
                Color = "bbbbbb",
                DateOfConstruction = DateTime.Now.AddSeconds(_buildings.Get(id).BuildingTime),
                DealID = deal_id
            });

            if (dealID == 0)
            {
                _dolars.GetAll().First(u => u.User_ID == uID).Value -= buildPrice;
            }

            _unitOfWork.Commit();

            var userBuildingID = _userBuildings.GetAll().OrderByDescending(i => i.ID).First(u => u.User_ID == uID && u.Building_ID == id && u.Status.Contains("budowa")).ID;

            _buildingQueue.Add(new BuildingQueue
            {
                User_ID = uID,
                UserBuilding_ID = userBuildingID,
                FinishTime = DateTime.Now.AddSeconds(_buildings.Get(id).BuildingTime),
                NewStatus = "gotowy"
            });

            _unitOfWork.Commit();

            foreach (var item in _userProducts.GetAll().Where(u => u.User_ID == uID))
            {
                if (item.Product_ID == idProduct)
                {
                    create = false;
                }
            }

            if (create)
            {
                _userProducts.Add(new UserProducts
                {
                    User_ID = uID,
                    Product_Name = _products.GetAll().First(i => i.ID == idProduct).Name,
                    Value = 0,
                    Product_ID = idProduct
                });

                _unitOfWork.Commit();
            }

            if (dealID != 0)
            {
                int userDealID;

                if (uID == _deal.Get(dealID).User1_ID)
                {
                    userDealID = _deal.Get(dealID).User2_ID;
                }
                else
                {
                    userDealID = _deal.Get(dealID).User1_ID;
                }

                _userBuildings.Add(new UserBuildings
                {
                    Building_ID = id,
                    Lvl = 1,
                    X_pos = col,
                    Y_pos = row,
                    User_ID = userDealID,
                    Status = "budowa",
                    Percent_product = 100 - percent,
                    Owner = false,
                    Color = "bbbbbb",
                    DateOfConstruction = DateTime.Now.AddSeconds(_buildings.Get(id).BuildingTime),
                    DealID = deal_id
                });

                _unitOfWork.Commit();

                var userDealBuildingID = _userBuildings.GetAll().OrderByDescending(i => i.ID).First(u => u.User_ID == userDealID && u.Building_ID == id && u.Status.Contains("budowa")).ID;

                _buildingQueue.Add(new BuildingQueue
                {
                    User_ID = userDealID,
                    UserBuilding_ID = userDealBuildingID,
                    FinishTime = DateTime.Now.AddSeconds(_buildings.Get(id).BuildingTime),
                    NewStatus = "gotowy"
                });

                _unitOfWork.Commit();

                foreach (var item in _userProducts.GetAll().Where(u => u.User_ID == userDealID))
                {
                    if (item.Product_ID == idProduct)
                    {
                        create = false;
                    }
                }

                if (create)
                {
                    _userProducts.Add(new UserProducts
                    {
                        User_ID = userDealID,
                        Product_Name = _products.GetAll().First(i => i.ID == idProduct).Name,
                        Value = 0,
                        Product_ID = idProduct
                    });

                    _unitOfWork.Commit();
                }

                _dealBuilgind.Delete(_dealBuilgind.GetAll().First(i => i.Deal_ID == dealID));
                _unitOfWork.Commit();
            }


            return true;
        }

        public void Destroy(string user, int ID)
        {
            int uID = _users.GetAll().First(a => a.Login == user).ID;

            var buildingID = _userBuildings.Get(ID).Building_ID;
            var destroyPrice = _buildings.Get(buildingID).Dest_price;
            var userDolar = _dolars.GetAll().First(i => i.User_ID == uID).Value;

            if (userDolar >= destroyPrice)
            {
                _userBuildings.Get(ID).Status = "burzenie";

                _buildingQueue.Add(new BuildingQueue
                {
                    User_ID = uID,
                    UserBuilding_ID = ID,
                    FinishTime = DateTime.Now.AddSeconds(_buildings.Get(buildingID).DestructionTime),
                    NewStatus = "wyburzony"
                });

                _dolars.GetAll().First(i => i.User_ID == uID).Value -= (int)destroyPrice;

                _unitOfWork.Commit();
            }
        }



        public bool Add(UserBuildingDto userBuilding)
        {
            if (_buildings.GetAll().Any(i => i.Alias == userBuilding.Building_Name)
                && userBuilding.Lvl > 0
                && userBuilding.Percent_Product > 0
                && userBuilding.Percent_Product <= 100)
            {
                try
                {
                    DateTime tempDate;

                    if (userBuilding.Status.ToLower().Contains("budowa") || userBuilding.Status.ToLower().Contains("rozbudowa"))
                    {
                        tempDate = DateTime.Now.AddSeconds(_buildings.GetAll().First(i => i.Alias == userBuilding.Building_Name).BuildingTime);
                    }
                    else if (userBuilding.Status.ToLower().Contains("burzenie"))
                    {
                        tempDate = DateTime.Now.AddSeconds(_buildings.GetAll().First(i => i.Alias == userBuilding.Building_Name).DestructionTime);
                    }
                    else
                    {
                        tempDate = DateTime.Now;
                    }

                    _userBuildings.Add(new UserBuildings
                    {
                        User_ID = _users.GetAll().First(i => i.Login == userBuilding.Login).ID,
                        X_pos = userBuilding.X_pos,
                        Y_pos = userBuilding.Y_pos,
                        Lvl = userBuilding.Lvl,
                        Building_ID = _buildings.GetAll().First(i => i.Alias == userBuilding.Building_Name).ID,
                        Status = userBuilding.Status,
                        DateOfConstruction = (DateTime)tempDate,
                        Color = userBuilding.Color,
                        Percent_product = userBuilding.Percent_Product,
                        Owner = userBuilding.Owner
                    });

                    _unitOfWork.Commit();

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }

        public bool Delete(int id)
        {
            try
            {
                if (_userBuildings.Get(id).DealID == null)
                {
                    _userBuildings.Delete(_userBuildings.Get(id));
                    _unitOfWork.Commit();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public List<UserBuildingDto> GetUserBuilding()
        {
            List<UserBuildingDto> userBuildingsDto = new List<UserBuildingDto>();
            foreach (var item in _userBuildings.GetAll())
            {
                int BuildLvl = item.Lvl;
                int Product_per_sec = _buildings.GetAll().First(b => b.Product_ID == item.Buildings.Product_ID).Product_per_sec;
                int Percent_per_lvl = _buildings.GetAll().First(b => b.Product_ID == item.Buildings.Product_ID).Percent_product_per_lvl / 100;

                try
                {
                    userBuildingsDto.Add(new UserBuildingDto
                    {
                        ID = item.ID,
                        User_ID = item.User_ID,
                        Login = _users.Get(item.User_ID).Login,
                        X_pos = item.X_pos,
                        Y_pos = item.Y_pos,
                        Lvl = item.Lvl,
                        Building_ID = item.Building_ID,
                        Building_Name = _buildings.Get(item.Building_ID).Alias,
                        Status = item.Status,
                        Produkcja = Product_per_sec * Percent_per_lvl * BuildLvl,
                        Color = item.Color,
                        Owner = item.Owner,
                        Percent_Product = item.Percent_product,
                        DateOfConstruction = item.DateOfConstruction,
                        DealID = item.DealID
                    });
                }
                catch (Exception)
                {
                }
            }
            return userBuildingsDto;
        }

        public List<UserBuildingDto> GetUserBuildingList(string User)
        {
            int uID = _users.GetAll().First(i => i.Login == User).ID;

            List<UserBuildingDto> userBuildingsDto = new List<UserBuildingDto>();
            foreach (var item in _userBuildings.GetAll().Where(i => i.User_ID == uID))
            {
                int BuildLvl = item.Lvl;
                var temp = _buildings.GetAll().First(b => b.Product_ID == item.Buildings.Product_ID);
                double tempProductValue = (((double)ProductService.Fibonacci(BuildLvl) * 10.0) * ((double)item.Percent_product / 100.0));
                double tempUpProductValue = (((double)ProductService.Fibonacci(BuildLvl + 1) * 10.0) * ((double)item.Percent_product / 100.0));
                try
                {
                    userBuildingsDto.Add(new UserBuildingDto
                    {
                        ID = item.ID,
                        User_ID = item.User_ID,
                        Login = _users.Get(item.User_ID).Login,
                        X_pos = item.X_pos,
                        Y_pos = item.Y_pos,
                        Lvl = item.Lvl,
                        Building_ID = item.Building_ID,
                        Building_Name = _buildings.Get(item.Building_ID).Alias,
                        Status = item.Status,
                        Produkcja = (int)tempProductValue,
                        ProdukcjaLvlUp = (int)tempUpProductValue,
                        PriceLvlUp = _buildings.GetAll().First(b => b.Product_ID == item.Buildings.Product_ID).Price * (BuildLvl + 1),
                        Color = item.Color,
                        Stock = item.Buildings.Stock,
                        DealID = item.DealID
                    });
                }
                catch (Exception)
                {
                }
            }
            return userBuildingsDto;
        }

        public bool Update(UserBuildingDto userBuilding)
        {
            if (_buildings.GetAll().Any(i => i.Alias == userBuilding.Building_Name)
               && userBuilding.Lvl > 0
               && userBuilding.Percent_Product > 0
               && userBuilding.Percent_Product <= 100)
            {
                try
                {
                    foreach (var item in _userBuildings.GetAll().Where(i => i.ID == userBuilding.ID))
                    {
                        item.User_ID = _users.GetAll().First(i => i.Login == userBuilding.Login).ID;
                        item.Building_ID = _buildings.GetAll().First(i => i.Alias == userBuilding.Building_Name).ID;
                        item.X_pos = userBuilding.X_pos;
                        item.Y_pos = userBuilding.Y_pos;
                        item.Lvl = userBuilding.Lvl;
                        item.Status = userBuilding.Status;
                        item.DateOfConstruction = userBuilding.DateOfConstruction;
                        item.Owner = userBuilding.Owner;
                        item.Percent_product = userBuilding.Percent_Product;
                        item.Color = userBuilding.Color;
                    }

                    _unitOfWork.Commit();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }

        public bool ifLvlUp(int id, string User)
        {
            int uID = _users.GetAll().First(i => i.Login == User).ID;
            int temp = _userBuildings.Get(id).Building_ID;
            int userDolars = _dolars.GetAll().First(i => i.User_ID == uID).Value;
            int percentPricePerLvl = _buildings.GetAll().First(i => i.ID == temp).Price * (_userBuildings.Get(id).Lvl + 1);
            int buildingPrice = _buildings.GetAll().First(i => i.ID == temp).Price;
            var building = _userBuildings.GetAll().First(i => i.ID == id && i.User_ID == uID);

            if (userDolars >= percentPricePerLvl
                && building.Percent_product == 100
                && building.Status.Contains("gotowy"))
            {
                return true;
            }
            return false;
        }

        public bool LvlUp(int id, string User)
        {
            int uID = _users.GetAll().First(i => i.Login == User).ID;
            int temp = _userBuildings.Get(id).Building_ID;
            int buildingTime = _buildings.GetAll().First(i => i.ID == temp).BuildingTime;
            int userDolars = _dolars.GetAll().First(i => i.User_ID == uID).Value;
            int buildingPrice = _buildings.GetAll().First(i => i.ID == temp).Price;
            int percentPricePerLvl = buildingPrice * (_userBuildings.Get(id).Lvl + 1);

            if (ifLvlUp(id, User))
            {
                _buildingQueue.Add(
                    new BuildingQueue
                    {
                        UserBuilding_ID = id,
                        User_ID = uID,
                        FinishTime = DateTime.Now.AddSeconds(buildingTime),
                        NewStatus = "gotowy"
                    });

                _userBuildings.Get(id).Status = "rozbudowa";
                _userBuildings.Get(id).Lvl += 1;

                _dolars.GetAll().First(i => i.User_ID == uID).Value -= percentPricePerLvl;

                _unitOfWork.Commit();

                return true;
            }
            return false;
        }

        public List<DealBuildingDto> GetDealBuildingList(string user)
        {
            int uID = _users.GetAll().First(i => i.Login == user).ID;
            List<DealBuildingDto> dealBuilding = new List<DealBuildingDto>();


            foreach (var item in _dealBuilgind.GetAll().Where(i => i.User_ID == uID))
            {
                var building = _buildings.Get(item.Building_ID);

                dealBuilding.Add(new DealBuildingDto
                {
                    ID = building.ID,
                    Alias = building.Alias,
                    Height = building.Height,
                    Width = building.Width,
                    Deal_ID = item.Deal_ID,
                });
            }

            return dealBuilding;
        }

        public void ChangeColor(string color, int id, string user)
        {
            int uID = _users.GetAll().First(i => i.Login == user).ID;

            if (_userBuildings.GetAll().Any(i => i.ID == id && i.User_ID == uID))
            {
                _userBuildings.Get(id).Color = color;
                _unitOfWork.Commit();
            }
        }
    }
}
