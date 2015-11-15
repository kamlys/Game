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
    public class ProductTableService : IProductTableService
    {
        private IRepository<Products> _products;
        private IUnitOfWork _unitOfWork;

        public ProductTableService(IRepository<Products> products, IUnitOfWork unitOfWork)
        {
            _products = products;
            _unitOfWork = unitOfWork;
        }

        public void Add(ProductDto product)
        {
            _products.Add(new Products
            {
                Name = product.Name,
                Price_per_unit = product.Price_per_unit,
                Unit = product.Unit,
                Alias = product.Alias
            });

            _unitOfWork.Commit();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<ProductDto> GetProduct()
        {
            List<ProductDto> productDto = new List<ProductDto>();
            foreach (var item in _products.GetAll())
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

        public void Update(ProductDto product, int id)
        {
            throw new NotImplementedException();
        }
    }
}
