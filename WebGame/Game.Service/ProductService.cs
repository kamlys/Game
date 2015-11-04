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
        private IUnitOfWork _unitOfWork;

        public ProductService(
            IRepository<Users> user,
            IRepository<UserProducts> userProduct,
            IRepository<Buildings> building,
            IRepository<UserBuildings> userBuilding,
            IRepository<Products> products,
            IUnitOfWork unitOfWork)
        {
            _user = user;
            _userProduct = userProduct;
            _building = building;
            _userBuilding = userBuilding;
            _product = products;
            _unitOfWork = unitOfWork;
        }


        public void UpdateUserProduct(string User)
        {
            int uID = _user.GetAll().First(u => u.Login == User).ID;


            if (_user.GetAll().First(u => u.ID == uID).Last_Update == null)
            {
                _user.GetAll().First(u => u.ID == uID).Last_Update = _user.GetAll().First(u => u.ID == uID).Registration_Date;
            }

            var dateSubstract = DateTime.Now.Subtract((DateTime)(_user.GetAll().First(u => u.ID == uID).Last_Update)).TotalSeconds;

            foreach (var item in _userProduct.GetAll().Where(u => u.User_ID == uID))
            {
                int pID = item.Product_ID;

                int Product_per_lvl = _building.GetAll().First(p => p.Product_ID == pID).Product_per_sec;
                int Percent_per_lvl = _building.GetAll().First(p => p.Product_ID == pID).Percent_product_per_lvl;
                int BuildLvl = _userBuilding.GetAll().First(b => b.Buildings.Product_ID == pID).Lvl;

                item.Value += (int)Math.Round((Product_per_lvl * (Percent_per_lvl * 0.01) * BuildLvl) * dateSubstract);
            }

            _user.GetAll().First(u => u.ID == uID).Last_Update = DateTime.Now;

            _unitOfWork.Commit();

        }
    }
}
