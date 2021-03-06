﻿using Game.Core.DTO;
using Game.GUI.ViewModels.Building.UserBuildings;
using Game.GUI.ViewModels.Deal;
using Game.GUI.ViewModels.Map;
using Game.GUI.ViewModels.Market;
using Game.GUI.ViewModels.Office;
using Game.GUI.ViewModels.Product.ProductRequirement;
using Game.GUI.ViewModels.Product.UserProduct;
using Game.Service.Interfaces;
using Game.Service.Interfaces.TableInterface;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private IBuildingHelper _buildingsHelper;
        private IProductService _productService;
        private IUserService _userService;
        private IMapService _mapService;
        private ITutorialService _tutorialService;


        public OfficeController(IUserBuildingService userBuildingService,
            IMarketService marketService,
            IUserProductService userProductService,
            INotificationService notificationService,
            IProductRequirementsService productRequirementService,
            IBuildingHelper buildingsHelper,
            IProductService productService,
            IMapService mapService,
            IUserService userService,
            IDealService dealService,
            ITutorialService tutorialService)
        {
            _userBuildingService = userBuildingService;
            _userProductService = userProductService;
            _marketService = marketService;
            _notificationService = notificationService;
            _dealService = dealService;
            _buildingsHelper = buildingsHelper;
            _productService = productService;
            _mapService = mapService;
            _userService = userService;
            _productRequirementService = productRequirementService;
            _tutorialService = tutorialService;
        }


        [Authorize]
        // GET: Office
        public ActionResult Index()
        {
            OfficeViewModel officeModel = new OfficeViewModel();
            officeModel.officeDiv = _tutorialService.tutorialUser(User.Identity.Name).officeDiv;
            officeModel.allDiv = _tutorialService.tutorialUser(User.Identity.Name).allDiv;
            return View(officeModel);
        }

        [Authorize]
        public ActionResult _UserProduct()
        {
            _productService.UpdateUserProduct(User.Identity.Name);
            MapViewModel vm = new MapViewModel();
            vm.Map = _mapService.GetMap(User.Identity.Name);
            var ub = _buildingsHelper.GetBuildings(User.Identity.Name);
            List<UserBuildingsViewModel> ubv = new List<UserBuildingsViewModel>();

            foreach (var a in ub)
            {
                if (!a.Owner)
                {
                    continue;
                }
                var building = _buildingsHelper.GetBuildings().Where(b => b.ID == a.Building_ID).First();
                var bTime = 0;
                if (a.Status == "budowa")
                {
                    bTime = building.BuildingTime;
                }
                else if (a.Status == "burzenie")
                {
                    bTime = building.DestructionTime;
                }
                ubv.Add(new UserBuildingsViewModel
                {
                    BuildingID = a.Building_ID,
                    level = a.Lvl,
                    name = building.Name,
                    x_left = a.X_pos,
                    x_right = a.X_pos + building.Width - 1,
                    y_top = a.Y_pos,
                    y_bottom = a.Y_pos + building.Height - 1,
                    ID = a.ID,
                    Status = a.Status,
                    BuildTime = bTime,
                    BuildDone = bTime - _buildingsHelper.BuildingTimeLeft(User.Identity.Name, a.ID),
                    Alias = building.Alias
                });
            }
            vm.UserBuildings = ubv;
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            vm.UserProducts = serializer.Serialize(_buildingsHelper.AddProductValue(User.Identity.Name));
            var abc = _buildingsHelper.ProductNames(User.Identity.Name);
            var names = serializer.Serialize(abc);
            vm.ProductNames = names;
            var buildingsArray = new int[ub.Count][];
            int i = 0;
            foreach (var a in ubv)
            {
                var building = new int[4] { a.x_left, a.x_right, a.y_top, a.y_bottom };
                buildingsArray[i] = building;
                i++;
            }
            if (buildingsArray != null)
                vm.BuildingsArray = serializer.Serialize(buildingsArray);
            return View(vm);
        }

        [Authorize]
        public ActionResult _UserBuildingList()
        {
            UserBuildingsViewModel userBuildingModel = new UserBuildingsViewModel();
            userBuildingModel.listModel = new List<ItemUserBuildingViewModel>();

            foreach (var item in _userBuildingService.GetUserBuildingList(User.Identity.Name))
            {
                var building = _buildingsHelper.GetBuildings().Where(b => b.ID == item.Building_ID).First();
                int bTime = 0;
                int timeLeft = 0;
                string status = String.Empty;
                if (item.Status == "budowa" || item.Status == "rozbudowa")
                {
                    bTime = building.BuildingTime;
                    timeLeft = _buildingsHelper.BuildingTimeLeft(User.Identity.Name, item.ID);
                }
                else if (item.Status == "burzenie")
                {
                    bTime = building.DestructionTime;
                    timeLeft = _buildingsHelper.BuildingTimeLeft(User.Identity.Name, item.ID);
                }
                userBuildingModel.listModel.Add(new ItemUserBuildingViewModel
                {
                    ID = item.ID,
                    User_ID = item.User_ID,
                    User_Login = item.Login,
                    X_pos = item.X_pos,
                    Y_pos = item.Y_pos,
                    Lvl = item.Lvl,
                    Building_ID = item.Building_ID,
                    Building_Name = item.Building_Name,
                    Status = item.Status,
                    Produkcja = item.Produkcja,
                    ifCan = _userBuildingService.ifLvlUp(item.ID, User.Identity.Name),
                    Percent_price_per_lvl = item.PriceLvlUp,
                    Percent_product_per_lvl = item.ProdukcjaLvlUp,
                    Color = item.Color,
                    Stock = item.Stock,
                    BuildTime = bTime,
                    BuildDone = bTime - timeLeft,
                    ifDeal = (item.DealID != null) ? "Tak" : "Nie"
                });
            }

            return View(userBuildingModel);
        }

        [Authorize]
        public ActionResult _UserProductList()
        {
            UserProductViewModel userBuildingModel = new UserProductViewModel();

            userBuildingModel.listModel = _userProductService.GetUserProductList(User.Identity.Name).Select(x => new ItemUserProductViewModel
            {
                User_ID = x.User_ID,
                User_Login = x.Login,
                Product_Name = x.Product_Name,
                Value = x.Value,
                Product_ID = x.Product_ID
            }).ToList();

            return View(userBuildingModel);
        }

        [Authorize]
        public ActionResult _UserDealList()
        {
            DealViewModel dealModel = new DealViewModel();

            dealModel.listModel = _dealService.GetUserDeals(User.Identity.Name).Select(x => new ItemDealViewModel
            {
                ID = x.ID,
                User1_Login = x.User1_Login,
                User2_Login = x.User2_Login,
                Percent_User1 = x.Percent_User1,
                Percent_User2 = x.Percent_User2,
                Building_Name = x.Building_Name,
                IsActive = x.IsActive,
                Owner = x.MyMap,
                Value = x.FinishDate.Value.Subtract(DateTime.Now).Minutes,
                DayToEnd = x.FinishDate.Value.Subtract(DateTime.Now).Days,
                DealDay = x.DayTime,
            }).ToList();

            //dealModel.buildingList = new string [ _buildingsHelper.GetBuildings().OrderBy(x => x.Alias).Select(x => x.Alias).ToString(), Convert.ToString(_buildingsHelper.GetBuildings().OrderBy(x => x.Alias).Select(x => x.Price))];
            dealModel.buildingList2 = new List<SelectListItem>();
            foreach (var item in _buildingsHelper.GetBuildings())
            {
                dealModel.buildingList2.Add(new SelectListItem
                {
                    Value = item.Alias,
                    Text = item.Alias +" - $"+ item.Price.ToString()
                });
            }

            List<string> temp = new List<string>();
            int i = 0;
            foreach (var item in _userService.GetUser())
            {
                if (!_userService.Ignored(User.Identity.Name, item.Login) && (item.Login != User.Identity.Name))
                {
                    temp.Add(item.Login);
                }
                i++;
            }

            dealModel.userList = temp.ToArray();

            Array.Sort(dealModel.userList);

            return View(dealModel);
        }

        [Authorize]
        public ActionResult _UserProductRequirementList()
        {
            ProductRequirementViewModel productModel = new ProductRequirementViewModel();

            productModel.listModel = _productRequirementService.GetCanUserProducts(User.Identity.Name).Select(x => new ItemProductRequirementViewModel
            {
                BaseName = x.Base_Name,
                RequireProduct = x.RequireProduct,
                BuildingName = x.RequireBuilding,
                ifCan = x.IfCanProduct
            }).ToList();

            return View(productModel);
        }

        [Authorize]
        public JsonResult AcceptDeal(int a, string user)
        {
            var accepted = _dealService.AcceptDeal(a, User.Identity.Name);
            if (accepted)
            {

                NotificationDto notificationDto = new NotificationDto();

                notificationDto.Description = "Umowa zaakceptowana";
                notificationDto.User_Login = user;

                _notificationService.SentNotification(notificationDto);
                return new JsonResult { Data = true };
            }
            else
            {
                return new JsonResult { Data = false };
            }

        }


        [Authorize]
        public JsonResult CancelDeal(int a, string user)
        {
            _dealService.CancelDeal(a);
            if (user != null)
            {
                NotificationDto notificationDto = new NotificationDto();

                notificationDto.Description = "Umowa odrzucona";
                notificationDto.User_Login = user;

                _notificationService.SentNotification(notificationDto);
            }

            return new JsonResult { Data = true };
        }

        [Authorize]
        public JsonResult CancelRerun(int a, string user)
        {
            _dealService.CancelRerun(a);

            return new JsonResult { Data = true };
        }


        [Authorize]
        public void RerunDeal(int a, string user)
        {
            _dealService.RerunDeal(a, User.Identity.Name);

            NotificationDto notificationDto = new NotificationDto();

            notificationDto.Description = "Odnowienie umowy";
            notificationDto.User_Login = user;

            _notificationService.SentNotification(notificationDto);
        }


        [Authorize]
        public ActionResult AddDeal(DealViewModel dealModel)
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

            DealDto dealDto = new DealDto();

            dealDto.User1_Login = User.Identity.Name;
            dealDto.User2_Login = dealModel.viewModel.User2_Login;
            dealDto.Building_Name = dealModel.viewModel.Building_Name;
            if (dealModel.viewModel.Owner == true)
            {
                dealDto.Map_ID = 1;
            }
            else
            {
                dealDto.Map_ID = 0;
            }
            var temp = dealModel.viewModel.Value;
            dealDto.Percent_User1 = dealModel.viewModel.Percent_User1;
            dealDto.FinishDate = (DateTime.Now.AddDays(dealModel.viewModel.DealDay));
            dealDto.DayTime = dealModel.viewModel.DealDay;

            if (dealDto.User2_Login != User.Identity.Name)
            {
                foreach (var item in _dealService.AddDeal(dealDto))
                {
                    if (item == 1)
                    {
                        NotificationDto notificationDto = new NotificationDto();

                        notificationDto.Description = "Nowa umowa";
                        notificationDto.User_Login = dealModel.viewModel.User2_Login;

                        _notificationService.SentNotification(notificationDto);

                        errors.Add("Oferta złożona");
                    }
                    else if (item == 2)
                    {
                        errors.Add("Taki budyenk nie istnieje.");
                    }
                    else if (item == 0)
                    {
                        errors.Add("Taki użytkownik nie istnieje.");
                    }
                    else if (item == 3)
                    {
                        errors.Add("Nie stać Cię na taką umowę.");
                    }
                }
            }
            else if (dealDto.User2_Login == User.Identity.Name)
            {
                errors.Add("Nie możesz złożyć oferty samemu sobie");
            }

            Session["val"] = errors.ToArray<string>();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddOffer(UserProductViewModel marketModel)
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

            MarketDto _marketDto = new MarketDto();

            _marketDto.Login = User.Identity.Name;
            _marketDto.Product_Name = marketModel.viewModel.Product_Name;
            _marketDto.Number = marketModel.viewModel.Value;
            _marketDto.Price = marketModel.viewModel.Price;
            _marketDto.TypeOffer = true;

            if(_marketService.AddOffer(_marketDto))
            {
                errors.Add("Dodano ofertę.");
            }
            else
            {
                errors.Add("Błąd. Spróbuj ponownie.");
            }
            Session["val"] = errors.ToArray<string>();

            return RedirectToAction("Index");
        }

    }
}