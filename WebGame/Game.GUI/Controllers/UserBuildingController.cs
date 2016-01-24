using Game.Core.DTO;
using Game.GUI.ViewModels.Building;
using Game.GUI.ViewModels.Building.UserBuildings;
using Game.Service.Interfaces;
using Game.Service.Interfaces.TableInterface;
using PagedList;
using System.Linq;
using System.Web.Mvc;

namespace Game.GUI.Controllers
{
    public class UserBuildingController : Controller
    {
        private IUserBuildingService _userBuildingService;
        private IDolarService _dolar;

        public UserBuildingController(IUserBuildingService userBuildingService, IDolarService dolar)
        {
            _userBuildingService = userBuildingService;
            _dolar = dolar;
        }

        // GET: UserBuilding
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }


        [Authorize]
        public ActionResult _DealBuildings()
        {
            BuildingViewModel buildingList = new BuildingViewModel();

            buildingList.listModel = _userBuildingService.GetDealBuildingList(User.Identity.Name).Select(x => new ItemBuildingViewModel
            {
                ID = x.ID,
                BuildingName = x.Alias,
                Height = x.Height,
                Width = x.Width,
                DealID = x.Deal_ID,
            }).ToList();


            buildingList.UserDolar = _dolar.UserDolar(User.Identity.Name);

            return View("~/Views/Building/_DealBuildings.cshtml", buildingList);
        }


        [HttpGet]
        [Authorize]
        public ActionResult LvlUp(int id)
        {
            _userBuildingService.LvlUp(id, User.Identity.Name);
            return View("~/Views/Office/Index.cshtml");
        }

        [Authorize]
        public void ChangeColor(string color, int id)
        {
            _userBuildingService.ChangeColor(color, id, User.Identity.Name);
        }

    }
}