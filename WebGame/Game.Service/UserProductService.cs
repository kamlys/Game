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
        private IUnitOfWork _unitOfWork;

        public UserProductService(IRepository<UserProducts> userProducts, IRepository<Users> user, IRepository<Products> product, IUnitOfWork unitOfWork)
        {
            _userProducts = userProducts;
            _product = product;
            _user = user;
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

        public void Update(UserProductDto userProduct, int id)
        {
            throw new NotImplementedException();
        }
    }
}
