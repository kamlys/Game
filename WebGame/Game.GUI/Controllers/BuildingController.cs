using Game.Core.DTO;
using Game.Service;
using Game.Service.Interfaces;
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

        public BuildingController(IBuildingHelper buildingService)
        {
            _buildingService = buildingService;
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

        public ActionResult _UserBuildingsList()
        {
            List<UserBuildingDto> userBuildigns = _buildingService.GetUserBuildings(User.Identity.Name);
            return View(userBuildigns);
        }

    }
}