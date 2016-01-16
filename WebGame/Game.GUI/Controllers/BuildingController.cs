using Game.Core.DTO;
using Game.GUI.ViewModels;
using Game.GUI.ViewModels.Building;
using Game.Service;
using Game.Service.Interfaces;
using Game.Service.Interfaces.TableInterface;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Game.GUI.Controllers
{
    public class BuildingController : Controller
    {
        private IBuildingHelper _buildingService;
        private IUserBuildingService _userBuilding;
        private IBuildingService _buildingTable;
        private IDolarService _dolar;


        public BuildingController(IBuildingHelper buildingService, IUserBuildingService userBuilding, IBuildingService buildingTable, IDolarService dolar)
        {
            _buildingService = buildingService;
            _userBuilding = userBuilding;
            _buildingTable = buildingTable;
            _dolar = dolar;
        }
        // GET: Building
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult _BuildingsList()
        {
            BuildingViewModel buildingList = new BuildingViewModel();

            buildingList.listModel = _buildingService.GetBuildings().Select(x => new ItemBuildingViewModel
            {
                ID = x.ID,
                BuildingName = x.Alias,
                Height = x.Height,
                Width = x.Width,
                Price = x.Price
            }).ToList();

            buildingList.UserDolar = _dolar.UserDolar(User.Identity.Name);

            return View(buildingList);
        }


        [Authorize]
        public ActionResult _BuildingList2(int? Page_No)
        {
            BuildingViewModel buildingModel = new BuildingViewModel();
            int Size_Of_Page = 10;
            int No_Of_Page = (Page_No ?? 1);
            buildingModel.pagedModel = _buildingTable.GetBuilding().Select(x => new ItemBuildingViewModel
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
            }).ToList().ToPagedList(No_Of_Page, Size_Of_Page);

            return View("~/Views/Admin/_BuildingList2.cshtml", buildingModel);
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

            _buildingTable.Add(_buildingDto);

            return View("~/Views/Admin/Admin.cshtml");
        }

        [HttpPost]
        [Authorize]
        public ActionResult Update(BuildingViewModel buildingModel)
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

            _buildingTable.Add(_buildingDto);

            return View("~/Views/Admin/Admin.cshtml");
        }

        [HttpGet]
        [Authorize]
        public ActionResult Delete(int id)
        {
            _buildingTable.Delete(id);
            return View("~/Views/Admin/Admin.cshtml");
        }

    }
}