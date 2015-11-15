using Game.Core.DTO;
using Game.GUI.ViewModels;
using Game.Service;
using Game.Service.Interfaces;
using Game.Service.Interfaces.TableInterface;
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
        private IBuildingTableService _buildingTable;


        public BuildingController(IBuildingHelper buildingService, IUserBuildingService userBuilding, IBuildingTableService buildingTable)
        {
            _buildingService = buildingService;
            _userBuilding = userBuilding;
            _buildingTable = buildingTable;
        }
        // GET: Building
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _BuildingsList()
        {
            List<BuildingDto> buildings = _buildingService.GetBuildings();
            return View(buildings);
        }


        public ActionResult _BuildingList2()
        {
            List<TableViewModel> adminViewModel = new List<TableViewModel>();

            foreach (var item in _buildingTable.GetBuilding())
            {
                adminViewModel.Add(new TableViewModel
                {
                    Name = item.Name,
                    Price = item.Price,
                    ID = item.ID,
                    Height = item.Height,
                    Width = item.Width,
                    Dest_price = (int)item.Dest_price,
                    Percent_price_per_lvl = item.Percent_price_per_lvl,
                    Percent_product_per_lvl = item.Percent_product_per_lvl,
                    Product_ID = item.Product_ID,
                    Product_per_sec = item.Product_per_sec,
                    Alias = item.Alias,
                    BuildingTime = item.BuildingTime,
                    DestructionTime = item.DestructionTime
                });
            }


            return View("~/Views/Admin/_BuildingList2.cshtml", adminViewModel);
        }

        [HttpPost]
        public ActionResult Add(string Name,
            int Price,
            int Height,
            int Width,
            int Dest_price,
            int Percent_price_per_lvl,
            int Percent_product_per_lvl,
            int Product_ID,
            int Product_per_sec,
            string Alias,
            int BuildingTime,
            int DestructionTime)
        {
            BuildingDto _buildingDto = new BuildingDto();

            _buildingDto.Name = Name;
            _buildingDto.Price = Price;
            _buildingDto.Height = Height;
            _buildingDto.Width = Width;
            _buildingDto.Dest_price = (int)Dest_price;
            _buildingDto.Percent_price_per_lvl = Percent_price_per_lvl;
            _buildingDto.Percent_product_per_lvl = Percent_product_per_lvl;
            _buildingDto.Product_ID = Product_ID;
            _buildingDto.Product_per_sec = Product_per_sec;
            _buildingDto.Alias = Alias;
            _buildingDto.BuildingTime = BuildingTime;
            _buildingDto.DestructionTime = DestructionTime;


            _buildingTable.Add(_buildingDto);

            return View("~/Views/Admin/Admin.cshtml");
        }

    }
}