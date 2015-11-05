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
    public class ProductService : IProductService
    {
        private IRepository<Products> _product;
        private IRepository<UserProducts> _userProduct;
        private IRepository<Users> _user;
        private IRepository<Buildings> _building;
        private IRepository<UserBuildings> _userBuilding;
        private IBuildingHelper _buildingHelper;
        private IUnitOfWork _unitOfWork;

        public ProductService(
            IRepository<Users> user,
            IRepository<UserProducts> userProduct,
            IRepository<Buildings> building,
            IRepository<UserBuildings> userBuilding,
            IRepository<Products> products,
            IBuildingHelper buildingHelper,
            IUnitOfWork unitOfWork)
        {
            _user = user;
            _userProduct = userProduct;
            _building = building;
            _userBuilding = userBuilding;
            _product = products;
            _buildingHelper = buildingHelper;
            _unitOfWork = unitOfWork;
        }


        public void UpdateUserProduct(string User)
        {
            int uID = _user.GetAll().First(u => u.Login == User).ID;


            int dateSubstract = (int)DateTime.Now.Subtract((DateTime)(_user.GetAll().First(u => u.ID == uID).Last_Update)).TotalSeconds;

            if (_user.GetAll().First(u => u.ID == uID).Last_Update == null)
            {
                _user.GetAll().First(u => u.ID == uID).Last_Update = _user.GetAll().First(u => u.ID == uID).Registration_Date;
            }
            foreach (var item in _userProduct.GetAll().Where(u => u.User_ID == uID))
            {
                int pID = item.Product_ID;
                foreach (var item2 in _buildingHelper.AddProductValue(uID, pID).Where(b => b.Key == pID))
                {
                    item.Value += (Convert.ToInt32(item2.Value * dateSubstract));
                }
            }

            _user.GetAll().First(u => u.ID == uID).Last_Update = DateTime.Now;

            _unitOfWork.Commit();
        }
    }
}
