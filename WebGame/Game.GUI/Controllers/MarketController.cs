using Game.Core.DTO;
using Game.GUI.ViewModels;
using Game.GUI.ViewModels.Market;
using Game.Service.Interfaces;
using Game.Service.Interfaces.TableInterface;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;

namespace Game.GUI.Controllers
{
    public class MarketController : Controller
    {
        private IMarketService _marketService;
        private IProductService _productService;
        private IUserProductService _userProductService;
        private ITutorialService _tutorialService;

        public MarketController(IMarketService marketService, IProductService productServise, IUserProductService userProductService, ITutorialService tutroialService)
        {
            _marketService = marketService;
            _productService = productServise;
            _userProductService = userProductService;
            _tutorialService = tutroialService;
        }

        // GET: Market
        [Authorize]
        public ActionResult Index()
        {
            MarketViewModel marketModel = new MarketViewModel();
            marketModel.viewModel = new ItemMarketViewModel();
            marketModel.viewModel.allDiv = _tutorialService.tutorialUser(User.Identity.Name).allDiv;
            marketModel.viewModel.marketDiv = _tutorialService.tutorialUser(User.Identity.Name).marketDiv;

            return View(marketModel);
        }

        //[Authorize]
        //public ActionResult _SellOffer(int? Page_No)
        //{
        //    MarketViewModel marketList = new MarketViewModel();
        //    int Size_Of_Page = 10;
        //    int No_Of_Page = (Page_No ?? 1);

        //    marketList.pagedList = _marketService.GetSellOffer().Select(x => new ItemMarketViewModel
        //    {
        //        ID = x.ID,
        //        User_Login = x.Login,
        //        User_ID = x.User_ID,
        //        Product_ID = x.Product_ID,
        //        Product_Name = x.Product_Name,
        //        Number = x.Number,
        //        Price = x.Price,
        //        TypeOffer = x.TypeOffer
        //    }).ToList().ToPagedList(No_Of_Page, Size_Of_Page);

        //    marketList.userProduct = _userProductService.GetUserProductList(User.Identity.Name).OrderBy(x => x.Alias).Select(i => i.Product_Name).ToArray();
        //    marketList.allProduct = _productService.GetProduct().OrderBy(x => x.Alias).Select(i => i.Alias).ToArray();

        //    return View("~/Views/Market/_SellOffer.cshtml", marketList);
        //}

        [Authorize]
        public ActionResult _SellOffer(int? Page_No, string productName, int? minPrice, int? maxPrice, bool? myOffer, int? columnName, int? sortOrder)
        {
            MarketViewModel marketList = new MarketViewModel();
            List<ItemMarketViewModel> tempList = new List<ItemMarketViewModel>();

            int Size_Of_Page = 10;
            int No_Of_Page = (Page_No ?? 1);
            if (minPrice == null)
            {
                minPrice = 0;
            }
            if (maxPrice == null)
            {
                maxPrice = Int32.MaxValue;
            }

            if (myOffer == true)
            {
                foreach (var x in _marketService.GetSellOffer().Where(i => i.Product_Name.Contains(productName) && i.Price >= minPrice && i.Price <= maxPrice && i.Login == User.Identity.Name))
                {
                    tempList.Add(new ItemMarketViewModel
                    {
                        ID = x.ID,
                        User_Login = x.Login,
                        User_ID = x.User_ID,
                        Product_ID = x.Product_ID,
                        Product_Name = x.Product_Name,
                        Number = x.Number,
                        Price = x.Price,
                        TypeOffer = x.TypeOffer
                    });

                }

            }
            else
            {

                foreach (var x in _marketService.GetSellOffer().Where(i => i.Product_Name.Contains(productName) && i.Price >= minPrice && i.Price <= maxPrice))
                {
                    tempList.Add(new ItemMarketViewModel
                    {
                        ID = x.ID,
                        User_Login = x.Login,
                        User_ID = x.User_ID,
                        Product_ID = x.Product_ID,
                        Product_Name = x.Product_Name,
                        Number = x.Number,
                        Price = x.Price,
                        TypeOffer = x.TypeOffer
                    });

                }

            }
            marketList.userProduct = _userProductService.GetUserProductList(User.Identity.Name).OrderBy(x => x.Alias).Select(i => i.Product_Name).ToArray();
            marketList.allProduct = _productService.GetProduct().OrderBy(x => x.Alias).Select(i => i.Alias).ToArray();


            //Sort
            //1.ID
            //2.User
            //3.Product
            //4.Number
            //5.Price

            if (sortOrder == 0 || sortOrder == null)
            {
                marketList.pagedList = tempList.OrderByDescending(i => i.ID).ToList().ToPagedList(No_Of_Page, Size_Of_Page);
            }
            else if (sortOrder == 1)
            {
                switch (columnName)
                {
                    case 2:
                        marketList.pagedList = tempList.OrderByDescending(i => i.User_Login).ToList().ToPagedList(No_Of_Page, Size_Of_Page);
                        break;
                    case 3:
                        marketList.pagedList = tempList.OrderByDescending(i => i.Product_Name).ToList().ToPagedList(No_Of_Page, Size_Of_Page);
                        break;
                    case 4:
                        marketList.pagedList = tempList.OrderByDescending(i => i.Number).ToList().ToPagedList(No_Of_Page, Size_Of_Page);
                        break;
                    case 5:
                        marketList.pagedList = tempList.OrderByDescending(i => i.Price).ToList().ToPagedList(No_Of_Page, Size_Of_Page);
                        break;
                    default:
                        break;
                }
            }
            else if (sortOrder == 2)
            {
                switch (columnName)
                {
                    case 2:
                        marketList.pagedList = tempList.OrderBy(i => i.User_Login).ToList().ToPagedList(No_Of_Page, Size_Of_Page);
                        break;
                    case 3:
                        marketList.pagedList = tempList.OrderBy(i => i.Product_Name).ToList().ToPagedList(No_Of_Page, Size_Of_Page);
                        break;
                    case 4:
                        marketList.pagedList = tempList.OrderBy(i => i.Number).ToList().ToPagedList(No_Of_Page, Size_Of_Page);
                        break;
                    case 5:
                        marketList.pagedList = tempList.OrderBy(i => i.Price).ToList().ToPagedList(No_Of_Page, Size_Of_Page);
                        break;
                    default:
                        break;
                }
            }

            return View("~/Views/Market/_SellOffer.cshtml", marketList);
        }

