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


        public void Add(MarketDto user)
        {
            _market.Add(new Market
            {
                User_ID = _user.Get(user.ID).ID,
                Product_ID = _product.Get(user.Product_ID).ID,
                Number = user.Number,
                Price = user.Price
            });

            _unitOfWork.Commit();
        }

        public void Delete(int id)
        {
            _user.Delete(_user.Get(id));
            _unitOfWork.Commit();
        }

        

        public void Update(MarketDto market, int id)
        {
            throw new NotImplementedException();
        }

        public bool BuyOffer(MarketDto market, string User)
        {
            int uID = _user.GetAll().First(u => u.Login == User).ID;
            int totalPrice = (market.Price * market.Number);
            int userDolar = _dolar.GetAll().First(u => u.User_ID == uID).Value;
            bool succes = false; // Czy user już ma taki produkt


            if(userDolar>=totalPrice)
            {
                foreach (var item in _userProduct.GetAll().Where(i => i.User_ID == uID && i.Product_ID == market.Product_ID))
                {
                    if (item.Product_ID == market.Product_ID)
                    {
                        succes = true;
                    }
                }

                if(succes)
                {
                    _userProduct.GetAll().First(i => i.User_ID == uID && i.Product_ID == market.Product_ID).Value += market.Number;
                    _dolar.GetAll().First(i => i.User_ID == uID).Value -= totalPrice;
                    _unitOfWork.Commit();
                }
                else
                {
                    _userProduct.Add(new UserProducts
                    {
                        User_ID = uID,
                        Product_ID = market.Product_ID,
                        Product_Name = _product.GetAll().First(n=> n.ID == market.Product_ID).Name,
                        Value = market.Number
                    });
                    _dolar.GetAll().First(i => i.User_ID == uID).Value -= totalPrice;
                    _unitOfWork.Commit();
                }

                _dolar.GetAll().First(i => i.User_ID == market.User_ID).Value += totalPrice;

                if(_market.Get(market.ID).Number == market.Number)
                {
                    _market.Delete(_market.Get(market.ID));
                }
                else
                {
                    _market.Get(market.ID).Number -= market.Number;
                }
                _unitOfWork.Commit();

                return true;
            }
            return false;
        }
    }
}
