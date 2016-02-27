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
    public class ProductService : IProductService
    {
        private IRepository<Products> _product;
        private IRepository<UserProducts> _userProduct;
        private IRepository<Users> _user;
        private IRepository<Buildings> _building;
        private IRepository<UserBuildings> _userBuilding;
        private IRepository<ProductRequirements> _requirementsProduct;
        private IBuildingHelper _buildingHelper;
        private IRepository<Deals> _deal;
        private IUnitOfWork _unitOfWork;

        public ProductService(
            IRepository<Users> user,
            IRepository<UserProducts> userProduct,
            IRepository<Buildings> building,
            IRepository<UserBuildings> userBuilding,
            IRepository<Products> products,
            IRepository<ProductRequirements> requirementsProduct,
            IBuildingHelper buildingHelper,
            IRepository<Deals> deal,
            IUnitOfWork unitOfWork)
        {
            _user = user;
            _userProduct = userProduct;
            _building = building;
            _userBuilding = userBuilding;
            _product = products;
            _requirementsProduct = requirementsProduct;
            _buildingHelper = buildingHelper;
            _deal = deal;
            _unitOfWork = unitOfWork;
        }


        public void UpdateUserProduct(string User)
        {
            _buildingHelper.ChangeStatus(User);

            int uID = _user.GetAll().First(u => u.Login == User).ID;

            int dateSubstract = (int)DateTime.Now.Subtract((DateTime)(_user.GetAll().First(u => u.ID == uID).Last_Update)).TotalSeconds;

            // dodajemy co minute
            int intervals = dateSubstract / 60;
            if (intervals > 0)
            {
                if (_user.GetAll().First(u => u.ID == uID).Last_Update == null)
                {
                    _user.GetAll().First(u => u.ID == uID).Last_Update = _user.GetAll().First(u => u.ID == uID).Registration_Date;
                }
                foreach (var itemBuilding in _userBuilding.GetAll().Where(b => b.User_ID == uID && b.Status.Contains("gotowy") && b.Buildings.Stock == true))
                {
                    int? ifEndDeal = null;
                    if (itemBuilding.DealID != null) //Jeśli budynek z umowy
                    {
                        ifEndDeal = _deal.Get((int)itemBuilding.DealID).FinishDate.Subtract(DateTime.Now).Seconds;
                    }
                    if (itemBuilding.DealID == null || ifEndDeal>0)
                    {
                        // jeśli wybudowano przed ostatnim update - jest ok
                        if (itemBuilding.DateOfConstruction < (DateTime)(_user.GetAll().First(u => u.ID == uID).Last_Update))
                        {
                            int bID = itemBuilding.Buildings.Product_ID;
                            foreach (var item in _userProduct.GetAll().Where(u => u.User_ID == uID && u.Product_ID == bID))
                            {
                                int pID = item.Product_ID;
                                double tempValue = (((double)Fibonacci(itemBuilding.Lvl) * (double)intervals * 10.0) * ((double)itemBuilding.Percent_product / 100.0));
                                item.Value += (int)tempValue;
                            }
                        }
                        else // wpp obliczamy ile można dodać
                        {
                            int newDateSubstract = (int)DateTime.Now.Subtract((DateTime)itemBuilding.DateOfConstruction).TotalSeconds;
                            // 1 co 60 sekund
                            int newIntervals = newDateSubstract / 6;
                            int bID = itemBuilding.Buildings.Product_ID;
                            foreach (var item in _userProduct.GetAll().Where(u => u.User_ID == uID && u.Product_ID == bID))
                            {
                                int pID = item.Product_ID;
                                double tempValue = ((double)Fibonacci(itemBuilding.Lvl) * (double)newIntervals) * ((double)itemBuilding.Percent_product / 100.0);
                                item.Value += (int)tempValue;
                            }
                        }
                    }
                }

                _user.GetAll().First(u => u.ID == uID).Last_Update = (DateTime)(_user.GetAll().First(u => u.ID == uID).Last_Update.Value.AddMinutes(intervals));

                _unitOfWork.Commit();
            }
        }
        public static int Fibonacci(int n)
        {
            int a = 0;
            for (int i = 1; i <= n; i++)
            {
                a += i;
            }
            return a;
        }

        public bool Add(ProductDto product)
        {
            try
            {
                if (!_product.GetAll().Any(i => i.Name == product.Name || i.Alias == product.Alias)
                    && product.Price_per_unit > 0)
                {
                    _product.Add(new Products
                    {
                        Name = product.Name,
                        Price_per_unit = product.Price_per_unit,
                        Unit = product.Unit,
                        Alias = product.Alias
                    });

                    _unitOfWork.Commit();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                foreach (var item in _building.GetAll().Where(i => i.Product_ID == id))
                {
                    _building.Delete(_building.Get(item.ID));
                    _unitOfWork.Commit();
                }

                foreach (var item in _requirementsProduct.GetAll().Where(i => i.Base_ID == id || i.Require_ID == id))
                {
                    _requirementsProduct.Delete(_requirementsProduct.Get(item.ID));
                    _unitOfWork.Commit();
                }

                _product.Delete(_product.Get(id));
                _unitOfWork.Commit();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<ProductDto> GetProduct()
        {
            List<ProductDto> productDto = new List<ProductDto>();
            foreach (var item in _product.GetAll())
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

        public bool Update(ProductDto product)
        {
            if (product.Price_per_unit > 0)
            {
                foreach (var item in _product.GetAll().Where(i => i.ID == product.ID))
                {
                    item.Name = product.Name;
                    item.Price_per_unit = product.Price_per_unit;
                    item.Unit = product.Unit;
                    item.Alias = product.Alias;
                }

                _unitOfWork.Commit();
                return true;
            }
            return false;
        }

    }
}
