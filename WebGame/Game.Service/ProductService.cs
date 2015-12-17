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
            _buildingHelper.ChangeStatus(User);

            int uID = _user.GetAll().First(u => u.Login == User).ID;

            int dateSubstract = (int)DateTime.Now.Subtract((DateTime)(_user.GetAll().First(u => u.ID == uID).Last_Update)).TotalSeconds;

            if (_user.GetAll().First(u => u.ID == uID).Last_Update == null)
            {
                _user.GetAll().First(u => u.ID == uID).Last_Update = _user.GetAll().First(u => u.ID == uID).Registration_Date;
            }
            foreach (var itemBuilding in _userBuilding.GetAll().Where(b => b.User_ID == uID && b.Status.Contains("gotowy") && b.Buildings.Stock == true))
            {
                int bID = itemBuilding.Buildings.Product_ID;
                foreach (var item in _userProduct.GetAll().Where(u => u.User_ID == uID && u.Product_ID == bID))
                {
                    int pID = item.Product_ID;
                    foreach (var item2 in _buildingHelper.AddProductValue(User))
                    {
                        item.Value += (Convert.ToInt32(item2[1] * dateSubstract));
                    }
                }
            }

            _user.GetAll().First(u => u.ID == uID).Last_Update = DateTime.Now;

            _unitOfWork.Commit();
        }


        public void Add(ProductDto product)
        {
            _product.Add(new Products
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
            _product.Delete(_product.Get(id));
            _unitOfWork.Commit();
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

        public void Update(ProductDto product)
        {
            foreach (var item in _product.GetAll().Where(i => i.ID == product.ID))
            {
                item.Name = product.Name;
                item.Price_per_unit = product.Price_per_unit;
                item.Unit = product.Unit;
                item.Alias = product.Alias;
            }

            _unitOfWork.Commit();
        }
    }
}
