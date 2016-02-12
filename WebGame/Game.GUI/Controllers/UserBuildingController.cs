using Game.Core.DTO;
using Game.GUI.ViewModels.Building;
using Game.GUI.ViewModels.Building.UserBuildings;
using Game.Service.Interfaces;
using Game.Service.Interfaces.TableInterface;
using PagedList;
using System.Collections.Generic;
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
            List<string> errors;
            if (Session["val"] != null)
            {
                errors = ((string[])Session["val"]).ToList();
            }
            else
            {
                errors = new List<string>();
            }
            if (!_userBuildingService.LvlUp(id, User.Identity.Name))
            {
                errors.Add("Coś poszło nie tak.");
                Session["val"] = errors.ToArray<string>();
            }

            return RedirectToAction("Index","Office");
        }

        [Authorize]
        public void ChangeColor(string color, int id)
        {
            _userBuildingService.ChangeColor(color, id, User.Identity.Name);
        }

    }
}