using Game.Core.DTO;
using Game.GUI.ViewModels;
using Game.GUI.ViewModels.Market;
using Game.Service.Interfaces;
using Game.Service.Interfaces.TableInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Game.GUI.Controllers
{
    public class MarketController : Controller
    {
        private IMarketService _marketService;
        private IProductService _productService;
        private IUserProductService _userProductService;

        public MarketController(IMarketService marketService, IProductService productServise, IUserProductService userProductService)
        {
            _marketService = marketService;
            _productService = productServise;
            _userProductService = userProductService;
        }

        // GET: Market
        public ActionResult Index()
        {
            return View(GetOfferList());
        }


        public ListMarketViewModel GetOfferList()
        {
            ListMarketViewModel marketList = new ListMarketViewModel();
            marketList.marketList = new List<MarketViewModel>();

            foreach (var item in _marketService.GetOffer())
            {
                marketList.marketList.Add(new MarketViewModel
                {
                    ID = item.ID,
                    Login = item.Login,
                    User_ID = item.User_ID,
                    Product_ID = item.Product_ID,
                    Product_Name = item.Product_Name,
                    Number = item.Number,
                    Price = item.Price
                });
            }
            var prod = _userProductService.GetUserProductList(User.Identity.Name);
            var opts = prod.ToList<UserProductDto>();
            var optsString = new List<string>();
            foreach(var o in opts)
            {
                if (!optsString.Contains(o.Product_Name))
                {
                    optsString.Add(o.Product_Name);
                }
            }

            marketList.options = optsString.ToArray();

            return marketList;
        }




        [HttpPost]
        public ActionResult AddOffer(ListMarketViewModel marketList)
        {
            MarketDto _marketDto = new MarketDto();

            _marketDto.Login = User.Identity.Name;
            _marketDto.Product_Name = marketList.marketView.Product_Name;
            _marketDto.Number = marketList.marketView.Number;
            _marketDto.Price = marketList.marketView.Price;

            _marketService.AddOffer(_marketDto);

            return View("~/Views/Market/Index.cshtml", GetOfferList());
        }

        [HttpPost]
        public JsonResult BuyOffer(MarketViewModel a)
        {
            MarketDto marketDto = new MarketDto();

            marketDto.ID = a.ID;
            marketDto.Number = a.Number;
            marketDto.Price = a.Price;
            marketDto.Product_ID = a.Product_ID;
            marketDto.User_ID = a.User_ID;

            if (_marketService.BuyOffer(marketDto, User.Identity.Name))
            {
                return new JsonResult { Data = true };
            }
            else
            {
                return new JsonResult { Data = false };
            }
        }


        public ActionResult _MarketList()
        {
            ListMarketViewModel marketList = new ListMarketViewModel();
            marketList.marketList = new List<MarketViewModel>();

            foreach (var item in _marketService.GetOffer())
            {
                marketList.marketList.Add(new MarketViewModel
                {
                    ID = item.ID,
                    Login = item.Login,
                    Product_Name = item.Product_Name,
                    Number = item.Number,
                    Price = item.Price
                });
            }
            return View("~/Views/Admin/_MarketList.cshtml", marketList);
        }



        [HttpPost]
        public ActionResult Add(ListTableViewModel listView)
        {
            MarketDto _marketDto = new MarketDto();

            _marketDto.Login = listView.tableView.Login;
            _marketDto.Product_Name = listView.tableView.Product_Name;
            _marketDto.Number = listView.tableView.Number;
            _marketDto.Price = listView.tableView.Price;

            _marketService.Add(_marketDto);

            return View("~/Views/Admin/Admin.cshtml");
        }

        [HttpPost]
        public ActionResult Update(ListTableViewModel listView)
        {
            MarketDto _marketDto = new MarketDto();

            _marketDto.Login = listView.tableView.Login;
            _marketDto.Product_Name = listView.tableView.Product_Name;
            _marketDto.Number = listView.tableView.Number;
            _marketDto.Price = listView.tableView.Price;

            _marketService.Update(_marketDto);

            return View("~/Views/Admin/Admin.cshtml");
        }


        [HttpGet]
        public ActionResult DeleteOffer(int id)
        {
            _marketService.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult DeleteOfferAdmin(int id)
        {
            _marketService.Delete(id);
            return View("~/Views/Admin/Admin.cshtml");
        }

    }
}