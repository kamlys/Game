using Game.Core.DTO;
using Game.GUI.ViewModels.Map;
using Game.Service;
using Game.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Game.GUI.ViewModels.UserBuildings;

namespace Game.GUI.Controllers
{
    public class MapController : Controller
    {
        private IMapService _mapService;
        private IUserBuildingService _userBuildingsService;
        private IBuildingService _buildingsService;

        public MapController(IMapService mapService, IUserBuildingService userBuildingService, IBuildingService buildings)
        {
            _mapService = mapService;
            _userBuildingsService = userBuildingService;
            _buildingsService = buildings;
        }
        // GET: Building
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _UserMap()
        {
            MapViewModel vm = new MapViewModel();
            vm.Map = _mapService.GetMap(User.Identity.Name);
            var ub = _userBuildingsService.GetBuildings(User.Identity.Name);
            List<UserBuildingsViewModel> ubv = new List<UserBuildingsViewModel>();

            foreach (var a in ub)
            {
                var building = _buildingsService.GetBuildings().Where(b => b.ID == a.Building_ID).First();
                ubv.Add(new UserBuildingsViewModel{
                    BuildingID = a.Building_ID,
                    level = a.Lvl,
                    name = building.Name,
                    x_left = a.X_pos,
                    x_right = a.X_pos + building.Width - 1,
                    y_top = a.Y_pos,
                    y_bottom = a.Y_pos + building.Height - 1
                });
            }
            vm.UserBuildings = ubv;
            return View(vm);
        }
    }
}