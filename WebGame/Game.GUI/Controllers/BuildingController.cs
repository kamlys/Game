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
        private IBuildingService _buildingTable;


        public BuildingController(IBuildingHelper buildingService, IUserBuildingService userBuilding, IBuildingService buildingTable)
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
            ListTableViewModel tableList = new ListTableViewModel();
            tableList.tableList = new List<TableViewModel>();


            foreach (var item in _buildingTable.GetBuilding())
            {
                tableList.tableList.Add(new TableViewModel
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
                    Product_Name = item.Product_Name,
                    Product_per_sec = item.Product_per_sec,
                    Alias = item.Alias,
                    BuildingTime = item.BuildingTime,
                    DestructionTime = item.DestructionTime
                });
            }


            return View("~/Views/Admin/_BuildingList2.cshtml", tableList);
        }

        [HttpPost]
        public ActionResult Add(ListTableViewModel viewModel)
        {
            BuildingDto _buildingDto = new BuildingDto();

            _buildingDto.Name = viewModel.tableView.Name;
            _buildingDto.Price = viewModel.tableView.Price;
            _buildingDto.Height = viewModel.tableView.Height;
            _buildingDto.Width = viewModel.tableView.Width;
            _buildingDto.Dest_price = (int)viewModel.tableView.Dest_price;
            _buildingDto.Percent_price_per_lvl = viewModel.tableView.Percent_price_per_lvl;
            _buildingDto.Percent_product_per_lvl = viewModel.tableView.Percent_product_per_lvl;
            _buildingDto.Product_ID = viewModel.tableView.Product_ID;
            _buildingDto.Product_Name = viewModel.tableView.Product_Name;
            _buildingDto.Product_per_sec = viewModel.tableView.Product_per_sec;
            _buildingDto.Alias = viewModel.tableView.Alias;
            _buildingDto.BuildingTime = viewModel.tableView.BuildingTime;
            _buildingDto.DestructionTime = viewModel.tableView.DestructionTime;

            _buildingTable.Add(_buildingDto);

            return View("~/Views/Admin/Admin.cshtml");
        }

        [HttpPost]
        public ActionResult Update(ListTableViewModel viewModel)
        {
            BuildingDto _buildingDto = new BuildingDto();

            _buildingDto.Name = viewModel.tableView.Name;
            _buildingDto.Price = viewModel.tableView.Price;
            _buildingDto.Height = viewModel.tableView.Height;
            _buildingDto.Width = viewModel.tableView.Width;
            _buildingDto.Dest_price = (int)viewModel.tableView.Dest_price;
            _buildingDto.Percent_price_per_lvl = viewModel.tableView.Percent_price_per_lvl;
            _buildingDto.Percent_product_per_lvl = viewModel.tableView.Percent_product_per_lvl;
            _buildingDto.Product_ID = viewModel.tableView.Product_ID;
            _buildingDto.Product_Name = viewModel.tableView.Product_Name;
            _buildingDto.Product_per_sec = viewModel.tableView.Product_per_sec;
            _buildingDto.Alias = viewModel.tableView.Alias;
            _buildingDto.BuildingTime = viewModel.tableView.BuildingTime;
            _buildingDto.DestructionTime = viewModel.tableView.DestructionTime;

            _buildingTable.Add(_buildingDto);

            return View("~/Views/Admin/Admin.cshtml");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            _buildingTable.Delete(id);
            return View("~/Views/Admin/Admin.cshtml");
        }

    }
}