        [Authorize]
        public ActionResult _BuyOffer(int? Page_No, string productName, int? minPrice, int? maxPrice, bool? myOffer, int? columnName, int? sortOrder)
        {
            MarketViewModel marketList = new MarketViewModel();
            List<ItemMarketViewModel> tempList = new List<ItemMarketViewModel>();
            int Size_Of_Page = 10;
            int No_Of_Page = (Page_No ?? 1);
            if (minPrice == null)
            {
                minPrice = 0;
            }
            if (maxPrice == null)
            {
                maxPrice = Int32.MaxValue;
            }

            if (myOffer == true)
            {
                foreach (var x in _marketService.GetBuyOffer().Where(i => i.Product_Name.Contains(productName) && i.Price >= minPrice && i.Price <= maxPrice && i.Login == User.Identity.Name))
                {
                    tempList.Add(new ItemMarketViewModel
                    {
                        ID = x.ID,
                        User_Login = x.Login,
                        User_ID = x.User_ID,
                        Product_ID = x.Product_ID,
                        Product_Name = x.Product_Name,
                        Number = x.Number,
                        Price = x.Price,
                        TypeOffer = x.TypeOffer
                    });
                }
            }
            else
            {
                foreach (var x in _marketService.GetBuyOffer().Where(i => i.Product_Name.Contains(productName) && i.Price >= minPrice && i.Price <= maxPrice))
                {
                    tempList.Add(new ItemMarketViewModel
                    {
                        ID = x.ID,
                        User_Login = x.Login,
                        User_ID = x.User_ID,
                        Product_ID = x.Product_ID,
                        Product_Name = x.Product_Name,
                        Number = x.Number,
                        Price = x.Price,
                        TypeOffer = x.TypeOffer
                    });
                }
            }
            marketList.allProduct = _productService.GetProduct().OrderBy(x => x.Alias).Select(i => i.Alias).ToArray();

            //Sort
            //1.ID
            //2.User
            //3.Product
            //4.Number
            //5.Price

            if (sortOrder == 0 || sortOrder == null)
            {
                marketList.pagedList = tempList.OrderByDescending(i => i.ID).ToList().ToPagedList(No_Of_Page, Size_Of_Page);
            }
            else if (sortOrder == 1)
            {
                switch (columnName)
                {
                    case 2:
                        marketList.pagedList = tempList.OrderByDescending(i => i.User_Login).ToList().ToPagedList(No_Of_Page, Size_Of_Page);
                        break;
                    case 3:
                        marketList.pagedList = tempList.OrderByDescending(i => i.Product_Name).ToList().ToPagedList(No_Of_Page, Size_Of_Page);
                        break;
                    case 4:
                        marketList.pagedList = tempList.OrderByDescending(i => i.Number).ToList().ToPagedList(No_Of_Page, Size_Of_Page);
                        break;
                    case 5:
                        marketList.pagedList = tempList.OrderByDescending(i => i.Price).ToList().ToPagedList(No_Of_Page, Size_Of_Page);
                        break;
                    default:
                        break;
                }
            }
            else if (sortOrder == 2)
            {
                switch (columnName)
                {
                    case 2:
                        marketList.pagedList = tempList.OrderBy(i => i.User_Login).ToList().ToPagedList(No_Of_Page, Size_Of_Page);
                        break;
                    case 3:
                        marketList.pagedList = tempList.OrderBy(i => i.Product_Name).ToList().ToPagedList(No_Of_Page, Size_Of_Page);
                        break;
                    case 4:
                        marketList.pagedList = tempList.OrderBy(i => i.Number).ToList().ToPagedList(No_Of_Page, Size_Of_Page);
                        break;
                    case 5:
                        marketList.pagedList = tempList.OrderBy(i => i.Price).ToList().ToPagedList(No_Of_Page, Size_Of_Page);
                        break;
                    default:
                        break;
                }
            }



            return View("~/Views/Market/_BuyOffer.cshtml", marketList);
        }

