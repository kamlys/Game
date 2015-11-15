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
    public class UserProductTableService : IUserProductTableService
    {

        private IRepository<UserProducts> _userProducts;
        private IUnitOfWork _unitOfWork;

        public UserProductTableService(IRepository<UserProducts> userProducts, IUnitOfWork unitOfWork)
        {
            _userProducts = userProducts;
            _unitOfWork = unitOfWork;
        }

        public void Add(UserProductDto userProduct)
        {
            _userProducts.Add(new UserProducts
            {
                User_ID = userProduct.User_ID,
                Product_Name = userProduct.Product_Name,
                Value = userProduct.Value,
                Product_ID = userProduct.Product_ID
            });

            _unitOfWork.Commit();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
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
