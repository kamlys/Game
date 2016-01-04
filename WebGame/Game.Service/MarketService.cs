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

        public bool AddOffer(MarketDto offer)
        {
            int userID = _user.GetAll().First(i => i.Login == offer.Login).ID;
            int totalPrice = (offer.Price * offer.Number);
            var temp = _userProduct.GetAll().First(i => i.User_ID == userID && i.Product_Name == offer.Product_Name);
            int ProductID = _userProduct.GetAll().First(i => i.User_ID == userID && i.Product_Name == offer.Product_Name).Product_ID;
            int userProductValue = _userProduct.GetAll().First(i => i.User_ID == userID && i.Product_ID == ProductID).Value;

            if (offer.Number > 0
                &&userProductValue>=offer.Number 
                && _product.GetAll().Any(i=> i.Name == offer.Product_Name) 
                && offer.Price >0
                && userProductValue>=offer.Number)
            {
                _market.Add(new Market
                {
                    User_ID = _user.GetAll().First(i => i.Login == offer.Login).ID,
                    Product_ID = _product.GetAll().First(i=> i.Name == offer.Product_Name).ID,
                    Number = offer.Number,
                    Price = offer.Price
                });

                _userProduct.GetAll().First(i => i.User_ID == userID && i.Product_ID==ProductID).Value -= offer.Number;

                _unitOfWork.Commit();


                return true;
            }

            return false;
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


        public void Add(MarketDto market)
        {
            _market.Add(new Market
            {
                User_ID = _user.Get(market.ID).ID,
                Product_ID = _product.GetAll().First(i => i.Name == market.Product_Name).ID,
                Number = market.Number,
                Price = market.Price
            });

            _unitOfWork.Commit();
        }

        public void Delete(int id)
        {
            int uID = _market.Get(id).User_ID;
            int productID = _market.Get(id).Product_ID;
            int value = _market.Get(id).Number;


            _userProduct.GetAll().First(i => i.User_ID == uID && i.Product_ID == productID).Value += value;

            _market.Delete(_market.Get(id));
            _unitOfWork.Commit();
        }

        

        public void Update(MarketDto market)
        {
            foreach (var item in _market.GetAll().Where(i => i.ID == market.ID))
            {
                item.User_ID = _user.GetAll().First(i => i.Login == market.Login).ID;
                item.Product_ID = _product.GetAll().First(i => i.Name == i.Name).ID;
                item.Number = market.Number;
                item.Price = market.Number;
            }

            _unitOfWork.Commit();
        }

        public bool BuyOffer(MarketDto market, string User)
        {
            int uID = _user.GetAll().First(u => u.Login == User).ID;
            int totalPrice = (market.Price * market.Number);
            int userDolar = _dolar.GetAll().First(u => u.User_ID == uID).Value;
            bool succes = false; // Czy user już ma taki produkt
            var m = _market.Get(market.ID);

            if(userDolar>=totalPrice && _market.Get(market.ID).Number>=market.Number)
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

        public bool SellProduct(int productID, int value, string user)
        {
            int uID = _user.GetAll().First(i => i.Login == user).ID;
            int money = _product.Get(productID).Price_per_unit * value;

            if (_userProduct.GetAll().First(i => i.Product_ID == productID && i.User_ID == uID).Value >= value)
            {
                _userProduct.GetAll().First(i => i.Product_ID == productID && i.User_ID == uID).Value -= value;
                _dolar.GetAll().First(i => i.User_ID == uID).Value += money;

                _unitOfWork.Commit();

                return true;
            }

            return false;
        }
    }
}
