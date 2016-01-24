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
                return View("~/Views/Home/Index.cshtml");
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
            AdminDto _adminDto = new AdminDto();

            _adminDto.Login = adminModel.viewModel.User_Login;

            _adminService.Add(_adminDto);

            return RedirectToAction("Admin");
        }

        [HttpPost]
        [Authorize]
        public ActionResult UpdateAdmin(AdminViewModel viewModel)
        {
            AdminDto _adminDto = new AdminDto();

            _adminDto.ID = viewModel.viewModel.ID;
            _adminDto.Login = viewModel.viewModel.User_Login;

            _adminService.Update(_adminDto);

            return RedirectToAction("Admin");
        }


        [HttpGet]
        [Authorize]
        public ActionResult DeleteAdmin(int id)
        {
            _adminService.Delete(id);
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
            BanDto _banDto = new BanDto();

            _banDto.Login = banModel.viewModel.User_Login;
            _banDto.Description = banModel.viewModel.Description;
            _banDto.Finish_Date = banModel.viewModel.FinishDate;

            _banService.Add(_banDto);

            return RedirectToAction("Admin");
        }


        [HttpPost]
        [Authorize]
        public ActionResult UpdateBan(BanViewModel banModel)
        {
            BanDto _banDto = new BanDto();

            _banDto.ID = banModel.viewModel.ID;
            _banDto.Login = banModel.viewModel.User_Login;
            _banDto.Description = banModel.viewModel.Description;
            _banDto.Finish_Date = banModel.viewModel.FinishDate;

            _banService.Update(_banDto);

            return RedirectToAction("Admin");
        }

        [HttpGet]
        [Authorize]
        public ActionResult DeleteBan(int id)
        {
            _banService.Delete(id);
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
                DestructionTime = x.DestructionTime
            }).ToList();

            return View(buildingModel);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Add(BuildingViewModel buildingModel)
        {
            BuildingDto _buildingDto = new BuildingDto();

            _buildingDto.Name = buildingModel.viewModel.BuildingName;
            _buildingDto.Price = buildingModel.viewModel.Price;
            _buildingDto.Height = buildingModel.viewModel.Height;
            _buildingDto.Width = buildingModel.viewModel.Width;
            _buildingDto.Dest_price = (int)buildingModel.viewModel.DestructionTime;
            _buildingDto.Percent_price_per_lvl = buildingModel.viewModel.Percent_price_per_lvl;
            _buildingDto.Percent_product_per_lvl = buildingModel.viewModel.Percent_product_per_lvl;
            _buildingDto.Product_ID = buildingModel.viewModel.Product_ID;
            _buildingDto.Product_Name = buildingModel.viewModel.Product_Name;
            _buildingDto.Product_per_sec = buildingModel.viewModel.Product_per_sec;
            _buildingDto.Alias = buildingModel.viewModel.Alias;
            _buildingDto.BuildingTime = buildingModel.viewModel.BuildingTime;
            _buildingDto.DestructionTime = buildingModel.viewModel.DestructionTime;

            _buildingService.Add(_buildingDto);

            return RedirectToAction("Admin");
        }

        [HttpPost]
        [Authorize]
        public ActionResult Update(BuildingViewModel buildingModel)
        {
            BuildingDto _buildingDto = new BuildingDto();

            _buildingDto.ID = buildingModel.viewModel.ID;
            _buildingDto.Name = buildingModel.viewModel.BuildingName;
            _buildingDto.Price = buildingModel.viewModel.Price;
            _buildingDto.Height = buildingModel.viewModel.Height;
            _buildingDto.Width = buildingModel.viewModel.Width;
            _buildingDto.Dest_price = (int)buildingModel.viewModel.DestructionTime;
            _buildingDto.Percent_price_per_lvl = buildingModel.viewModel.Percent_price_per_lvl;
            _buildingDto.Percent_product_per_lvl = buildingModel.viewModel.Percent_product_per_lvl;
            _buildingDto.Product_ID = buildingModel.viewModel.Product_ID;
            _buildingDto.Product_Name = buildingModel.viewModel.Product_Name;
            _buildingDto.Product_per_sec = buildingModel.viewModel.Product_per_sec;
            _buildingDto.Alias = buildingModel.viewModel.Alias;
            _buildingDto.BuildingTime = buildingModel.viewModel.BuildingTime;
            _buildingDto.DestructionTime = buildingModel.viewModel.DestructionTime;

            _buildingService.Add(_buildingDto);

            return RedirectToAction("Admin");
        }

        [HttpGet]
        [Authorize]
        public ActionResult DeleteBuilding(int id)
        {
            _buildingService.Delete(id);
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
                Password = x.Password,
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
            UserDto _userDto = new UserDto();

            _userDto.Login = userModel.viewModel.User_Login;
            _userDto.Password = userModel.viewModel.Password;
            _userDto.Email = userModel.viewModel.Email;
            _userDto.Last_Log = userModel.viewModel.LastLog;
            _userDto.Registration_Date = userModel.viewModel.RegistrationDate;
            _userDto.Last_Update = userModel.viewModel.LastUpdate;

            _userService.Add(_userDto);

            return RedirectToAction("Admin");
        }

        [HttpPost]
        [Authorize]
        public ActionResult UpdateUser(UserViewModel userModel)
        {
            UserDto _userDto = new UserDto();

            _userDto.ID = userModel.viewModel.ID;
            _userDto.Login = userModel.viewModel.User_Login;
            _userDto.Password = userModel.viewModel.Password;
            _userDto.Email = userModel.viewModel.Email;
            _userDto.Last_Log = userModel.viewModel.LastLog;
            _userDto.Registration_Date = userModel.viewModel.RegistrationDate;
            _userDto.Last_Update = userModel.viewModel.LastUpdate;

            _userService.Update(_userDto);

            return RedirectToAction("Admin");
        }

        [HttpGet]
        [Authorize]
        public ActionResult DeleteUser(int id)
        {
            _userService.Delete(id);
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
            DolarDto _dolarDto = new DolarDto();

            _dolarDto.User_ID = dolarModel.viewModel.User_ID;
            _dolarDto.Login = dolarModel.viewModel.User_Login;
            _dolarDto.Value = dolarModel.viewModel.DolarValue;

            _dolarService.Add(_dolarDto);

            return RedirectToAction("Admin");
        }

        [HttpPost]
        [Authorize]
        public ActionResult UpdateDolar(DolarViewModel dolarModel)
        {
            DolarDto _dolarDto = new DolarDto();

            _dolarDto.ID = dolarModel.viewModel.ID;
            _dolarDto.User_ID = dolarModel.viewModel.User_ID;
            _dolarDto.Login = dolarModel.viewModel.User_Login;
            _dolarDto.Value = dolarModel.viewModel.DolarValue;

            _dolarService.Update(_dolarDto);

            return RedirectToAction("Admin");
        }

        [HttpGet]
        [Authorize]
        public ActionResult DeleteDolar(int id)
        {
            _dolarService.Delete(id);
            return RedirectToAction("Admin");
        }
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
            MapDto _mapDto = new MapDto();

            _mapDto.User_ID = mapModel.viewModel.User_ID;
            _mapDto.Height = mapModel.viewModel.Height;
            _mapDto.Width = mapModel.viewModel.Width;

            _mapService.Add(_mapDto);

            return RedirectToAction("Admin");
        }

        [HttpPost]
        [Authorize]
        public ActionResult UpdateMap(MapViewModel mapModel)
        {
            MapDto _mapDto = new MapDto();

            _mapDto.Map_ID = mapModel.viewModel.ID;
            _mapDto.User_ID = mapModel.viewModel.User_ID;
            _mapDto.Height = mapModel.viewModel.Height;
            _mapDto.Width = mapModel.viewModel.Width;

            _mapService.Add(_mapDto);

            return RedirectToAction("Admin");
        }

        [HttpGet]
        [Authorize]
        public ActionResult DeleteMap(int id)
        {
            _mapService.Delete(id);
            return RedirectToAction("Admin");
        }
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
                TypeOffer = x.TypeOffer,
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
            MarketDto _marketDto = new MarketDto();

            _marketDto.Login = marketModel.viewModel.User_Login;
            _marketDto.Product_Name = marketModel.viewModel.Product_Name;
            _marketDto.Number = marketModel.viewModel.Number;
            _marketDto.Price = marketModel.viewModel.Price;
            _marketDto.TypeOffer = marketModel.viewModel.TypeOffer;

            _marketService.Add(_marketDto);

            return RedirectToAction("Admin");
        }

        [HttpPost]
        [Authorize]
        public ActionResult UpdateMarket(MarketViewModel marketModel)
        {
            MarketDto _marketDto = new MarketDto();

            _marketDto.ID = marketModel.viewModel.ID;
            _marketDto.Login = marketModel.viewModel.User_Login;
            _marketDto.Product_Name = marketModel.viewModel.Product_Name;
            _marketDto.Number = marketModel.viewModel.Number;
            _marketDto.Price = marketModel.viewModel.Price;
            _marketDto.TypeOffer = marketModel.viewModel.TypeOffer;

            _marketService.Update(_marketDto);

            return RedirectToAction("Admin");
        }


        [HttpGet]
        [Authorize]
        public ActionResult DeleteMarket(int id)
        {
            _marketService.Delete(id);
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
            ProductDto _productDto = new ProductDto();

            _productDto.Name = productModel.viewModel.ProductName;
            _productDto.Price_per_unit = productModel.viewModel.Price_per_unit;
            _productDto.Unit = productModel.viewModel.Unit;
            _productDto.Alias = productModel.viewModel.Alias;

            _productService.Add(_productDto);

            return RedirectToAction("Admin");
        }

        [HttpPost]
        [Authorize]
        public ActionResult UpdateProduct(ProductViewModel productModel)
        {
            ProductDto _productDto = new ProductDto();

            _productDto.ID = productModel.viewModel.ID;
            _productDto.Name = productModel.viewModel.ProductName;
            _productDto.Price_per_unit = productModel.viewModel.Price_per_unit;
            _productDto.Unit = productModel.viewModel.Unit;
            _productDto.Alias = productModel.viewModel.Alias;

            _productService.Add(_productDto);

            return RedirectToAction("Admin");
        }

        [HttpGet]
        [Authorize]
        public ActionResult DeleteProduct(int id)
        {
            _productService.Delete(id);
            return RedirectToAction("Admin");
        }

        #endregion

        #region UserBuildingTable
        [Authorize]
        public ActionResult _UserBuildingList()
        {
            UserBuildingsViewModel userBuildingModel = new UserBuildingsViewModel();
            userBuildingModel.listModel = _userBuildingService.GetUserBuilding().Where(i => !i.Owner).Select(x => new ItemUserBuildingViewModel
            {
                ID = x.ID,
                User_ID = x.User_ID,
                User_Login = x.Login,
                X_pos = x.X_pos,
                Y_pos = x.Y_pos,
                Lvl = x.Lvl,
                Building_ID = x.Building_ID,
                Building_Name = x.Building_Name,
                Status = x.Status
            }).ToList();

            return View(userBuildingModel);
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddUserBuilding(UserBuildingsViewModel userBuildingModel)
        {
            UserBuildingDto _userBuildingDto = new UserBuildingDto();

            _userBuildingDto.User_ID = userBuildingModel.viewModel.User_ID;
            _userBuildingDto.Login = userBuildingModel.viewModel.User_Login;
            _userBuildingDto.X_pos = userBuildingModel.viewModel.X_pos;
            _userBuildingDto.Y_pos = userBuildingModel.viewModel.Y_pos;
            _userBuildingDto.Lvl = userBuildingModel.viewModel.Lvl;
            _userBuildingDto.Building_ID = userBuildingModel.viewModel.Building_ID;
            _userBuildingDto.Building_Name = userBuildingModel.viewModel.Building_Name;
            _userBuildingDto.Status = userBuildingModel.viewModel.Status;

            _userBuildingService.Add(_userBuildingDto);

            return RedirectToAction("Admin");
        }

        [HttpPost]
        [Authorize]
        public ActionResult UpdateUserBuilding(UserBuildingsViewModel userBuildingModel)
        {
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

            _userBuildingService.Update(_userBuildingDto);

            return RedirectToAction("Admin");
        }


        [HttpGet]
        [Authorize]
        public ActionResult DeleteUserBuilding(int id)
        {
            _userBuildingService.Delete(id);
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

            return View(userProductModel);
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddUserProduct(UserProductViewModel userProductModel)
        {
            UserProductDto _userProductDto = new UserProductDto();

            _userProductDto.Login = userProductModel.viewModel.User_Login;
            _userProductDto.Product_Name = userProductModel.viewModel.Product_Name;
            _userProductDto.Value = userProductModel.viewModel.Value;

            _userProductService.Add(_userProductDto);

            return RedirectToAction("Admin");
        }

        [HttpPost]
        [Authorize]
        public ActionResult UpdateUserProduct(UserProductViewModel userProductModel)
        {
            UserProductDto _userProductDto = new UserProductDto();

            _userProductDto.ID = userProductModel.viewModel.ID;
            _userProductDto.ID = userProductModel.viewModel.ID;
            _userProductDto.Login = userProductModel.viewModel.User_Login;
            _userProductDto.Product_Name = userProductModel.viewModel.Product_Name;
            _userProductDto.Value = userProductModel.viewModel.Value;

            _userProductService.Update(_userProductDto);

            return RedirectToAction("Admin");
        }

        [HttpGet]
        [Authorize]
        public ActionResult DeleteUserProduct(int id)
        {
            _userProductService.Delete(id);
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
            BuildingQueueDto _queueDto = new BuildingQueueDto();

            _queueDto.Login = queueModel.viewModel.User_Login;
            _queueDto.UserBuilding_ID = queueModel.viewModel.UserBuilding_ID;
            _queueDto.NewStatus = queueModel.viewModel.NewStatus;
            _queueDto.FinishTime = DateTime.Now.AddSeconds(queueModel.viewModel.Second);

            _queueService.Add(_queueDto);

            return RedirectToAction("Admin");
        }

        [HttpPost]
        [Authorize]
        public ActionResult UpdateQueue(QueueViewModel queueModel)
        {
            BuildingQueueDto _buildingQueueDto = new BuildingQueueDto();

            _buildingQueueDto.ID = queueModel.viewModel.ID;
            _buildingQueueDto.ID = queueModel.viewModel.ID;
            _buildingQueueDto.Login = queueModel.viewModel.User_Login;

            _queueService.Update(_buildingQueueDto);

            return RedirectToAction("Admin");
        }

        [HttpGet]
        [Authorize]
        public ActionResult DeleteQueue(int id)
        {
            _queueService.Delete(id);
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
                IsActive = x.IsActive,
                Value = x.Map_ID,
                Percent_User1 = x.Percent_User1,
                Percent_User2 = x.Percent_User2,
                User1_Login = x.User1_Login,
                User2_Login = x.User2_Login,
            }).ToList();

            dealModel.buildingList = _buildingService.GetBuilding().Select(x => x.Alias).ToArray();
            dealModel.userList = _userService.GetUser().Select(x => x.Login).ToArray();
            return View(dealModel);
        }

        [Authorize]
        public ActionResult AddDeal(DealViewModel dealModel)
        {
            DealDto dealDto = new DealDto();

            dealDto.Building_Name = dealModel.viewModel.Building_Name;
            dealDto.DayTime = dealModel.viewModel.DealDay;
            dealDto.IsActive = dealModel.viewModel.IsActive;
            dealDto.Map_ID = dealModel.viewModel.Value;
            dealDto.Percent_User1 = dealModel.viewModel.Percent_User1;
            dealDto.Percent_User2 = dealModel.viewModel.Percent_User2;
            dealDto.User2_Login = dealModel.viewModel.User2_Login;
            dealDto.User1_Login = dealModel.viewModel.User1_Login;

            _dealService.AddDealAdmin(dealDto);

            return RedirectToAction("Admin");
        }

        [Authorize]
        public ActionResult UpdateDeal(DealViewModel dealModel)
        {
            DealDto dealDto = new DealDto();

            dealDto.ID = dealModel.viewModel.ID;
            dealDto.Building_Name = dealModel.viewModel.Building_Name;
            dealDto.DayTime = dealModel.viewModel.DealDay;
            dealDto.IsActive = dealModel.viewModel.IsActive;
            dealDto.Map_ID = dealModel.viewModel.Value;
            dealDto.Percent_User1 = dealModel.viewModel.Percent_User1;
            dealDto.Percent_User2 = dealModel.viewModel.Percent_User2;
            dealDto.User2_Login = dealModel.viewModel.User2_Login;
            dealDto.User1_Login = dealModel.viewModel.User1_Login;

            _dealService.UpdateDealAdmin(dealDto);

            return RedirectToAction("Admin");
        }

        [Authorize]
        public ActionResult DeleteDeal(int id)
        {
            _dealService.DeleteDealAdmin(id);
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
            DealBuildingDto dealbDto = new DealBuildingDto();

            dealbDto.BuildingName = dealbModel.viewModel.Building_Name;
            dealbDto.Login = dealbModel.viewModel.User_Login;
            dealbDto.Deal_ID = dealbModel.viewModel.Deal_ID;

            _dealService.UpdateDealBuildingAdmin(dealbDto);

            return RedirectToAction("Admin");
        }

        [Authorize]
        public ActionResult UpdateDealBuilding(DealBuildingViewModel dealbModel)
        {
            DealBuildingDto dealbDto = new DealBuildingDto();

            dealbDto.ID = dealbModel.viewModel.ID;
            dealbDto.Building_ID = dealbModel.viewModel.Building_ID;
            dealbDto.User_ID = dealbModel.viewModel.User_ID;
            dealbDto.Deal_ID = dealbModel.viewModel.Deal_ID;

            _dealService.UpdateDealBuildingAdmin(dealbDto);

            return RedirectToAction("Admin");
        }

        [Authorize]
        public ActionResult DeleteDealBuilding(int id)
        {
            _dealService.DeleteDealBuildingAdmin(id);

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
                OrAccepted = x.OrAccepted,
                User_ID = x.User_ID,
                User_Login = x.User_Login
            }).ToList();

            friendModel.allUser = _userService.GetUser().Select(x => x.Login).ToArray();

            return View(friendModel);
        }

        [Authorize]
        public ActionResult AddFriendAdmin(FriendViewModel friendModel)
        {
            FriendDto friendDto = new FriendDto();

            friendDto.ID = friendModel.viewModel.ID;
            friendDto.Friend_Login = friendModel.viewModel.Friend_Login;
            friendDto.User_Login = friendModel.viewModel.User_Login;
            friendDto.OrAccepted = friendModel.viewModel.OrAccepted;

            _userService.AddFriendAdmin(friendDto);

            return RedirectToAction("Admin");
        }

        [Authorize]
        public ActionResult UpdateFriendAdmin(FriendViewModel friendModel)
        {
            FriendDto friendDto = new FriendDto();

            friendDto.Friend_ID = friendModel.viewModel.Friend_ID;
            friendDto.User_ID = friendModel.viewModel.User_ID;
            friendDto.OrAccepted = friendModel.viewModel.OrAccepted;

            _userService.UpdateFriendAdmin(friendDto);

            return RedirectToAction("Admin");
        }

        [Authorize]
        public ActionResult DeleteFriendAdmin(int id)
        {
            _userService.DeleteFriendAdmin(id);

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
            IgnoredDto ignoredDto = new IgnoredDto();


            ignoredDto.Ignored_Login = ignoredModel.viewModel.Ignored_Login;
            ignoredDto.User_Login = ignoredModel.viewModel.User_Login;

            _userService.AddIgnoredAdmin(ignoredDto);

            return RedirectToAction("Admin");
        }

        [Authorize]
        public ActionResult UpdateIgnored(IgnoredViewModel ignoredModel)
        {
            IgnoredDto ignoredDto = new IgnoredDto();

            ignoredDto.ID = ignoredModel.viewModel.ID;
            ignoredDto.Ignored_ID = ignoredModel.viewModel.Ignored_ID;
            ignoredDto.User_ID = ignoredModel.viewModel.User_ID;

            _userService.UpdateIgnoredAdmin(ignoredDto);

            return RedirectToAction("Admin");
        }

        [Authorize]
        public ActionResult DeleteIgnored(int id)
        {
            _userService.DeleteIgnoredAdmin(id);

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
                IfRead = x.IfRead,
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
            MessageDto messageDto = new MessageDto();

            messageDto.Content = messageModel.viewModel.Content;
            messageDto.Customer_Login = messageModel.viewModel.Customer_Login;
            messageDto.Sender_Login = messageModel.viewModel.Sender_Login;
            messageDto.Theme = messageModel.viewModel.Theme;
            messageDto.PostDate = messageModel.viewModel.PostDate;
            messageDto.IfRead = messageModel.viewModel.IfRead;

            _messageService.AddMessageAdmin(messageDto);

            return RedirectToAction("Admin");
        }

        [Authorize]
        public ActionResult UpdateMessageAdmin(MessageViewModel messageModel)
        {
            MessageDto messageDto = new MessageDto();

            messageDto.ID = messageModel.viewModel.ID;
            messageDto.Content = messageModel.viewModel.Content;
            messageDto.Customer_ID = messageModel.viewModel.Customer_ID;
            messageDto.Sender_ID = messageModel.viewModel.Sender_ID;
            messageDto.Theme = messageModel.viewModel.Theme;
            messageDto.PostDate = messageModel.viewModel.PostDate;
            messageDto.IfRead = messageModel.viewModel.IfRead;

            _messageService.UpdateMessageAdmin(messageDto);

            return RedirectToAction("Admin");
        }

        [Authorize]
        public ActionResult DeleteMessageAdmin(int id)
        {
            _messageService.DeleteMessageAdmin(id);

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
            ProductRequirementDto productrDto = new ProductRequirementDto();

            productrDto.Base_Name = productrMode.viewModel.BaseName;
            productrDto.Require_Name = productrMode.viewModel.RequireName;
            productrDto.Value = productrMode.viewModel.Value;

            _productRService.AddProduct(productrDto);

            return RedirectToAction("Admin");
        }

        [Authorize]
        public ActionResult UpdateProductRequirementAdmin(ProductRequirementViewModel productrMode)
        {
            ProductRequirementDto productrDto = new ProductRequirementDto();

            productrDto.ID = productrMode.viewModel.ID;
            productrDto.Base_ID = productrMode.viewModel.Base_ID;
            productrDto.Require_ID = productrMode.viewModel.Require_ID;
            productrDto.Value = productrMode.viewModel.Value;

            _productRService.UpdateProduct(productrDto);

            return RedirectToAction("Admin");
        }

        [Authorize]
        public ActionResult DeleteProductRequirementAdmin(int id)
        {
            _productRService.DeleteProduct(id);

            return RedirectToAction("Admin");
        }
        #endregion
    }
}
