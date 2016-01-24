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
    }
}