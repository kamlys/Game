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
        public ActionResult _UserBuildingList(int? Page_No)
        {
            UserBuildingsViewModel userBuildingModel = new UserBuildingsViewModel();
            int Size_Of_Page = 10;
            int No_Of_Page = (Page_No ?? 1);
            userBuildingModel.pagedList = _userBuildingService.GetUserBuilding().Where(i => !i.Owner).Select(x => new ItemUserBuildingViewModel
            {
                ID = x.ID,
                User_ID = x.User_ID,
                User_Login = x.Login,
                X_pos = x.X_pos,
                Y_pos = x.Y_pos,
                Lvl = x.Lvl,
                Building_ID = x.Building_ID,
                Building_Name = x.Building_Name,
                Status = x.Status
            }).ToList().ToPagedList(No_Of_Page, Size_Of_Page);

            return View("~/Views/Admin/_UserBuildingList.cshtml", userBuildingModel);
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


        [HttpPost]
        [Authorize]
        public ActionResult Add(UserBuildingsViewModel userBuildingModel)
        {
            UserBuildingDto _userBuildingDto = new UserBuildingDto();

            _userBuildingDto.User_ID = userBuildingModel.viewModel.User_ID;
            _userBuildingDto.Login = userBuildingModel.viewModel.User_Login;
            _userBuildingDto.X_pos = userBuildingModel.viewModel.X_pos;
            _userBuildingDto.Y_pos = userBuildingModel.viewModel.Y_pos;
            _userBuildingDto.Lvl = userBuildingModel.viewModel.Lvl;
            _userBuildingDto.Building_ID = userBuildingModel.viewModel.Building_ID;
            _userBuildingDto.Building_Name = userBuildingModel.viewModel.Building_Name;
            _userBuildingDto.Status = userBuildingModel.viewModel.Status;

            _userBuildingService.Add(_userBuildingDto);

            return View("~/Views/Admin/Admin.cshtml");
        }

        [HttpPost]
        [Authorize]
        public ActionResult Update(UserBuildingsViewModel userBuildingModel)
        {
            UserBuildingDto _userBuildingDto = new UserBuildingDto();

            _userBuildingDto.ID = userBuildingModel.viewModel.ID;
            _userBuildingDto.User_ID = userBuildingModel.viewModel.User_ID;
            _userBuildingDto.Login = userBuildingModel.viewModel.User_Login;
            _userBuildingDto.X_pos = userBuildingModel.viewModel.X_pos;
            _userBuildingDto.Y_pos = userBuildingModel.viewModel.Y_pos;
            _userBuildingDto.Lvl = userBuildingModel.viewModel.Lvl;
            _userBuildingDto.Building_ID = userBuildingModel.viewModel.Building_ID;
            _userBuildingDto.Building_Name = userBuildingModel.viewModel.Building_Name;
            _userBuildingDto.Status = userBuildingModel.viewModel.Status;

            _userBuildingService.Update(_userBuildingDto);

            return View("~/Views/Admin/Admin.cshtml");
        }


        [HttpGet]
        [Authorize]
        public ActionResult Delete(int id)
        {
            _userBuildingService.Delete(id);
            return View("~/Views/Admin/Admin.cshtml");
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