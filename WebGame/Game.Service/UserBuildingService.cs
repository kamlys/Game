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
        private IBuildingHelper _buildingHelper;
        private IUnitOfWork _unitOfWork;

        public UserBuildingService(
            IRepository<UserBuildings> userBuildings,
            IRepository<UserProducts> userProducts,
            IRepository<Users> users,
            IRepository<Buildings> buildings,
            IRepository<Products> products,
            IRepository<Dolars> dolars,
        IRepository<BuildingQueue> buildingQueue,
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
            _buildingHelper = buildingHelper;
        }


        public bool Build(int id, int col, int row, string user)
        {
            int uID = _users.GetAll().First(a => a.Login == user).ID;
            int buildPrice = _buildings.GetAll().First(b => b.ID == id).Price;
            var dolarsAccount = _dolars.GetAll().First(u => u.User_ID == uID).Value;
            int idProduct = _buildings.GetAll().First(b => b.ID == id).Product_ID;
            bool create = true;

            _userBuildings.Add(new UserBuildings
            {
                Building_ID = id,
                Lvl = 1,
                X_pos = col,
                Y_pos = row,
                User_ID = uID,
                Status = "budowa"
            });
            _dolars.GetAll().First(u => u.User_ID == uID).Value -= buildPrice;

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
            return true;
        }

        public void Destroy(string user, int ID)
        {
            int uID = _users.GetAll().First(a => a.Login == user).ID;

            var buildingID = _userBuildings.Get(ID).Building_ID;

            _userBuildings.Get(ID).Status = "burzenie";

            _buildingQueue.Add(new BuildingQueue
            {
                User_ID = uID,
                UserBuilding_ID = ID,
                FinishTime = DateTime.Now.AddSeconds(_buildings.Get(buildingID).DestructionTime),
                NewStatus = "wyburzony"
            });

            _unitOfWork.Commit();
        }



        public void Add(UserBuildingDto userBuilding)
        {
            _userBuildings.Add(new UserBuildings
            {
                User_ID = _users.GetAll().First(i=> i.ID == userBuilding.User_ID).ID,
                X_pos = userBuilding.X_pos,
                Y_pos = userBuilding.Y_pos,
                Lvl = userBuilding.Lvl,
                Building_ID = _buildings.Get(userBuilding.Building_ID).ID,
                Status = userBuilding.Status
            });

            _unitOfWork.Commit();
        }

        public void Delete(int id)
        {
            _userBuildings.Delete(_userBuildings.Get(id));
            _unitOfWork.Commit();
        }

        public List<UserBuildingDto> GetUserBuilding()
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
                        Login = _users.Get(item.User_ID).Login,
                        X_pos = item.X_pos,
                        Y_pos = item.Y_pos,
                        Lvl = item.Lvl,
                        Building_ID = item.Building_ID,
                        Building_Name = _buildings.Get(item.Building_ID).Alias,
                        Status = item.Status
                    });
                }
                catch (Exception)
                {
                }
            }
            return userBuildingsDto;
        }

        public void Update(UserBuildingDto userBuilding, int id)
        {
            throw new NotImplementedException();
        }
    }
}
