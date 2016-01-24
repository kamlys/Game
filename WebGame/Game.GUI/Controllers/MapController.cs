using Game.Core.DTO;
using Game.GUI.ViewModels.Map;
using Game.Service.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PagedList;
using Game.GUI.ViewModels.Building.UserBuildings;

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
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult _UserMap()
        {
            _productService.UpdateUserProduct(User.Identity.Name);
            MapViewModel vm = new MapViewModel();
            vm.Map = _mapService.GetMap(User.Identity.Name);
            var ub = _buildingsHelper.GetBuildings(User.Identity.Name);
            List<UserBuildingsViewModel> ubv = new List<UserBuildingsViewModel>();

            foreach (var a in ub)
            {
                if (!a.Owner)
                {
                    continue;
                }
                var building = _buildingsHelper.GetBuildings().Where(b => b.ID == a.Building_ID).First();
                var bTime = 0;
                if(a.Status == "budowa" || a.Status == "rozbudowa")
                {
                    bTime = building.BuildingTime;
                }
                else if (a.Status == "burzenie")
                {
                    bTime = building.DestructionTime;
                }
                ubv.Add(new UserBuildingsViewModel {
                    id = a.ID,
                    BuildingID = a.Building_ID,
                    level = a.Lvl,
                    name = building.Name,
                    x_left = a.X_pos,
                    x_right = a.X_pos + building.Width - 1,
                    y_top = a.Y_pos,
                    y_bottom = a.Y_pos + building.Height - 1,
                    ID = a.ID,
                    Status = a.Status,
                    BuildTime = bTime,
                    BuildDone = bTime - _buildingsHelper.BuildingTimeLeft(User.Identity.Name, a.ID),
                    Alias = building.Alias,
                    Color = a.Color
                });
            }
            vm.UserBuildings = ubv;
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            vm.UserProducts = serializer.Serialize(_buildingsHelper.AddProductValue(User.Identity.Name));
            var abc = _buildingsHelper.ProductNames(User.Identity.Name);
            var names = serializer.Serialize(abc);
            vm.ProductNames = names;
            var buildingsArray = new int[ub.Count][];
            int i = 0;
            foreach(var a in ubv)
            {
                var building = new int[4] { a.x_left, a.x_right, a.y_top, a.y_bottom };
                buildingsArray[i] = building;
                i++;
            }
            if(buildingsArray != null)
            vm.BuildingsArray = serializer.Serialize(buildingsArray);
            return View(vm);
        }
    }
}