using Game.Core.DTO;
using Game.GUI.ViewModels;
using Game.Service.Interfaces;
using Game.Service.Interfaces.TableInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Game.GUI.Controllers
{
    public class OfficeController : Controller
    {
        private IUserBuildingService _userBuildingService;
        private IUserProductService _userProductService;
        private IMarketService _marketService;


        public OfficeController(IUserBuildingService userBuildingService, 
            IMarketService marketService,
            IUserProductService userProductService)
        {
            _userBuildingService = userBuildingService;
            _userProductService = userProductService;
            _marketService = marketService;
        }

        // GET: Office
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult _UserBuildingList()
        {
            ListTableViewModel tableList = new ListTableViewModel();
            tableList.tableList = new List<TableViewModel>();

            foreach (var item in _userBuildingService.GetUserBuilding())
            {
                tableList.tableList.Add(new TableViewModel
                {
                    User_ID = item.User_ID,
                    Login = item.Login,
                    X_pos = item.X_pos,
                    Y_pos = item.Y_pos,
                    Lvl = item.Lvl,
                    Building_ID = item.Building_ID,
                    Name = item.Building_Name,
                    Status = item.Status,
                    Value = item.Produkcja
                    
                });
            }


            return View(tableList);
        }


        public ActionResult _UserProductList()
        {
            ListTableViewModel tableList = new ListTableViewModel();
            tableList.tableList = new List<TableViewModel>();

            foreach (var item in _userProductService.GetUserProduct())
            {
                tableList.tableList.Add(new TableViewModel
                {
                    ID = item.ID,
                    User_ID = item.User_ID,
                    Login = item.Login,
                    Name = item.Product_Name,
                    Value = item.Value,
                    Product_ID = item.Product_ID
                });
            }


            return View(tableList);
        }

        [HttpGet]
        public ActionResult AddOffer(string name, int number, int price)
        {
            MarketDto _marketDto = new MarketDto();

            _marketDto.Login = User.Identity.Name;
            _marketDto.Product_Name = name;
            _marketDto.Number = number;
            _marketDto.Price = price;

            _marketService.AddOffer(_marketDto);

            return View("~/Views/Office/Index.cshtml");
        }

    }
}