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
            if (offer.TypeOffer)
            {
                int userID = _user.GetAll().First(i => i.Login == offer.Login).ID;
                int totalPrice = (offer.Price * offer.Number);
                string productName = _product.GetAll().First(i => i.Alias == offer.Product_Name).Name;
                int ProductID = _userProduct.GetAll().First(i => i.User_ID == userID && i.Product_Name == productName).Product_ID;
                int userProductValue = _userProduct.GetAll().First(i => i.User_ID == userID && i.Product_ID == ProductID).Value;

                if (offer.Number > 0
                    && userProductValue >= offer.Number
                    && _product.GetAll().Any(i => i.Alias == offer.Product_Name)
                    && offer.Price > 0
                    && userProductValue >= offer.Number)
                {
                    _market.Add(new Market
                    {
                        User_ID = _user.GetAll().First(i => i.Login == offer.Login).ID,
                        Product_ID = ProductID,
                        Number = offer.Number,
                        Price = offer.Price,
                        TypeOffer = offer.TypeOffer
                    });


                    if ((_userProduct.GetAll().First(i => i.User_ID == userID && i.Product_ID == ProductID).Value - offer.Number) == 0)
                    {
                        int upID = _userProduct.GetAll().First(i => i.User_ID == userID && i.Product_ID == ProductID).ID;
                        _userProduct.Delete(_userProduct.Get(upID));
                    }
                    else
                    {
                        _userProduct.GetAll().First(i => i.User_ID == userID && i.Product_ID == ProductID).Value -= offer.Number;
                    }
                    _unitOfWork.Commit();

                    return true;
                }

                return false;
            }
            else
            {
                _market.Add(new Market
                {
                    User_ID = _user.GetAll().First(i => i.Login == offer.Login).ID,
                    Product_ID = _product.GetAll().First(i => i.Alias == offer.Product_Name).ID,
                    Number = offer.Number,
                    Price = offer.Price,
                    TypeOffer = offer.TypeOffer
                });

                _unitOfWork.Commit();
                return true;
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
                        Login = _user.GetAll().First(i => i.ID == item.User_ID).Login,
                        Product_ID = item.Product_ID,
                        Product_Name = _product.GetAll().First(i => i.ID == item.Product_ID).Alias,
                        Number = item.Number,
                        Price = item.Price,
                        TypeOffer = item.TypeOffer
                    });
                }
                catch (Exception)
                {
                }
            }
            return marketDto;
        }

        public List<MarketDto> GetSellOffer()
        {
            List<MarketDto> marketDto = new List<MarketDto>();
            foreach (var item in _market.GetAll().Where(i => i.TypeOffer == true))
            {
                try
                {
                    marketDto.Add(new MarketDto
                    {
                        ID = item.ID,
                        User_ID = item.User_ID,
                        Login = _user.GetAll().First(i => i.ID == item.User_ID).Login,
                        Product_ID = item.Product_ID,
                        Product_Name = _product.GetAll().First(i => i.ID == item.Product_ID).Alias,
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

        public List<MarketDto> GetBuyOffer()
        {
            List<MarketDto> marketDto = new List<MarketDto>();
            foreach (var item in _market.GetAll().Where(i => i.TypeOffer == false))
            {
                try
                {
                    marketDto.Add(new MarketDto
                    {
                        ID = item.ID,
                        User_ID = item.User_ID,
                        Login = _user.GetAll().First(i => i.ID == item.User_ID).Login,
                        Product_ID = item.Product_ID,
                        Product_Name = _product.GetAll().First(i => i.ID == item.Product_ID).Alias,
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


        public bool Add(MarketDto market)
        {
            try
            {
                int userID = _user.GetAll().First(i => i.Login == market.Login).ID;
                int totalPrice = (market.Price * market.Number);
                string productName = _product.GetAll().First(i => i.Alias == market.Product_Name).Name;
                int ProductID = _userProduct.GetAll().First(i => i.User_ID == userID && i.Product_Name == productName).Product_ID;
                int userProductValue = _userProduct.GetAll().First(i => i.User_ID == userID && i.Product_ID == ProductID).Value;

                if (market.Number > 0
                    && userProductValue >= market.Number
                    && _product.GetAll().Any(i => i.Alias == market.Product_Name)
                    && market.Price > 0
                    && userProductValue >= market.Number)
                {
                    _market.Add(new Market
                    {
                        User_ID = _user.GetAll().First(i => i.Login == market.Login).ID,
                        Product_ID = _product.GetAll().First(i => i.Alias == market.Product_Name).ID,
                        Number = market.Number,
                        Price = market.Price,
                        TypeOffer = market.TypeOffer
                    });

                    _unitOfWork.Commit();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }

            return false;
        }

        public bool Delete(int id)
        {
            int uID = _market.Get(id).User_ID;
            int productID = _market.Get(id).Product_ID;
            int value = _market.Get(id).Number;

            if (_market.Get(id).TypeOffer == true)
            {
                if (_userProduct.GetAll().Any(i => i.User_ID == uID && i.Product_ID == productID))
                {
                    _userProduct.GetAll().First(i => i.User_ID == uID && i.Product_ID == productID).Value += value;
                }
                else
                {
                    _userProduct.Add(new UserProducts
                    {
                        Product_ID = productID,
                        User_ID = uID,
                        Value = value,
                        Product_Name = _product.Get(productID).Name,
                    });
                }
            }
            _market.Delete(_market.Get(id));
            _unitOfWork.Commit();
            return true;
        }



        public bool Update(MarketDto market)
        {
            int userID = _user.GetAll().First(i => i.Login == market.Login).ID;
            int totalPrice = (market.Price * market.Number);
            string productName = _product.GetAll().First(i => i.Alias == market.Product_Name).Name;
            int ProductID = _userProduct.GetAll().First(i => i.User_ID == userID && i.Product_Name == productName).Product_ID;
            int userProductValue = _userProduct.GetAll().First(i => i.User_ID == userID && i.Product_ID == ProductID).Value;

            if (market.Number > 0
                && userProductValue >= market.Number
                && _product.GetAll().Any(i => i.Alias == market.Product_Name)
                && market.Price > 0
                && userProductValue >= market.Number)
            {
                foreach (var item in _market.GetAll().Where(i => i.ID == market.ID))
                {
                    item.User_ID = _user.GetAll().First(i => i.Login == market.Login).ID;
                    item.Product_ID = _product.GetAll().First(i => i.Alias == market.Product_Name).ID;
                    item.Number = market.Number;
                    item.Price = market.Number;
                    item.TypeOffer = market.TypeOffer;
                }

                _unitOfWork.Commit();

                return true;
            }
            return false;
        }

        public bool BuyOffer(MarketDto market, string User)
        {
            if (market.TypeOffer)
            {
                int uID = _user.GetAll().First(u => u.Login == User).ID;
                int totalPrice = (market.Price * market.Number);
                int userDolar = _dolar.GetAll().First(u => u.User_ID == uID).Value;
                bool succes = false; // Czy user już ma taki produkt
                var m = _market.Get(market.ID);

                if (userDolar >= totalPrice && _market.Get(market.ID).Number >= market.Number)
                {
                    foreach (var item in _userProduct.GetAll().Where(i => i.User_ID == uID && i.Product_ID == market.Product_ID))
                    {
                        if (item.Product_ID == market.Product_ID)
                        {
                            succes = true;
                        }
                    }

                    if (succes)
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
                            Product_Name = _product.GetAll().First(n => n.ID == market.Product_ID).Name,
                            Value = market.Number
                        });
                        _dolar.GetAll().First(i => i.User_ID == uID).Value -= totalPrice;
                        _unitOfWork.Commit();
                    }

                    _dolar.GetAll().First(i => i.User_ID == market.User_ID).Value += totalPrice;

                    if (_market.Get(market.ID).Number == market.Number)
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
            }
            else
            {
                int uID = _user.GetAll().First(i => i.Login == User).ID;
                int suID = _market.Get(market.ID).User_ID;
                int suserProductValue = _userProduct.GetAll().First(i => i.User_ID == uID && i.Product_ID == market.Product_ID).Value;
                int totalPrice = (market.Price * market.Number);

                if (suserProductValue >= market.Number)
                {
                    if (_userProduct.GetAll().Any(i => i.User_ID == suID && i.Product_ID == market.Product_ID))
                    {
                        _userProduct.GetAll().First(i => i.User_ID == suID && i.Product_ID == market.Product_ID).Value += market.Number;
                        _dolar.GetAll().First(i => i.User_ID == suID).Value -= totalPrice;

                        _userProduct.GetAll().First(i => i.User_ID == uID && i.Product_ID == market.Product_ID).Value -= market.Number;
                        _dolar.GetAll().First(i => i.User_ID == uID).Value += totalPrice;

                        if (market.Number == _market.Get(market.ID).Number)
                        {
                            _market.Delete(_market.Get(market.ID));
                            _unitOfWork.Commit();
                        }
                        else
                        {
                            _market.Get(market.ID).Number -= market.Number;
                        }

                        _unitOfWork.Commit();
                        return true;
                    }
                    else
                    {
                        _userProduct.Add(new UserProducts
                        {
                            Product_ID = market.Product_ID,
                            Product_Name = _product.Get(market.Product_ID).Name,
                            User_ID = suID,
                            Value = market.Number
                        });
                        _dolar.GetAll().First(i => i.User_ID == suID).Value -= totalPrice;

                        _userProduct.GetAll().First(i => i.User_ID == uID && i.Product_ID == market.Product_ID).Value -= market.Number;
                        _dolar.GetAll().First(i => i.User_ID == uID).Value += totalPrice;

                        if (market.Number == _market.Get(market.ID).Number)
                        {
                            _market.Delete(_market.Get(market.ID));
                            _unitOfWork.Commit();
                        }

                        _unitOfWork.Commit();
                        return true;
                    }
                }
            }
            return false;
        }

        public bool SellProduct(int productID, int value, string user)
        {
            int uID = _user.GetAll().First(i => i.Login == user).ID;
            int money = _product.Get(productID).Price_per_unit * value;

            if (_userProduct.GetAll().First(i => i.Product_ID == productID && i.User_ID == uID).Value >= value)
            {
                int upID = _userProduct.GetAll().First(i => i.Product_ID == productID && i.User_ID == uID).ID;
                if ((_userProduct.GetAll().First(i => i.Product_ID == productID && i.User_ID == uID).Value - value) > 0)
                {
                    _userProduct.GetAll().First(i => i.Product_ID == productID && i.User_ID == uID).Value -= value;
                }
                else
                {
                    _userProduct.Delete(_userProduct.Get(upID));
                }
                _dolar.GetAll().First(i => i.User_ID == uID).Value += money;

                _unitOfWork.Commit();

                return true;
            }

            return false;
        }


    }
}
