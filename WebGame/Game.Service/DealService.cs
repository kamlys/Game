using Game.Core.DTO;
using Game.Dal;
using Game.Dal.Model;
using Game.Dal.Repository;
using Game.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Service
{
    public class DealService : IDealService
    {
        private IRepository<Deals> _deals;
        private IRepository<Users> _users;
        private IRepository<Buildings> _buildings;
        private IRepository<UserBuildings> _userBuildings;
        private IRepository<Maps> _maps;
        private IRepository<Dolars> _dolars;
        private IRepository<DealsBuildings> _dealsBuildings;
        private IUnitOfWork _unitOfWork;

        public DealService(IRepository<Deals> deals,
            IRepository<Users> users,
            IRepository<Buildings> buildings,
            IRepository<UserBuildings> userBuildings,
            IRepository<Maps> maps,
            IRepository<Dolars> dolars,
            IRepository<DealsBuildings> dealsBuildings,
            IUnitOfWork unitOfWork)
        {
            _deals = deals;
            _users = users;
            _buildings = buildings;
            _userBuildings = userBuildings;
            _maps = maps;
            _dolars = dolars;
            _dealsBuildings = dealsBuildings;
            _unitOfWork = unitOfWork;
        }

        public List<DealDto> GetDeals()
        {
            List<DealDto> dealList = new List<DealDto>();
            try
            {
                foreach (var item in _deals.GetAll())
                {
                    dealList.Add(new DealDto
                    {
                        ID = item.ID,
                        User1_Login = _users.Get(item.User1_ID).Login,
                        User2_Login = _users.Get(item.User2_ID).Login,
                        Building_Name = _buildings.Get(item.Building_ID).Alias,
                        Percent_User1 = item.Percent_User1,
                        Percent_User2 = item.Percent_User2,
                        IsActive = item.IsActive,
                        FinishDate = item.FinishDate,
                        DayTime = item.DayTime,
                        Map_ID = item.Map_ID
                    });
                }
            }
            catch (Exception)
            {
            }

            return dealList;
        }

        public void AddDealAdmin(DealDto dealDto)
        {
            _deals.Add(new Deals
            {
                Building_ID = _buildings.GetAll().First(i => i.Alias == dealDto.Building_Name).ID,
                IsActive = dealDto.IsActive,
                Map_ID = dealDto.Map_ID,
                Percent_User1 = dealDto.Percent_User1,
                Percent_User2 = dealDto.Percent_User2,
                User1_ID = _users.GetAll().First(i => i.Login == dealDto.User1_Login).ID,
                User2_ID = _users.GetAll().First(i => i.Login == dealDto.User2_Login).ID,
                DayTime = dealDto.DayTime,
                FinishDate = DateTime.Now.AddDays(dealDto.DayTime)
            });

            _unitOfWork.Commit();
        }

        public void DeleteDealAdmin(int id)
        {
            _deals.Delete(_deals.Get(id));

            _unitOfWork.Commit();
        }

        public void UpdateDealAdmin(DealDto dealDto)
        {
            foreach (var item in _deals.GetAll().Where(i=> i.ID == dealDto.ID))
            {
                item.IsActive = dealDto.IsActive;
                item.Map_ID = dealDto.Map_ID;
                item.Percent_User1 = dealDto.Percent_User1;
                item.Percent_User2 = dealDto.Percent_User2;
                item.User1_ID = _users.GetAll().First(i => i.Login == dealDto.User1_Login).ID;
                item.User2_ID = _users.GetAll().First(i => i.Login == dealDto.User2_Login).ID;
                item.FinishDate = DateTime.Now.AddDays(dealDto.DayTime);
                item.DayTime = dealDto.DayTime;
                item.Building_ID = _buildings.GetAll().First(i => i.Alias == dealDto.Building_Name).ID;
            }

            _unitOfWork.Commit();
        }

        public List<DealBuildingDto> GetDealBuildings()
        {
            List<DealBuildingDto> dealbDto = new List<DealBuildingDto>();

            try
            {
                foreach (var item in _dealsBuildings.GetAll())
                {
                    dealbDto.Add(new DealBuildingDto
                    {
                        BuildingName = _buildings.Get(item.Building_ID).Alias,
                        Deal_ID = item.Deal_ID,
                        User_ID = item.User_ID,
                        Login = _users.Get(item.User_ID).Login
                    });
                }
            }
            catch (Exception)
            {
            }

            return dealbDto;
        }

        public void AddDealBuildingAdmin(DealBuildingDto dealDto)
        {
            _dealsBuildings.Add(new DealsBuildings
            {
                Deal_ID = dealDto.Deal_ID,
                Building_ID = _buildings.GetAll().First(i => i.Alias == dealDto.BuildingName).ID,
                User_ID = _users.GetAll().First(i => i.Login == dealDto.Login).ID,
            });

            _unitOfWork.Commit();
        }

        public void DeleteDealBuildingAdmin(int id)
        {
            _dealsBuildings.Delete(_dealsBuildings.Get(id));
            _unitOfWork.Commit();
        }

        public void UpdateDealBuildingAdmin(DealBuildingDto dealDto)
        {
            throw new NotImplementedException();
        }

        public List<DealDto> GetUserDeals(string User)
        {
            int uID = _users.GetAll().First(i => i.Login == User).ID;
            List<DealDto> dealList = new List<DealDto>();
            bool myMap = false;

            try
            {
                foreach (var item in _deals.GetAll().Where(i => i.User1_ID == uID || i.User2_ID == uID))
                {
                    if (item.Map_ID == _maps.GetAll().First(i => i.User_ID == uID).Map_ID)
                    {
                        myMap = true;
                    }

                    dealList.Add(new DealDto
                    {
                        ID = item.ID,
                        User1_Login = _users.Get(item.User1_ID).Login,
                        User2_Login = _users.Get(item.User2_ID).Login,
                        Building_Name = _buildings.Get(item.Building_ID).Alias,
                        Percent_User1 = item.Percent_User1,
                        Percent_User2 = item.Percent_User2,
                        IsActive = item.IsActive,
                        MyMap = myMap,
                        FinishDate = item.FinishDate,
                        DayTime = item.DayTime
                    });
                }
            }
            catch (Exception)
            {
            }

            return dealList;

        }

        public int[] AddDeal(DealDto dealDto)
        {
            List<int> val = new List<int>();

            if (_users.GetAll().Any(i => i.Login == dealDto.User2_Login))
            {
                if (_buildings.GetAll().Any(i => i.Alias == dealDto.Building_Name))
                {
                    int user1ID = _users.GetAll().First(i => i.Login == dealDto.User1_Login).ID;
                    int user2ID = _users.GetAll().First(i => i.Login == dealDto.User2_Login).ID;
                    int buildingID = _buildings.GetAll().First(i => i.Alias == dealDto.Building_Name).ID;
                    int mapID = 0;
                    if (dealDto.Map_ID == 1)
                    {
                        mapID = _maps.GetAll().First(i => i.User_ID == user1ID).Map_ID;
                    }
                    else if (dealDto.Map_ID == 0)
                    {
                        mapID = _maps.GetAll().First(i => i.User_ID == user2ID).Map_ID;
                    }

                    if (dealDto.Percent_User1 >= 10 && dealDto.Percent_User1 <= 90)
                    {
                        _deals.Add(new Deals
                        {
                            User1_ID = user1ID,
                            User2_ID = user2ID,
                            Building_ID = _buildings.GetAll().First(i => i.Alias == dealDto.Building_Name).ID,
                            Map_ID = mapID,
                            IsActive = false,
                            Percent_User1 = dealDto.Percent_User1,
                            Percent_User2 = 100 - dealDto.Percent_User1,
                            FinishDate = (DateTime)dealDto.FinishDate,
                            DayTime = dealDto.DayTime
                        });

                        _unitOfWork.Commit();
                        int price = ((_buildings.Get(buildingID).Price * dealDto.Percent_User1) / 100);
                        int dolar = _dolars.GetAll().First(i => i.User_ID == user1ID).Value;

                        _dolars.GetAll().First(i => i.User_ID == user1ID).Value -= price;
                        _unitOfWork.Commit();

                        val.Add(1); //Oferta złożona
                    }
                }
                else
                {
                    val.Add(2); //Nie ma takiego budynku
                }
            }
            else
            {
                val.Add(0); //Nie ma takiego użytkownika   
            }

            return val.ToArray();
        }

        public bool AcceptDeal(int ID, string user)
        {
            int uID = _users.GetAll().First(i => i.Login == user).ID;
            int buildingID = _deals.Get(ID).Building_ID;
            int price = _buildings.Get(buildingID).Price;
            int myPercent = _deals.Get(ID).Percent_User2;
            int myPrice = ((price * myPercent) / 100);

            if (_dolars.GetAll().First(i => i.User_ID == uID).Value >= myPrice)
            {
                _deals.Get(ID).IsActive = true;
                if ((_deals.Get(ID).FinishDate.Subtract(DateTime.Now)).Days > 20)
                {
                    _deals.Get(ID).FinishDate = DateTime.Now.AddDays(30);
                    _deals.Get(ID).DayTime = 30;
                }
                else if ((_deals.Get(ID).FinishDate.Subtract(DateTime.Now)).Days < 20 && (_deals.Get(ID).FinishDate.Subtract(DateTime.Now)).Days > 10)
                {
                    _deals.Get(ID).FinishDate = DateTime.Now.AddDays(20);
                    _deals.Get(ID).DayTime = 20;
                }
                else if ((_deals.Get(ID).FinishDate.Subtract(DateTime.Now)).Days < 10 && (_deals.Get(ID).FinishDate.Subtract(DateTime.Now)).Days > 7)
                {
                    _deals.Get(ID).FinishDate = DateTime.Now.AddDays(10);
                    _deals.Get(ID).DayTime = 10;
                }
                else
                {
                    _deals.Get(ID).FinishDate = DateTime.Now.AddDays(7);
                    _deals.Get(ID).DayTime = 7;
                }
                _dolars.GetAll().First(i => i.User_ID == uID).Value -= myPrice;
                var ownerID = _deals.Get(ID).Maps.User_ID;
                _dealsBuildings.Add(new DealsBuildings { Building_ID = buildingID, User_ID = ownerID, Deal_ID = ID });

                _unitOfWork.Commit();

                return true;
            }
            return false;
        }

        public void CancelDeal(int ID)
        {
            foreach (var item in _dealsBuildings.GetAll().Where(i => i.Deal_ID == ID))
            {
                _dealsBuildings.Delete(_dealsBuildings.Get(item.ID));
            }
            _deals.Delete(_deals.Get(ID));
            _unitOfWork.Commit();
        }

        public void RerunDeal(int ID, string user)
        {
            int temp1, temp2;
            int uID = _users.GetAll().First(i => i.Login == user).ID;

            if (uID == _deals.Get(ID).User1_ID)
            {
                _deals.Get(ID).IsActive = false;
                _deals.Get(ID).FinishDate = DateTime.Now.AddDays(_deals.Get(ID).DayTime);
            }
            else
            {
                temp1 = _deals.Get(ID).User1_ID;
                temp2 = _deals.Get(ID).User2_ID;
                _deals.Get(ID).IsActive = false;
                _deals.Get(ID).FinishDate = DateTime.Now.AddDays(_deals.Get(ID).DayTime);
                _deals.Get(ID).User1_ID = temp2;
                _deals.Get(ID).User2_ID = temp1;
            }
            _unitOfWork.Commit();
        }

    }
}
