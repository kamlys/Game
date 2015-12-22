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
        private INotificationService _notificationService;
        private IDealService _dealService;
        private IProductRequirementsService _productRequirementService;


        public OfficeController(IUserBuildingService userBuildingService,
            IMarketService marketService,
            IUserProductService userProductService,
            INotificationService notificationService,
            IProductRequirementsService productRequirementService,
            IDealService dealService)
        {
            _userBuildingService = userBuildingService;
            _userProductService = userProductService;
            _marketService = marketService;
            _notificationService = notificationService;
            _dealService = dealService;
            _productRequirementService = productRequirementService;
        }


        [Authorize]
        // GET: Office
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult _UserBuildingList()
        {
            ListTableViewModel tableList = new ListTableViewModel();
            tableList.tableList = new List<TableViewModel>();

            foreach (var item in _userBuildingService.GetUserBuildingList(User.Identity.Name))
            {
                tableList.tableList.Add(new TableViewModel
                {
                    ID = item.ID,
                    User_ID = item.User_ID,
                    Login = item.Login,
                    X_pos = item.X_pos,
                    Y_pos = item.Y_pos,
                    Lvl = item.Lvl,
                    Building_ID = item.Building_ID,
                    Name = item.Building_Name,
                    Status = item.Status,
                    Value = item.Produkcja,
                    ifCan = _userBuildingService.ifLvlUp(item.ID, User.Identity.Name),
                    Percent_price_per_lvl = item.PriceLvlUp,
                    Percent_product_per_lvl = item.ProdukcjaLvlUp
                });
            }


            return View(tableList);
        }

        [Authorize]
        public ActionResult _UserProductList()
        {
            ListTableViewModel tableList = new ListTableViewModel();
            tableList.tableList = new List<TableViewModel>();

            foreach (var item in _userProductService.GetUserProductList(User.Identity.Name))
            {
                tableList.tableList.Add(new TableViewModel
                {
                    User_ID = item.User_ID,
                    Login = item.Login,
                    Name = item.Product_Name,
                    Value = item.Value,
                    Product_ID = item.Product_ID
                });
            }


            return View(tableList);
        }

        [Authorize]
        public ActionResult _UserDealList()
        {
            ListTableViewModel tableList = new ListTableViewModel();
            tableList.tableList = new List<TableViewModel>();

            foreach (var item in _dealService.GetUserDeals(User.Identity.Name))
            {
                tableList.tableList.Add(new TableViewModel
                {
                    ID = item.ID,
                    Login = item.User1_Login,
                    User2_Login = item.User2_Login,
                    Percent_User1 = item.Percent_User1,
                    Percent_User2 = item.Percent_User2,
                    Name = item.Building_Name,
                    IsActive = item.IsActive,
                    Owner = item.MyMap

                });
            }
            return View(tableList);
        }

        [Authorize]
        public ActionResult _UserProductRequirementList()
        {
            ListTableViewModel tableList = new ListTableViewModel();
            tableList.tableList = new List<TableViewModel>();
            
            foreach (var item in _productRequirementService.GetCanUserProducts(User.Identity.Name))
            {
                tableList.tableList.Add(new TableViewModel
                {
                    Base_Name = item.Base_Name,
                    RequireProduct = item.RequireProduct,
                    Name = item.RequireBuilding,
                    ifCan = item.IfCanProduct
                });
            }

            return View(tableList);
        }

        [Authorize]
        public JsonResult AcceptDeal(int a, string user)
        {
            var accepted = _dealService.AcceptDeal(a, User.Identity.Name);
            if(accepted)
            {

                NotificationDto notificationDto = new NotificationDto();

                notificationDto.Description = "Umowa zaakceptowana";
                notificationDto.User_Login = user;

                _notificationService.SentNotification(notificationDto);
                return new JsonResult { Data = true };
            }else
            {
                return new JsonResult { Data = false };
            }

        }

        [Authorize]
        public JsonResult CancelDeal(int a, string user)
        {
            _dealService.CancelDeal(a);
            NotificationDto notificationDto = new NotificationDto();

            notificationDto.Description = "Umowa odrzucona";
            notificationDto.User_Login = user;

            _notificationService.SentNotification(notificationDto);

            return new JsonResult { Data = true };
        }

        [Authorize]
        public ActionResult AddDeal(ListTableViewModel tableList)
        {
            DealDto dealDto = new DealDto();

            dealDto.User1_Login = User.Identity.Name;
            dealDto.User2_Login = tableList.tableView.Login;
            dealDto.Building_Name = tableList.tableView.Name;
            if (tableList.tableView.Owner == true)
            {
                dealDto.Map_ID = 1;
            }
            else
            {
                dealDto.Map_ID = 0;
            }

            dealDto.Percent_User1 = tableList.tableView.Percent_User1;

            _dealService.AddDeal(dealDto);

            NotificationDto notificationDto = new NotificationDto();

            notificationDto.Description = "Nowa umowa";
            notificationDto.User_Login = tableList.tableView.Login;

            _notificationService.SentNotification(notificationDto);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddOffer(ListTableViewModel tableList)
        {
            MarketDto _marketDto = new MarketDto();

            _marketDto.Login = User.Identity.Name;
            _marketDto.Product_Name = tableList.tableView.Product_Name;
            _marketDto.Number = tableList.tableView.Number;
            _marketDto.Price = tableList.tableView.Price;

            _marketService.AddOffer(_marketDto);

            return View("~/Views/Office/Index.cshtml");
        }

    }
}