        [Authorize]
        public ActionResult _SystemOffer()
        {
            MarketViewModel marketList = new MarketViewModel();

            marketList.systemOfferList = _userProductService.GetUserProductList(User.Identity.Name).Select(x => new ItemMarketViewModel
            {
                Product_ID = x.Product_ID,
                Product_Name = x.Product_Name,
                Price = x.Price
            }).ToList();

            return View(marketList);
        }

        [HttpPost]
        [Authorize]
        public ActionResult SellProduct(int productid, int valueproduct, int money, string name)
        {
            List<string> errors;
            if (Session["val"] != null)
            {
                errors = ((string[])Session["val"]).ToList();
            }
            else
            {
                errors = new List<string>();
            }
            if (valueproduct <= 0)
            {
                errors.Add("Coś poszło nie tak.");
            }
            else if (_marketService.SellProduct(productid, valueproduct, User.Identity.Name))
            {
                errors.Add("Sukces!");
                errors.Add("+$" + money);
                errors.Add(name + " -" + valueproduct);
            }
            else
            {
                errors.Add("Nie posiadasz tyle produktów");
            }
            Session["val"] = errors.ToArray<string>();

            return View("~/Views/Market/Index.cshtml");
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddOffer(MarketViewModel marketList)
        {
            List<string> errors;
            if (Session["val"] != null)
            {
                errors = ((string[])Session["val"]).ToList();
            }
            else
            {
                errors = new List<string>();
            }

            if ((marketList.viewModel.Number == 0) || marketList.viewModel.Price == 0)
            {
                errors.Add("Coś poszło nie tak. Spróbuj ponownie.");
                Session["val"] = errors.ToArray<string>();
            }
            else {
                MarketDto _marketDto = new MarketDto();

                _marketDto.Login = User.Identity.Name;
                _marketDto.Product_Name = marketList.viewModel.Product_Name;
                _marketDto.Number = marketList.viewModel.Number;
                _marketDto.Price = marketList.viewModel.Price;
                _marketDto.TypeOffer = marketList.viewModel.TypeOffer;

                if (!_marketService.AddOffer(_marketDto))
                {
                    errors.Add("Coś poszło nie tak. Spróbuj ponownie.");
                    Session["val"] = errors.ToArray<string>();
                }
            }
            return RedirectToAction("Index");

        }

        [HttpPost]
        [Authorize]
        public JsonResult BuyOffer(ItemMarketViewModel a)
        {
            MarketDto marketDto = new MarketDto();


            marketDto.ID = a.ID;
            marketDto.Number = a.Number;
            marketDto.Price = a.Price;
            marketDto.Product_ID = a.Product_ID;
            marketDto.User_ID = a.User_ID;
            marketDto.TypeOffer = a.TypeOffer;

            if (_marketService.BuyOffer(marketDto, User.Identity.Name))
            {
                return new JsonResult { Data = false };
            }
            else
            {
                return new JsonResult { Data = false };
            }
        }



        [HttpGet]
        [Authorize]
        public ActionResult DeleteOffer(int id)
        {
            _marketService.Delete(id);
            return RedirectToAction("Index");
        }


    }
}