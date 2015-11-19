using Game.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Core.DTO;
using Game.Dal;
using Game.Dal.Repository;
using Game.Dal.Model;

namespace Game.Service
{
    public class MarketService : IMarketService
    {
        private IRepository<Market> _market;
        private IRepository<Users> _user;
        private IRepository<Products> _product;
        private IRepository<Dolars> _dolar;
        private IRepository<UserProducts> _userProduct;


        private IUnitOfWork _unitOfWork;

        public MarketService(IUnitOfWork unitOfWork, 
            IRepository<Market> market,
            IRepository<Users> user,
            IRepository<Products> product,
            IRepository<Dolars> dolar,
            IRepository<UserProducts> userProduct)
        {
            _market = market;
            _user = user;
            _product = product;
            _dolar = dolar;
            _userProduct = userProduct;
            _unitOfWork = unitOfWork;
        }

        public void AddOffer(MarketDto offer)
        {
            int userID = _user.GetAll().First(i => i.Login == offer.Login).ID;
            int totalPrice = (offer.Price * offer.Number);
            var temp = _userProduct.GetAll().First(i => i.User_ID == userID && i.Product_Name == offer.Product_Name);
            int ProductID = _userProduct.GetAll().First(i => i.User_ID == userID && i.Product_Name == offer.Product_Name).Product_ID;
            int userProductValue = _userProduct.GetAll().First(i => i.User_ID == userID && i.Product_ID == ProductID).Value;

            if (userProductValue>=offer.Number)
            {
                _market.Add(new Market
                {
                    User_ID = _user.GetAll().First(i => i.Login == offer.Login).ID,
                    Product_ID = ProductID,
                    Number = offer.Number,
                    Price = offer.Price
                });

                _userProduct.GetAll().First(i => i.User_ID == userID && i.Product_ID==ProductID).Value -= offer.Number;

                _unitOfWork.Commit();
            }
        }

        public List<MarketDto> GetOffer()
        {
            List<MarketDto> marketDto = new List<MarketDto>();
            foreach (var item in _market.GetAll())
            {
                try
                {
                    marketDto.Add(new MarketDto
                    {
                        ID = item.ID,
                        User_ID = item.User_ID,
                        Login = _user.GetAll().First(i=> i.ID == item.User_ID).Login,
                        Product_ID = item.Product_ID,
                        Product_Name = _product.GetAll().First(i=> i.ID == item.Product_ID).Alias, 
                        Number = item.Number,
                        Price = item.Price
                    });
                }
                catch (Exception)
                {
                }
            }
            return marketDto;
        }
    }
}
