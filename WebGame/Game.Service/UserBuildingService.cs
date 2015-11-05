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
        private IBuildingHelper _buildingHelper;
        private IUnitOfWork _unitOfWork;

        public UserBuildingService(
            IRepository<UserBuildings> userBuildings,
            IRepository<UserProducts> userProducts,
            IRepository<Users> users,
            IRepository<Buildings> buildings,
            IRepository<Products> products,
            IRepository<Dolars> dolars,
            IBuildingHelper buildingHelper,
            IUnitOfWork unitOfWork)
        {
            _userBuildings = userBuildings;
            _unitOfWork = unitOfWork;
            _users = users;
            _buildings = buildings;
            _products = products;
            _userProducts = userProducts;
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
                User_ID = uID
            });
            _dolars.GetAll().First(u => u.User_ID == uID).Value -= buildPrice;

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

        public bool Destroy(string user, int ID)
        {
            int uID = _users.GetAll().First(a => a.Login == user).ID;

            _userBuildings.Delete(u => u.User_ID == uID && u.ID == ID);
            _unitOfWork.Commit();

            return true;
        }

    }
}
