using Game.Service.Interfaces.TableInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Core.DTO;
using Game.Dal.Repository;
using Game.Dal.Model;
using Game.Dal;

namespace Game.Service.Table
{
    public class UserProductService : IUserProductService
    {

        private IRepository<UserProducts> _userProducts;
        private IRepository<Products> _product;
        private IRepository<Users> _user;
        private IRepository<ProductRequirements> _productRequirement;
        private IUnitOfWork _unitOfWork;

        public UserProductService(IRepository<UserProducts> userProducts, IRepository<Users> user, IRepository<Products> product, IRepository<ProductRequirements> productRequirement, IUnitOfWork unitOfWork)
        {
            _userProducts = userProducts;
            _product = product;
            _user = user;
            _productRequirement = productRequirement;
            _unitOfWork = unitOfWork;
        }

        public void Add(UserProductDto userProduct)
        {
            _userProducts.Add(new UserProducts
            {
                User_ID = _user.GetAll().First(i => i.Login == userProduct.Login).ID,
                Product_Name = userProduct.Product_Name,
                Value = userProduct.Value,
                Product_ID = _product.GetAll().First(i => i.Alias == userProduct.Product_Name).ID
            });

            _unitOfWork.Commit();
        }

        public void Delete(int id)
        {
            _userProducts.Delete(_userProducts.Get(id));
            _unitOfWork.Commit();
        }

        public List<UserProductDto> GetUserProduct()
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
                        Login = _user.GetAll().First(i => i.ID == item.User_ID).Login,
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

        public List<UserProductDto> GetUserProductList(string User)
        {
            int uID = _user.GetAll().First(i => i.Login == User).ID;
            List<UserProductDto> userProductsDto = new List<UserProductDto>();
            foreach (var item in _userProducts.GetAll().Where(i => i.User_ID == uID))
            {
                try
                {
                    userProductsDto.Add(new UserProductDto
                    {
                        ID = item.ID,
                        User_ID = item.User_ID,
                        Login = _user.GetAll().First(i => i.ID == item.User_ID).Login,
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

        public void Update(UserProductDto userProduct)
        {
            foreach (var item in _userProducts.GetAll().Where(i => i.ID == userProduct.ID))
            {
                item.User_ID = _user.GetAll().First(i => i.Login == userProduct.Login).ID;
                item.Product_ID = _product.GetAll().First(i => i.Alias == userProduct.Product_Name).ID;
                item.Value = userProduct.Value;
            }

            _unitOfWork.Commit();
        }

        public void CreateProduct(int value, string productName, string user)
        {
            var product = _product.GetAll().First(i => i.Alias == productName);
            var uID = _user.GetAll().First(i => i.Login == user).ID;
            bool ifcan = true;

            if(_userProducts.GetAll().Any(i=> i.Product_ID == product.ID))
            {
                _userProducts.GetAll().First(i => i.Product_ID == product.ID && i.User_ID == uID).Value += value;
            }
            else
            {
                _userProducts.Add(new UserProducts
                {
                    Product_ID = product.ID,
                    Product_Name = product.Name,
                    User_ID = uID,
                    Value = value
                });
            }

            foreach (var item in _userProducts.GetAll().Where(i => i.Product_ID == product.ID))
            {
                foreach (var item2 in _productRequirement.GetAll().Where(i => i.Base_ID == product.ID))
                {
                    int temp = value * item2.Value;
                    int productValue = _userProducts.GetAll().First(i => i.Product_ID == item2.Require_ID && i.User_ID == uID).Value;
                   

                    if(item2.Value > productValue)
                    {
                        ifcan = false;
                    }

                    _userProducts.GetAll().First(i => i.Product_ID == item2.Require_ID && i.User_ID == uID).Value -= temp;
                }
            }

            if (ifcan)
            {
                _unitOfWork.Commit();
            }
        }
    }
}
