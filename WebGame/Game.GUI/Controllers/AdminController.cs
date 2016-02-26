using Game.Core.DTO;
using Game.GUI.ViewModels.Building;
using Game.GUI.ViewModels.Building.DealBuildings;
using Game.GUI.ViewModels.Building.QueueBuilding;
using Game.GUI.ViewModels.Building.UserBuildings;
using Game.GUI.ViewModels.Deal;
using Game.GUI.ViewModels.Dolar;
using Game.GUI.ViewModels.Map;
using Game.GUI.ViewModels.Market;
using Game.GUI.ViewModels.Message;
using Game.GUI.ViewModels.Product;
using Game.GUI.ViewModels.Product.ProductRequirement;
using Game.GUI.ViewModels.Product.UserProduct;
using Game.GUI.ViewModels.User;
using Game.GUI.ViewModels.User.Admin;
using Game.GUI.ViewModels.User.Ban;
using Game.GUI.ViewModels.User.Friend;
using Game.GUI.ViewModels.User.Ignored;
using Game.Service.Interfaces;
using Game.Service.Interfaces.TableInterface;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Game.GUI.Controllers
{
    public class AdminController : Controller
    {
        private IAdminService _adminService;
        private IBanService _banService;
        private IUserService _userService;
        private IBuildingService _buildingService;
        private IQueueService _queueService;
        private IUserBuildingService _userBuildingService;
        private IUserProductService _userProductService;
        private IMapService _mapService;
        private IMarketService _marketService;
        private IDolarService _dolarService;
        private IDealService _dealService;
        private INotificationService _notificationService;
        private IProductService _productService;
        private IMessageService _messageService;
        private IProductRequirementsService _productRService;

        public AdminController(IAdminService adminService,
            IUserService userService,
            IBanService banService,
            IBuildingService buildingService,
            IQueueService queueService,
            IUserBuildingService userBuildingService,
            IUserProductService userProductService,
            IMapService mapService,
            IMarketService marketService,
            IDolarService dolarService,
            IDealService dealService,
            INotificationService notificationService,
            IProductService productService,
            IMessageService messageService,
            IProductRequirementsService productRService)
        {
            _adminService = adminService;
            _userService = userService;
            _banService = banService;
            _buildingService = buildingService;
            _queueService = queueService;
            _userBuildingService = userBuildingService;
            _userProductService = userProductService;
            _mapService = mapService;
            _marketService = marketService;
            _dolarService = dolarService;
            _dealService = dealService;
            _notificationService = notificationService;
            _productService = productService;
            _messageService = messageService;
            _productRService = productRService;
        }

        [Authorize]
        public ActionResult Admin()
        {
            if (_adminService.ifAdmin(User.Identity.Name))
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        #region AdminTable

        [Authorize]
        public ActionResult _AdminList()
        {
            AdminViewModel adminModel = new AdminViewModel();

            adminModel.listModel = _adminService.GetAdmin().Select(x => new ItemAdminViewModel
            {
                ID = x.ID,
                User_Login = x.Login
            }).ToList();

            adminModel.allUser = _userService.GetUser().Select(x => x.Login).ToArray();

            return View(adminModel);
        }

        [Authorize]
        public ActionResult AddAdmin(AdminViewModel adminModel)
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

            AdminDto _adminDto = new AdminDto();

            _adminDto.Login = adminModel.viewModel.User_Login;

            if (_adminService.Add(_adminDto))
            {
                errors.Add("Dodano administratora.");
            }
            else
            {
                errors.Add("Błąd. Spróbuj ponownie.");
            }
            Session["val"] = errors.ToArray<string>();

            return RedirectToAction("Admin");
        }

        [HttpPost]
        [Authorize]
        public ActionResult UpdateAdmin(AdminViewModel viewModel)
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

            AdminDto _adminDto = new AdminDto();

            _adminDto.ID = viewModel.viewModel.ID;
            _adminDto.Login = viewModel.viewModel.User_Login;

            if (_adminService.Update(_adminDto))
            {
                errors.Add("Zaktualizowano administratora.");
            }
            else
            {
                errors.Add("Błąd. Spróbuj ponownie.");
            }

            Session["val"] = errors.ToArray<string>();
            return RedirectToAction("Admin");
        }


        [HttpGet]
        [Authorize]
        public ActionResult DeleteAdmin(int id)
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

            if (_adminService.Delete(id))
            {
                errors.Add("Usunięto administratora.");
            }
            else
            {
                errors.Add("Błąd. Srpróbuj ponownie.");
            }
            Session["val"] = errors.ToArray<string>();

            return RedirectToAction("Admin");
        }

        #endregion

        #region BanTable
        [Authorize]
        public ActionResult _BanList()
        {
            BanViewModel banModel = new BanViewModel();

            banModel.listModel = _banService.GetBan().Select(x => new ItemBanViewModel
            {
                ID = x.ID,
                Description = x.Description,
                FinishDate = x.Finish_Date,
                StartDate = x.Start_Date,
                User_Login = x.Login
            }).ToList();

            banModel.allUser = _userService.GetUser().Select(x => x.Login).ToArray();

            return View(banModel);
        }


        [HttpPost]
        [Authorize]
        public ActionResult AddBan(BanViewModel banModel)
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

            BanDto _banDto = new BanDto();

            _banDto.Login = banModel.viewModel.User_Login;
            _banDto.Description = banModel.viewModel.Description;
            _banDto.Finish_Date = banModel.viewModel.FinishDate;

            if (_banService.Add(_banDto))
            {
                errors.Add("Zablokowano gracza.");
            }
            else
            {
                errors.Add("Błąd. Srpróbuj ponownie.");
            }
            Session["val"] = errors.ToArray<string>();

            return RedirectToAction("Admin");
        }


        [HttpPost]
        [Authorize]
        public ActionResult UpdateBan(BanViewModel banModel)
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

            BanDto _banDto = new BanDto();

            _banDto.ID = banModel.viewModel.ID;
            _banDto.Login = banModel.viewModel.User_Login;
            _banDto.Description = banModel.viewModel.Description;
            _banDto.Finish_Date = banModel.viewModel.FinishDate;

            if (_banService.Update(_banDto))
            {
                errors.Add("Zaktualizowano blokadę.");
            }
            else
            {
                errors.Add("Błąd. Srpróbuj ponownie.");
            }
            Session["val"] = errors.ToArray<string>();

            return RedirectToAction("Admin");
        }

        [HttpGet]
        [Authorize]
        public ActionResult DeleteBan(int id)
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

            if (_banService.Delete(id))
            {
                errors.Add("Usunięto blokade.");
            }
            else
            {
                errors.Add("Błąd. Srpróbuj ponownie.");
            }
            Session["val"] = errors.ToArray<string>();

            return RedirectToAction("Admin");
        }
        #endregion

        #region BuildingTable
        [Authorize]
        public ActionResult _BuildingList()
        {
            BuildingViewModel buildingModel = new BuildingViewModel();

            buildingModel.listModel = _buildingService.GetBuilding().Select(x => new ItemBuildingViewModel
            {
                BuildingName = x.Name,
                Price = x.Price,
                ID = x.ID,
                Height = x.Height,
                Width = x.Width,
                DestPrice = (int)x.Dest_price,
                Percent_price_per_lvl = x.Percent_price_per_lvl,
                Percent_product_per_lvl = x.Percent_product_per_lvl,
                Product_ID = x.Product_ID,
                Product_Name = x.Product_Name,
                Product_per_sec = x.Product_per_sec,
                Alias = x.Alias,
                BuildingTime = x.BuildingTime,
                DestructionTime = x.DestructionTime,
                Stock = x.Stock ? "Tak" : "Nie"
            }).ToList();

            buildingModel.productName = _productService.GetProduct().Select(x => x.Alias).ToArray();

            return View(buildingModel);
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddBuilding(BuildingViewModel buildingModel)
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

            BuildingDto _buildingDto = new BuildingDto();

            _buildingDto.Name = buildingModel.viewModel.BuildingName;
            _buildingDto.Price = buildingModel.viewModel.Price;
            _buildingDto.Height = buildingModel.viewModel.Height;
            _buildingDto.Width = buildingModel.viewModel.Width;
            _buildingDto.Dest_price = (int)buildingModel.viewModel.DestructionTime;
            _buildingDto.Percent_price_per_lvl = buildingModel.viewModel.Percent_price_per_lvl;
            _buildingDto.Percent_product_per_lvl = buildingModel.viewModel.Percent_product_per_lvl;
            _buildingDto.Product_Name = buildingModel.viewModel.Product_Name;
            _buildingDto.Product_per_sec = buildingModel.viewModel.Product_per_sec;
            _buildingDto.Alias = buildingModel.viewModel.Alias;
            _buildingDto.BuildingTime = buildingModel.viewModel.BuildingTime;
            _buildingDto.DestructionTime = buildingModel.viewModel.DestructionTime;
            _buildingDto.Stock = buildingModel.viewModel.Stock.ToLower().Contains("tak") ? true : false;

            if (_buildingService.Add(_buildingDto))
            {
                errors.Add("Dodano budynek.");
            }
            else
            {
                errors.Add("Błąd. Spróbuj ponownie.");
            }
            Session["val"] = errors.ToArray<string>();

            return RedirectToAction("Admin");
        }

        [HttpPost]
        [Authorize]
        public ActionResult UpdateBuilding(BuildingViewModel buildingModel)
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

            BuildingDto _buildingDto = new BuildingDto();

            _buildingDto.ID = buildingModel.viewModel.ID;
            _buildingDto.Name = buildingModel.viewModel.BuildingName;
            _buildingDto.Price = buildingModel.viewModel.Price;
            _buildingDto.Height = buildingModel.viewModel.Height;
            _buildingDto.Width = buildingModel.viewModel.Width;
            _buildingDto.Dest_price = (int)buildingModel.viewModel.DestructionTime;
            _buildingDto.Percent_price_per_lvl = buildingModel.viewModel.Percent_price_per_lvl;
            _buildingDto.Percent_product_per_lvl = buildingModel.viewModel.Percent_product_per_lvl;
            _buildingDto.Product_Name = buildingModel.viewModel.Product_Name;
            _buildingDto.Product_per_sec = buildingModel.viewModel.Product_per_sec;
            _buildingDto.Alias = buildingModel.viewModel.Alias;
            _buildingDto.BuildingTime = buildingModel.viewModel.BuildingTime;
            _buildingDto.DestructionTime = buildingModel.viewModel.DestructionTime;
            _buildingDto.Stock = buildingModel.viewModel.Stock.ToLower().Contains("tak") ? true : false;

            if (_buildingService.Update(_buildingDto))
            {
                errors.Add("Zaktualizowano budynek.");
            }
            else
            {
                errors.Add("Błąd. Spróbuj ponownie.");
            }
            Session["val"] = errors.ToArray<string>();

            return RedirectToAction("Admin");
        }

        [HttpGet]
        [Authorize]
        public ActionResult DeleteBuilding(int id)
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

            if (_buildingService.Delete(id))
            {
                errors.Add("Usunięto budynek.");
            }
            else
            {
                errors.Add("Błąd. Spróbuj ponownie.");
            }
            Session["val"] = errors.ToArray<string>();

            return RedirectToAction("Admin");
        }
        #endregion

        #region UserTable
        [Authorize]
        public ActionResult _UserList()
        {
            UserViewModel userModel = new UserViewModel();

            userModel.listModel = _userService.GetUser().Select(x => new ItemUserViewModel
            {
                ID = x.ID,
                User_Login = x.Login,
                Email = x.Email,
                LastUpdate = x.Last_Update,
                RegistrationDate = x.Registration_Date,
                LastLog = x.Last_Log
            }).ToList();
            return View(userModel);
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddUser(UserViewModel userModel)
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

            UserDto _userDto = new UserDto();

            _userDto.Login = userModel.viewModel.User_Login;
            _userDto.Password = userModel.viewModel.Password;
            _userDto.Email = userModel.viewModel.Email;
            _userDto.Last_Log = userModel.viewModel.LastLog;
            _userDto.Registration_Date = userModel.viewModel.RegistrationDate;
            _userDto.Last_Update = userModel.viewModel.LastUpdate;

            if (_userService.Add(_userDto))
            {
                errors.Add("Dodano użytkownika.");
            }
            else
            {
                errors.Add("Błąd. Spróbuj ponownie.");
            }
            Session["val"] = errors.ToArray<string>();

            return RedirectToAction("Admin");
        }

        [HttpPost]
        [Authorize]
        public ActionResult UpdateUser(UserViewModel userModel)
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

            UserDto _userDto = new UserDto();

            _userDto.ID = userModel.viewModel.ID;
            _userDto.Login = userModel.viewModel.User_Login;
            _userDto.Email = userModel.viewModel.Email;
            _userDto.Last_Log = userModel.viewModel.LastLog;
            _userDto.Registration_Date = userModel.viewModel.RegistrationDate;
            _userDto.Last_Update = userModel.viewModel.LastUpdate;

            if (_userService.Update(_userDto))
            {
                errors.Add("Zaktualizowano użytkownika.");
            }
            else
            {
                errors.Add("Błąd. Spróbuj ponownie.");
            }
            Session["val"] = errors.ToArray<string>();

            return RedirectToAction("Admin");
        }

        [HttpGet]
        [Authorize]
        public ActionResult DeleteUser(int id)
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


            if (_userService.Delete(id))
            {
                errors.Add("Usunięto użytkownika.");
            }
            else
            {
                errors.Add("Błąd. Sprawdź czy użytkownik nie ma aktualnej umowy i spróbuj ponownie.");
            }
            Session["val"] = errors.ToArray<string>();

            return RedirectToAction("Admin");
        }

        #endregion

        #region DolarTable
        [Authorize]
        public ActionResult _DolarList()
        {
            DolarViewModel dolarModel = new DolarViewModel();

            dolarModel.listModel = _dolarService.GetDolars().Select(x => new ItemDolarViewModel
            {
                ID = x.ID,
                User_ID = x.User_ID,
                User_Login = x.Login,
                DolarValue = x.Value
            }).ToList();

            dolarModel.allUser = _userService.GetUser().Select(x => x.Login).ToArray();

            return View(dolarModel);
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddDolar(DolarViewModel dolarModel)
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

            DolarDto _dolarDto = new DolarDto();

            _dolarDto.User_ID = dolarModel.viewModel.User_ID;
            _dolarDto.Login = dolarModel.viewModel.User_Login;
            _dolarDto.Value = dolarModel.viewModel.DolarValue;

            if (_dolarService.Add(_dolarDto))
            {
                errors.Add("Dodano pieniądze.");
            }
            else
            {
                errors.Add("Błąd. Spróbuj ponownie.");
            }
            Session["val"] = errors.ToArray<string>();

            return RedirectToAction("Admin");
        }

        [HttpPost]
        [Authorize]
        public ActionResult UpdateDolar(DolarViewModel dolarModel)
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

            DolarDto _dolarDto = new DolarDto();

            _dolarDto.ID = dolarModel.viewModel.ID;
            _dolarDto.User_ID = dolarModel.viewModel.User_ID;
            _dolarDto.Login = dolarModel.viewModel.User_Login;
            _dolarDto.Value = dolarModel.viewModel.DolarValue;

            if (_dolarService.Update(_dolarDto))
            {
                errors.Add("Zaktualizowano pieniądze.");
            }
            else
            {
                errors.Add("Błąd. Spróbuj ponownie.");
            }
            Session["val"] = errors.ToArray<string>();

            return RedirectToAction("Admin");
        }

        //[HttpGet]
        //[Authorize]
        //public ActionResult DeleteDolar(int id)
        //{
        //    _dolarService.Delete(id);
        //    return RedirectToAction("Admin");
        //}
        #endregion

        #region MapTable
        [Authorize]
        public ActionResult _MapList(int? Page_No)
        {
            MapViewModel mapModel = new MapViewModel();

            mapModel.listModel = _mapService.GetMaps().Select(x => new ItemMapViewModel
            {
                ID = x.Map_ID,
                User_ID = x.User_ID,
                User_Login = x.Login,
                Height = x.Height,
                Width = x.Width
            }).ToList();

            mapModel.allUser = _userService.GetUser().Select(x => x.Login).ToArray();


            return View(mapModel);
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddMap(MapViewModel mapModel)
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

            MapDto _mapDto = new MapDto();

            _mapDto.Login = mapModel.viewModel.User_Login;
            _mapDto.Height = mapModel.viewModel.Height;
            _mapDto.Width = mapModel.viewModel.Width;

            if (_mapService.Add(_mapDto))
            {
                errors.Add("Dodano mapę.");
            }
            else
            {
                errors.Add("Błąd. Spróbuj ponownie.");
            }
            Session["val"] = errors.ToArray<string>();

            return RedirectToAction("Admin");
        }

        [HttpPost]
        [Authorize]
        public ActionResult UpdateMap(MapViewModel mapModel)
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

            MapDto _mapDto = new MapDto();

            _mapDto.Map_ID = mapModel.viewModel.ID;
            _mapDto.Login = mapModel.viewModel.User_Login;
            _mapDto.Height = mapModel.viewModel.Height;
            _mapDto.Width = mapModel.viewModel.Width;

            if (_mapService.Update(_mapDto))
            {
                errors.Add("Zaktualizowano mapę.");
            }
            else
            {
                errors.Add("Błąd. Spróbuj ponownie.");
            }
            Session["val"] = errors.ToArray<string>();

            return RedirectToAction("Admin");
        }

        //[HttpGet]
        //[Authorize]
        //public ActionResult DeleteMap(int id)
        //{
        //    List<string> errors;
        //    if (Session["val"] != null)
        //    {
        //        errors = ((string[])Session["val"]).ToList();
        //    }
        //    else
        //    {
        //        errors = new List<string>();
        //    }

        //    try
        //    {
        //        _mapService.Delete(id);
        //        errors.Add("Usunięto mapę");
        //    }
        //    catch (Exception ex)
        //    {
        //        errors.Add(ex.Message);
        //    }
        //    Session["val"] = errors.ToArray<string>();

        //    return RedirectToAction("Admin");
        //}
        #endregion

        #region MarketTable

        [Authorize]
        public ActionResult _MarketList()
        {
            MarketViewModel marketModel = new MarketViewModel();

            marketModel.listModel = _marketService.GetOffer().Select(x => new ItemMarketViewModel
            {
                Number = x.Number,
                ID = x.ID,
                Price = x.Price,
                Product_ID = x.Product_ID,
                Product_Name = x.Product_Name,
                TypeOfferAdmin = (x.TypeOffer == false ? "Kupno" : "Sprzedaż"),
                User_ID = x.User_ID,
                User_Login = x.Login,
            }).ToList();

            marketModel.allProduct = _productService.GetProduct().Select(x => x.Alias).ToArray();
            marketModel.allUser = _userService.GetUser().Select(x => x.Login).ToArray();

            return View(marketModel);
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddMarket(MarketViewModel marketModel)
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

            _marketDto.Login = marketModel.viewModel.User_Login;
            _marketDto.Product_Name = marketModel.viewModel.Product_Name;
            _marketDto.Number = marketModel.viewModel.Number;
            _marketDto.Price = marketModel.viewModel.Price;
            _marketDto.TypeOffer = marketModel.TypeOfferAdmin.ToLower().Contains("Sprzedaż") ? true : false;

            if (_marketService.Add(_marketDto))
            {
                errors.Add("Dodano ofertę.");
            }
            else
            {
                errors.Add("Błąd. Spróbuj ponownie.");
            }
            Session["val"] = errors.ToArray<string>();

            return RedirectToAction("Admin");
        }

        [HttpPost]
        [Authorize]
        public ActionResult UpdateMarket(MarketViewModel marketModel)
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

            _marketDto.ID = marketModel.viewModel.ID;
            _marketDto.Login = marketModel.viewModel.User_Login;
            _marketDto.Product_Name = marketModel.viewModel.Product_Name;
            _marketDto.Number = marketModel.viewModel.Number;
            _marketDto.Price = marketModel.viewModel.Price;
            _marketDto.TypeOffer = marketModel.TypeOfferAdmin.ToLower().Contains("Sprzedaż") ? true : false;

            if (_marketService.Update(_marketDto))
            {
                errors.Add("Zaktualizowano ofertę.");
            }
            else
            {
                errors.Add("Błąd. Spróbuj ponownie.");
            }
            Session["val"] = errors.ToArray<string>();

            return RedirectToAction("Admin");
        }


        [HttpGet]
        [Authorize]
        public ActionResult DeleteMarket(int id)
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

            if (_marketService.Delete(id))
            {
                errors.Add("Usunięto ofertę.");
            }
            else
            {
                errors.Add("Błąd. Spróbuj ponownie.");
            }
            Session["val"] = errors.ToArray<string>();
            return RedirectToAction("Admin");
        }

        #endregion

        #region ProductTable
        [Authorize]
        public ActionResult _ProductList()
        {
            ProductViewModel productModel = new ProductViewModel();

            productModel.listModel = _productService.GetProduct().Select(x => new ItemProductViewModel
            {
                ID = x.ID,
                ProductName = x.Name,
                Price_per_unit = x.Price_per_unit,
                Unit = x.Unit,
                Alias = x.Alias
            }).ToList();

            return View(productModel);
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddProduct(ProductViewModel productModel)
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

            ProductDto _productDto = new ProductDto();

            _productDto.Name = productModel.viewModel.ProductName;
            _productDto.Price_per_unit = productModel.viewModel.Price_per_unit;
            _productDto.Unit = productModel.viewModel.Unit;
            _productDto.Alias = productModel.viewModel.Alias;

            if (_productService.Add(_productDto))
            {
                errors.Add("Dodano produkt.");
            }
            else
            {
                errors.Add("Błąd. Spróbuj ponownie.");
            }
            Session["val"] = errors.ToArray<string>();

            return RedirectToAction("Admin");
        }

        [HttpPost]
        [Authorize]
        public ActionResult UpdateProduct(ProductViewModel productModel)
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

            ProductDto _productDto = new ProductDto();

            _productDto.ID = productModel.viewModel.ID;
            _productDto.Name = productModel.viewModel.ProductName;
            _productDto.Price_per_unit = productModel.viewModel.Price_per_unit;
            _productDto.Unit = productModel.viewModel.Unit;
            _productDto.Alias = productModel.viewModel.Alias;

            if (_productService.Update(_productDto))
            {
                errors.Add("Zaktualizowano produkt.");
            }
            else
            {
                errors.Add("Błąd. Spróbuj ponownie.");
            }
            Session["val"] = errors.ToArray<string>();

            return RedirectToAction("Admin");
        }

        [HttpGet]
        [Authorize]
        public ActionResult DeleteProduct(int id)
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

            if (_productService.Delete(id))
            {
                errors.Add("Usunięto produkt i budynek, który go produkuje.");
            }
            else
            {
                errors.Add("Błąd. Spróbuj ponownie.");
            }
            Session["val"] = errors.ToArray<string>();

            return RedirectToAction("Admin");
        }

        #endregion

        #region UserBuildingTable
        [Authorize]
        public ActionResult _UserBuildingList()
        {
            UserBuildingsViewModel userBuildingModel = new UserBuildingsViewModel();
            userBuildingModel.listModel = _userBuildingService.GetUserBuilding().Select(x => new ItemUserBuildingViewModel
            {
                ID = x.ID,
                User_ID = x.User_ID,
                User_Login = x.Login,
                X_pos = x.X_pos,
                Y_pos = x.Y_pos,
                Lvl = x.Lvl,
                Building_ID = x.Building_ID,
                Building_Name = x.Building_Name,
                Status = x.Status,
                DateOfConstruction = x.DateOfConstruction,
                Color = x.Color,
                Percent_product = x.Percent_Product,
                Owner = x.Owner ? "Tak" : "Nie"
            }).ToList();

            userBuildingModel.allBuilding = _buildingService.GetBuilding().Select(x => x.Alias).ToArray();
            userBuildingModel.allProduct = _productService.GetProduct().Select(x => x.Alias).ToArray();
            userBuildingModel.allUser = _userService.GetUser().Select(x => x.Login).ToArray();

            return View(userBuildingModel);
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddUserBuilding(UserBuildingsViewModel userBuildingModel)
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

            UserBuildingDto _userBuildingDto = new UserBuildingDto();

            _userBuildingDto.User_ID = userBuildingModel.viewModel.User_ID;
            _userBuildingDto.Login = userBuildingModel.viewModel.User_Login;
            _userBuildingDto.X_pos = userBuildingModel.viewModel.X_pos;
            _userBuildingDto.Y_pos = userBuildingModel.viewModel.Y_pos;
            _userBuildingDto.Lvl = userBuildingModel.viewModel.Lvl;
            _userBuildingDto.Building_ID = userBuildingModel.viewModel.Building_ID;
            _userBuildingDto.Building_Name = userBuildingModel.viewModel.Building_Name;
            _userBuildingDto.Status = userBuildingModel.viewModel.Status;
            _userBuildingDto.Owner = userBuildingModel.viewModel.Owner.ToLower().Contains("tak") ? true : false;
            _userBuildingDto.Percent_Product = userBuildingModel.viewModel.Percent_product;
            _userBuildingDto.Color = userBuildingModel.viewModel.Color;

            if (_userBuildingService.Add(_userBuildingDto))
            {
                errors.Add("Dodano budynek użytkownikowi.");
            }
            else
            {
                errors.Add("Błąd. Spróbuj ponownie.");
            }
            Session["val"] = errors.ToArray<string>();

            return RedirectToAction("Admin");
        }

        [HttpPost]
        [Authorize]
        public ActionResult UpdateUserBuilding(UserBuildingsViewModel userBuildingModel)
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

            UserBuildingDto _userBuildingDto = new UserBuildingDto();

            _userBuildingDto.ID = userBuildingModel.viewModel.ID;
            _userBuildingDto.User_ID = userBuildingModel.viewModel.User_ID;
            _userBuildingDto.Login = userBuildingModel.viewModel.User_Login;
            _userBuildingDto.X_pos = userBuildingModel.viewModel.X_pos;
            _userBuildingDto.Y_pos = userBuildingModel.viewModel.Y_pos;
            _userBuildingDto.Lvl = userBuildingModel.viewModel.Lvl;
            _userBuildingDto.Building_ID = userBuildingModel.viewModel.Building_ID;
            _userBuildingDto.Building_Name = userBuildingModel.viewModel.Building_Name;
            _userBuildingDto.Status = userBuildingModel.viewModel.Status;
            _userBuildingDto.Owner = userBuildingModel.viewModel.Owner.ToLower().Contains("tak") ? true : false;
            _userBuildingDto.Percent_Product = userBuildingModel.viewModel.Percent_product;
            _userBuildingDto.DateOfConstruction = userBuildingModel.viewModel.DateOfConstruction;
            _userBuildingDto.Color = userBuildingModel.viewModel.Color;

            if (_userBuildingService.Update(_userBuildingDto))
            {
                errors.Add("Zaktualizowano budynek użytkownika.");
            }
            else
            {
                errors.Add("Błąd. Spróbuj ponownie.");
            }
            Session["val"] = errors.ToArray<string>();

            return RedirectToAction("Admin");
        }


        [HttpGet]
        [Authorize]
        public ActionResult DeleteUserBuilding(int id)
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

            if (_userBuildingService.Delete(id))
            {
                errors.Add("Usunięto budynek użytkownikowi.");
            }
            else
            {
                errors.Add("Błąd. Spróbuj ponownie.");
            }

            return RedirectToAction("Admin");
        }

        #endregion

        #region UserProductTable
        [Authorize]
        public ActionResult _UserProductList()
        {
            UserProductViewModel userProductModel = new UserProductViewModel();

            userProductModel.listModel = _userProductService.GetUserProduct().Select(x => new ItemUserProductViewModel
            {
                ID = x.ID,
                User_ID = x.User_ID,
                User_Login = x.Login,
                Product_Name = x.Product_Name,
                Value = x.Value,
                Product_ID = x.Product_ID
            }).ToList();

            userProductModel.allProduct = _productService.GetProduct().Select(x => x.Alias).ToArray();
            userProductModel.allUser = _userService.GetUser().Select(x => x.Login).ToArray();

            return View(userProductModel);
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddUserProduct(UserProductViewModel userProductModel)
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

            UserProductDto _userProductDto = new UserProductDto();

            _userProductDto.Login = userProductModel.viewModel.User_Login;
            _userProductDto.Product_Name = userProductModel.viewModel.Product_Name;
            _userProductDto.Value = userProductModel.viewModel.Value;

            if(_userProductService.Add(_userProductDto))
            {
                errors.Add("Dodano produkt użytkownikowi.");
            }
            else
            {
                errors.Add("Błąd. Spróbuj ponownie.");
            }
            Session["val"] = errors.ToArray<string>();

            return RedirectToAction("Admin");
        }

        [HttpPost]
        [Authorize]
        public ActionResult UpdateUserProduct(UserProductViewModel userProductModel)
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

            UserProductDto _userProductDto = new UserProductDto();

            _userProductDto.ID = userProductModel.viewModel.ID;
            _userProductDto.ID = userProductModel.viewModel.ID;
            _userProductDto.Login = userProductModel.viewModel.User_Login;
            _userProductDto.Product_Name = userProductModel.viewModel.Product_Name;
            _userProductDto.Value = userProductModel.viewModel.Value;

            if(_userProductService.Update(_userProductDto))
            {
                errors.Add("Zaktualizowano produkt użytkownika.");
            }
            else
            {
                errors.Add("Błąd. Spróbuj ponownie.");
            }
            Session["val"] = errors.ToArray<string>();

            return RedirectToAction("Admin");
        }

        [HttpGet]
        [Authorize]
        public ActionResult DeleteUserProduct(int id)
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

            if(_userProductService.Delete(id))
            {
                errors.Add("Usunięto produkt użytkownika.");
            }
            else
            {
                errors.Add("Błąd. Spróbuj ponownie.");
            }
            Session["val"] = errors.ToArray<string>();

            return RedirectToAction("Admin");
        }
        #endregion

        #region QueueTable
        [Authorize]
        public ActionResult _BuildingQueueList()
        {
            QueueViewModel queueModel = new QueueViewModel();

            queueModel.listModel = _queueService.GetQueue().Select(x => new ItemQueueViewModel
            {
                ID = x.ID,
                User_ID = x.User_ID,
                User_Login = x.Login,
                UserBuilding_ID = x.UserBuilding_ID,
                BuildingName = x.BuildingName,
                FinishDate = x.FinishTime,
                NewStatus = x.NewStatus
            }).ToList();

            return View(queueModel);
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddQueue(QueueViewModel queueModel)
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

            BuildingQueueDto _queueDto = new BuildingQueueDto();

            _queueDto.Login = queueModel.viewModel.User_Login;
            _queueDto.UserBuilding_ID = queueModel.viewModel.UserBuilding_ID;
            _queueDto.NewStatus = queueModel.viewModel.NewStatus;

            if(_queueService.Add(_queueDto))
            {
                errors.Add("Dodano budynek do kolejki.");
            }
            else
            {
                errors.Add("Błąd. Spróbuj ponownie.");
            }
            Session["val"] = errors.ToArray<string>();

            return RedirectToAction("Admin");
        }

        [HttpPost]
        [Authorize]
        public ActionResult UpdateQueue(QueueViewModel queueModel)
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

            BuildingQueueDto _buildingQueueDto = new BuildingQueueDto();

            _buildingQueueDto.ID = queueModel.viewModel.ID;
            _buildingQueueDto.Login = queueModel.viewModel.User_Login;
            _buildingQueueDto.UserBuilding_ID = queueModel.viewModel.UserBuilding_ID;
            _buildingQueueDto.NewStatus = queueModel.viewModel.NewStatus;
            _buildingQueueDto.FinishTime = queueModel.viewModel.FinishDate;

            if(_queueService.Update(_buildingQueueDto))
            {
                errors.Add("Zaktualizowano budynek w kolejce.");
            }
            else
            {
                errors.Add("Błąd. Spróbuj ponownie.");
            }
            Session["val"] = errors.ToArray<string>();

            return RedirectToAction("Admin");
        }

        [HttpGet]
        [Authorize]
        public ActionResult DeleteQueue(int id)
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

            if (_queueService.Delete(id))
            {
                errors.Add("Usunięto budynek z kolejki.");
            }
            else
            {
                errors.Add("Błąd. Spróbuj ponownie.");
            }
            Session["val"] = errors.ToArray<string>();

            return RedirectToAction("Admin");
        }
        #endregion

        #region DealTable

        public ActionResult _DealList()
        {
            DealViewModel dealModel = new DealViewModel();

            dealModel.listModel = _dealService.GetDeals().Select(x => new ItemDealViewModel
            {
                Building_Name = x.Building_Name,
                DealDay = x.DayTime,
                ID = x.ID,
                Active = (x.IsActive==true ? "Tak":"Nie"),
                Value = x.Map_ID,
                Percent_User1 = x.Percent_User1,
                Percent_User2 = x.Percent_User2,
                User1_Login = x.User1_Login,
                User2_Login = x.User2_Login,
                FinishDate = (DateTime)x.FinishDate
            }).ToList();

            //dealModel.buildingList = _buildingService.GetBuilding().Select(x => x.Alias).ToArray();
            dealModel.userList = _userService.GetUser().Select(x => x.Login).ToArray();
            return View(dealModel);
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

            dealDto.Building_Name = dealModel.viewModel.Building_Name;
            dealDto.DayTime = dealModel.viewModel.DealDay;
            dealDto.IsActive = dealModel.viewModel.Active.ToLower().Contains("tak")? true : false;
            dealDto.Map_ID = dealModel.viewModel.Value;
            dealDto.Percent_User1 = dealModel.viewModel.Percent_User1;
            dealDto.Percent_User2 = dealModel.viewModel.Percent_User2;
            dealDto.User2_Login = dealModel.viewModel.User2_Login;
            dealDto.User1_Login = dealModel.viewModel.User1_Login;

            if(_dealService.AddDealAdmin(dealDto))
            {
                errors.Add("Dodano umowę.");
            }
            else
            {
                errors.Add("Błąd. Spróbuj ponownie.");
            }
            Session["val"] = errors.ToArray<string>();

            return RedirectToAction("Admin");
        }

        [Authorize]
        public ActionResult UpdateDeal(DealViewModel dealModel)
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

            dealDto.ID = dealModel.viewModel.ID;
            dealDto.Building_Name = dealModel.viewModel.Building_Name;
            dealDto.DayTime = dealModel.viewModel.DealDay;
            dealDto.IsActive = dealModel.viewModel.Active.ToLower().Contains("tak") ? true : false;
            dealDto.Map_ID = dealModel.viewModel.Value;
            dealDto.Percent_User1 = dealModel.viewModel.Percent_User1;
            dealDto.Percent_User2 = dealModel.viewModel.Percent_User2;
            dealDto.User2_Login = dealModel.viewModel.User2_Login;
            dealDto.User1_Login = dealModel.viewModel.User1_Login;

            if(_dealService.UpdateDealAdmin(dealDto))
            {
                errors.Add("Zaktualizowano umowę.");
            }
            else
            {
                errors.Add("Błąd. Spróbuj ponownie.");
            }
            Session["val"] = errors.ToArray<string>();

            return RedirectToAction("Admin");
        }

        [Authorize]
        public ActionResult DeleteDeal(int id)
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

            if (_dealService.DeleteDealAdmin(id))
            {
                errors.Add("Usunięto umowę.");
            }
            else
            {
                errors.Add("Błąd. Spróbuj ponownie.");
            }
            Session["val"] = errors.ToArray<string>();

            return RedirectToAction("Admin");
        }
        #endregion

        #region DealBuildingTable
        [Authorize]
        public ActionResult _DealBuildingList()
        {
            DealBuildingViewModel dealbModel = new DealBuildingViewModel();

            dealbModel.listModel = _dealService.GetDealBuildings().Select(x => new ItemDealBuildingViewModel
            {
                ID = x.ID,
                Building_ID = x.Building_ID,
                Building_Name = x.BuildingName,
                Deal_ID = x.Deal_ID,
                User_ID = x.User_ID,
                User_Login = x.Login
            }).ToList();

            dealbModel.allBuilding = _buildingService.GetBuilding().Select(x => x.Alias).ToArray();
            dealbModel.allUser = _userService.GetUser().Select(x => x.Login).ToArray();

            return View(dealbModel);
        }

        [Authorize]
        public ActionResult AddDealBuilding(DealBuildingViewModel dealbModel)
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


            DealBuildingDto dealbDto = new DealBuildingDto();

            dealbDto.BuildingName = dealbModel.viewModel.Building_Name;
            dealbDto.Login = dealbModel.viewModel.User_Login;
            dealbDto.Deal_ID = dealbModel.viewModel.Deal_ID;

            if(_dealService.AddDealBuildingAdmin(dealbDto))
            {
                errors.Add("Dodano budynek do umowy.");
            }
            else
            {
                errors.Add("Błąd. Spróbuj ponownie.");
            }
            Session["val"] = errors.ToArray<string>();

            return RedirectToAction("Admin");
        }

        [Authorize]
        public ActionResult UpdateDealBuilding(DealBuildingViewModel dealbModel)
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

            DealBuildingDto dealbDto = new DealBuildingDto();

            dealbDto.ID = dealbModel.viewModel.ID;
            dealbDto.BuildingName = dealbModel.viewModel.Building_Name;
            dealbDto.Login = dealbModel.viewModel.User_Login;
            dealbDto.Deal_ID = dealbModel.viewModel.Deal_ID;

            if(_dealService.UpdateDealBuildingAdmin(dealbDto))
            {
                errors.Add("Zaktualizowano budynek z umowy.");
            }
            else
            {
                errors.Add("Błąd. Spróbuj ponownie.");
            }
            Session["val"] = errors.ToArray<string>();

            return RedirectToAction("Admin");
        }

        [Authorize]
        public ActionResult DeleteDealBuilding(int id)
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

            if(_dealService.DeleteDealBuildingAdmin(id))
            {
                errors.Add("Usunięto budynek z umowy.");
            }
            else
            {
                errors.Add("Błąd. Spróbuj ponownie.");
            }
            Session["val"] = errors.ToArray<string>();

            return RedirectToAction("Admin");
        }
        #endregion

        #region FriendTable
        [Authorize]
        public ActionResult _FriendList()
        {
            FriendViewModel friendModel = new FriendViewModel();

            friendModel.listModel = _userService.GetFriends().Select(x => new ItemFriendViewModel
            {
                ID = x.ID,
                Friend_ID = x.Friend_ID,
                Friend_Login = x.Friend_Login,
                Accepted = x.OrAccepted ? "Tak" : "Nie",
                User_ID = x.User_ID,
                User_Login = x.User_Login
            }).ToList();

            friendModel.allUser = _userService.GetUser().Select(x => x.Login).ToArray();

            return View(friendModel);
        }

        [Authorize]
        public ActionResult AddFriendAdmin(FriendViewModel friendModel)
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

            FriendDto friendDto = new FriendDto();

            friendDto.ID = friendModel.viewModel.ID;
            friendDto.Friend_Login = friendModel.viewModel.Friend_Login;
            friendDto.User_Login = friendModel.viewModel.User_Login;
            friendDto.OrAccepted = friendModel.viewModel.Accepted.ToLower().Contains("tak") ? true : false;

            if(_userService.AddFriendAdmin(friendDto))
            {
                errors.Add("Dodano znajomych.");
            }
            else
            {
                errors.Add("Błąd. Spróbuj ponownie.");
            }
            Session["val"] = errors.ToArray<string>();

            return RedirectToAction("Admin");
        }

        [Authorize]
        public ActionResult UpdateFriendAdmin(FriendViewModel friendModel)
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

            FriendDto friendDto = new FriendDto();

            friendDto.ID = friendModel.viewModel.ID;
            friendDto.Friend_Login = friendModel.viewModel.Friend_Login;
            friendDto.User_Login = friendModel.viewModel.User_Login;
            friendDto.OrAccepted = friendModel.viewModel.Accepted.ToLower().Contains("tak") ? true : false;

            if(_userService.UpdateFriendAdmin(friendDto))
            {
                errors.Add("Zaktualizowano znajomych.");
            }
            else
            {
                errors.Add("Błąd. Spróbuj ponownie.");
            }
            Session["val"] = errors.ToArray<string>();

            return RedirectToAction("Admin");
        }

        [Authorize]
        public ActionResult DeleteFriendAdmin(int id)
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

            if(_userService.DeleteFriendAdmin(id))
            {
                errors.Add("Usunięto znajomych.");
            }
            else
            {
                errors.Add("Błąd. Spróbuj ponownie.");
            }
            Session["val"] = errors.ToArray<string>();

            return RedirectToAction("Admin");
        }

        #endregion

        #region IgnoredTable
        [Authorize]
        public ActionResult _IgnoredList()
        {
            IgnoredViewModel ignoredModel = new IgnoredViewModel();

            ignoredModel.listModel = _userService.GetIgnored().Select(x => new ItemIgnoredViewModel
            {
                ID = x.ID,
                Ignored_ID = x.Ignored_ID,
                Ignored_Login = x.Ignored_Login,
                User_ID = x.User_ID,
                User_Login = x.User_Login
            }).ToList();

            ignoredModel.allUser = _userService.GetUser().Select(x => x.Login).ToArray();

            return View(ignoredModel);
        }

        [Authorize]
        public ActionResult AddIgnored(IgnoredViewModel ignoredModel)
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

            IgnoredDto ignoredDto = new IgnoredDto();

            ignoredDto.Ignored_Login = ignoredModel.viewModel.Ignored_Login;
            ignoredDto.User_Login = ignoredModel.viewModel.User_Login;

            if(_userService.AddIgnoredAdmin(ignoredDto))
            {
                errors.Add("Dodano ignorowanych.");
            }
            else
            {
                errors.Add("Błąd. Spróbuj ponownie.");
            }
            Session["val"] = errors.ToArray<string>();

            return RedirectToAction("Admin");
        }

        [Authorize]
        public ActionResult UpdateIgnored(IgnoredViewModel ignoredModel)
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

            IgnoredDto ignoredDto = new IgnoredDto();

            ignoredDto.ID = ignoredModel.viewModel.ID;
            ignoredDto.Ignored_Login = ignoredModel.viewModel.Ignored_Login;
            ignoredDto.User_Login = ignoredModel.viewModel.User_Login;

            if(_userService.UpdateIgnoredAdmin(ignoredDto))
            {
                errors.Add("Zaktualizowano ignorowanych.");
            }
            else
            {
                errors.Add("Błąd. Spróbuj ponownie.");
            }
            Session["val"] = errors.ToArray<string>();

            return RedirectToAction("Admin");
        }

        [Authorize]
        public ActionResult DeleteIgnored(int id)
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

            if (_userService.DeleteIgnoredAdmin(id))
            {
                errors.Add("Usunięto ignorowanych.");
            }
            else
            {
                errors.Add("Błąd. Spróbuj ponownie.");
            }
            Session["val"] = errors.ToArray<string>();

            return RedirectToAction("Admin");
        }
        #endregion

        #region MessageTable
        [Authorize]
        public ActionResult _MessageList()
        {
            MessageViewModel messageModel = new MessageViewModel();

            messageModel.listModel = _messageService.GetMessage().Select(x => new ItemMessageViewModel
            {
                Content = x.Content,
                Customer_ID = x.Customer_ID,
                Customer_Login = x.Customer_Login,
                ID = x.ID,
                Read = x.IfRead ? "Tak" : "Nie",
                PostDate = x.PostDate,
                Sender_ID = x.Sender_ID,
                Sender_Login = x.Sender_Login,
                Theme = x.Theme
            }).ToList();

            messageModel.userList = _userService.GetUser().Select(x => x.Login).ToArray();

            return View(messageModel);
        }

        [Authorize]
        public ActionResult AddMessageAdmin(MessageViewModel messageModel)
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

            MessageDto messageDto = new MessageDto();

            messageDto.Content = messageModel.viewModel.Content;
            messageDto.Customer_Login = messageModel.viewModel.Customer_Login;
            messageDto.Sender_Login = messageModel.viewModel.Sender_Login;
            messageDto.Theme = messageModel.viewModel.Theme;
            messageDto.IfRead = messageModel.viewModel.Read.ToLower().Contains("tak") ? true : false;

            if(_messageService.AddMessageAdmin(messageDto))
            {
                errors.Add("Dodano wiadomość.");
            }
            else
            {
                errors.Add("Błąd. Spróbuj ponownie.");
            }
            Session["val"] = errors.ToArray<string>();

            return RedirectToAction("Admin");
        }

        [Authorize]
        public ActionResult UpdateMessageAdmin(MessageViewModel messageModel)
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

            MessageDto messageDto = new MessageDto();

            messageDto.ID = messageModel.viewModel.ID;
            messageDto.Content = messageModel.viewModel.Content;
            messageDto.Customer_ID = messageModel.viewModel.Customer_ID;
            messageDto.Sender_ID = messageModel.viewModel.Sender_ID;
            messageDto.Theme = messageModel.viewModel.Theme;
            messageDto.IfRead = messageModel.viewModel.Read.ToLower().Contains("tak") ? true : false;

            if(_messageService.UpdateMessageAdmin(messageDto))
            {
                errors.Add("Zaktualizowano wiadomość.");
            }
            else
            {
                errors.Add("Błąd. Spróbuj ponownie.");
            }
            Session["val"] = errors.ToArray<string>();

            return RedirectToAction("Admin");
        }

        [Authorize]
        public ActionResult DeleteMessageAdmin(int id)
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

            if(_messageService.DeleteMessageAdmin(id))
            {
                errors.Add("Usunięto wiadomość.");
            }
            else
            {
                errors.Add("Błąd. Spróbuj ponownie.");
            }
            Session["val"] = errors.ToArray<string>();

            return RedirectToAction("Admin");
        }

        #endregion

        #region ProductRequirementTable
        [Authorize]
        public ActionResult _ProductRequirementList()
        {
            ProductRequirementViewModel productrViewModel = new ProductRequirementViewModel();

            productrViewModel.listModel = _productRService.GetProducts().Select(x => new ItemProductRequirementViewModel
            {
                ID = x.ID,
                BaseName = x.Base_Name,
                Base_ID = x.Base_ID,
                RequireName = x.Require_Name,
                Require_ID = x.Require_ID,
                Value = x.Value
            }).ToList();

            productrViewModel.allProduct = _productService.GetProduct().Select(x => x.Alias).ToArray();

            return View(productrViewModel);
        }

        [Authorize]
        public ActionResult AddProductRequirementAdmin(ProductRequirementViewModel productrMode)
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

            ProductRequirementDto productrDto = new ProductRequirementDto();

            productrDto.Base_Name = productrMode.viewModel.BaseName;
            productrDto.Require_Name = productrMode.viewModel.RequireName;
            productrDto.Value = productrMode.viewModel.Value;

            if(_productRService.AddProduct(productrDto))
            {
                errors.Add("Dodano produkt.");
            }
            else
            {
                errors.Add("Błąd. Spróbuj ponownie.");
            }
            Session["val"] = errors.ToArray<string>();

            return RedirectToAction("Admin");
        }

        [Authorize]
        public ActionResult UpdateProductRequirementAdmin(ProductRequirementViewModel productrMode)
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

            ProductRequirementDto productrDto = new ProductRequirementDto();

            productrDto.ID = productrMode.viewModel.ID;
            productrDto.Base_Name = productrMode.viewModel.BaseName;
            productrDto.Require_Name = productrMode.viewModel.RequireName;
            productrDto.Value = productrMode.viewModel.Value;

            if(_productRService.UpdateProduct(productrDto))
            {
                errors.Add("Zaktualizowano produkt.");
            }
            else
            {
                errors.Add("Błąd. Spróbuj ponownie.");
            }
            Session["val"] = errors.ToArray<string>();

            return RedirectToAction("Admin");
        }

        [Authorize]
        public ActionResult DeleteProductRequirementAdmin(int id)
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

            if(_productRService.DeleteProduct(id))
            {
                errors.Add("Usunięto produkt.");
            }
            else
            {
                errors.Add("Błąd. Spróbuj ponownie.");
            }
            Session["val"] = errors.ToArray<string>();

            return RedirectToAction("Admin");
        }
        #endregion
    }
}
