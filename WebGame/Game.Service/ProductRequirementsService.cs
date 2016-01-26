using Game.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Core.DTO;
using Game.Dal.Repository;
using Game.Dal.Model;
using Game.Dal;

namespace Game.Service
{
    public class ProductRequirementsService : IProductRequirementsService
    {
        private IRepository<ProductRequirements> _productR;
        private IRepository<Users> _user;
        private IRepository<Products> _product;
        private IRepository<UserProducts> _userProduct;
        private IRepository<Buildings> _building;
        private IRepository<UserBuildings> _userBuilding;

        private IUnitOfWork _unitOfWork;

        public ProductRequirementsService(IRepository<ProductRequirements> productR,
            IRepository<Users> user,
            IRepository<Products> product,
            IRepository<UserProducts> userProduct,
            IRepository<UserBuildings> userBuilding,
            IRepository<Buildings> building,
            IUnitOfWork unitOfWork)
        {
            _productR = productR;
            _user = user;
            _product = product;
            _userProduct = userProduct;
            _userBuilding = userBuilding;
            _building = building;
            _unitOfWork = unitOfWork;
        }

        public List<ProductRequirementDto> GetCanUserProducts(string user)
        {
            int uID = _user.GetAll().First(i => i.Login == user).ID;

            List<ProductRequirementDto> productRDto = new List<ProductRequirementDto>();
            ProductRequirementDto productDictionary = new ProductRequirementDto();
            productDictionary.RequireProduct = new List<Dictionary<string, int[]>>();
            bool temp = true;
            bool temp2 = false;
            bool can = false;
            string building = String.Empty;

            var products = _productR.GetAll().GroupBy(i => i.Base_ID);
            foreach (var item in products)
            {
                foreach (var item2 in item)
                {
                    if ((_userProduct.GetAll().First(i => i.User_ID == uID && i.Product_ID == item2.Require_ID).Value < item2.Value)
                        || (!_userProduct.GetAll().Any(i => i.User_ID == uID && i.Product_ID == item2.Require_ID)))
                    {
                        temp = false;
                    }
                    var buildingID = _building.GetAll().FirstOrDefault(p => p.Product_ID == item2.Base_ID).ID;
                    if (_userBuilding.GetAll().Any(i => i.User_ID == uID && i.Building_ID == buildingID && i.Status.Contains("gotowy")))
                    {
                        temp2 = true;
                    }

                    building = _building.Get(buildingID).Alias;
                }
                if (temp && temp2)
                {
                    can = true;
                }
                productRDto.Add(new ProductRequirementDto
                {
                    Base_Name = _product.Get(item.Key).Alias,
                    RequireProduct = item.Select(i => new Dictionary<string, int[]>() { { _product.Get(i.Require_ID).Alias, new int[] { i.Value, i.Require_ID } }, }).ToList(),
                    IfCanProduct = can,
                    RequireBuilding = building
                });
            }

            return productRDto;
        }

        public List<ProductRequirementDto> GetProducts()
        {
            List<ProductRequirementDto> productDto = new List<ProductRequirementDto>();

            try
            {
                foreach (var item in _productR.GetAll())
                {
                    productDto.Add(new ProductRequirementDto
                    {
                        ID = item.ID,
                        Base_ID = item.Base_ID,
                        Base_Name = _product.Get(item.Base_ID).Alias,
                        Require_ID = item.Require_ID,
                        Require_Name = _product.Get(item.Require_ID).Alias,
                        Value = item.Value
                    });
                }
            }
            catch (Exception)
            {
            }

            return productDto;
        }

        public void AddProduct(ProductRequirementDto productDto)
        {
            _productR.Add(new ProductRequirements
            {
                Base_ID = _product.GetAll().First(i => i.Alias == productDto.Base_Name).ID,
                Require_ID = _product.GetAll().First(i => i.Alias == productDto.Require_Name).ID,
                Value = productDto.Value
            });

            _unitOfWork.Commit();
        }

        public void UpdateProduct(ProductRequirementDto productDto)
        {
            foreach (var item in _productR.GetAll().Where(i => i.ID == productDto.ID))
            {
                item.Base_ID = productDto.Base_ID;
                item.Require_ID = productDto.Require_ID;
                item.Value = productDto.Value;
            }

            _unitOfWork.Commit();
        }

        public void DeleteProduct(int id)
        {
            _productR.Delete(_productR.Get(id));

            _unitOfWork.Commit();
        }
    }
}
