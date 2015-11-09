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
        private IBuildingHelper _buildingsHelper;
        private IProductService _productService;

        public MapController(IMapService mapService, IBuildingHelper buildings, IProductService productService)
        {
            _mapService = mapService;
            _buildingsHelper = buildings;
            _productService = productService;
        }
        // GET: Building
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _UserMap()
        {
            _productService.UpdateUserProduct(User.Identity.Name);
            MapViewModel vm = new MapViewModel();
            vm.Map = _mapService.GetMap(User.Identity.Name);
            var ub = _buildingsHelper.GetBuildings(User.Identity.Name);
            List<UserBuildingsViewModel> ubv = new List<UserBuildingsViewModel>();

            foreach (var a in ub)
            {
                var building = _buildingsHelper.GetBuildings().Where(b => b.ID == a.Building_ID).First();
                ubv.Add(new UserBuildingsViewModel {
                    BuildingID = a.Building_ID,
                    level = a.Lvl,
                    name = building.Name,
                    x_left = a.X_pos,
                    x_right = a.X_pos + building.Width - 1,
                    y_top = a.Y_pos,
                    y_bottom = a.Y_pos + building.Height - 1,
                    ID = a.ID
                });
            }
            vm.UserBuildings = ubv;
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            vm.UserProducts = serializer.Serialize(_buildingsHelper.AddProductValue(User.Identity.Name));
            return View(vm);
        }
    }
}