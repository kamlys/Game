﻿using Game.Core.DTO;
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
            ListTableViewModel tableList = new ListTableViewModel();
            tableList.tableList = new List<TableViewModel>();
            tableList.tableView = new TableViewModel();

            foreach (var item in _buildingService.GetBuildings())
            {
                tableList.tableList.Add(new TableViewModel
                {
                    ID = item.ID,
                    Alias = item.Alias,
                    Height = item.Height,
                    Width = item.Width,
                    Price = item.Price
                });
            }

            tableList.tableView.Value = _dolar.UserDolar(User.Identity.Name);

            return View(tableList);
        }


        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
        public ActionResult Delete(int id)
        {
            _buildingTable.Delete(id);
            return View("~/Views/Admin/Admin.cshtml");
        }

    }
}