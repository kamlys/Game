using Game.Core.DTO;
using Game.GUI.ViewModels;
using Game.Service.Interfaces;
using Game.Service.Interfaces.TableInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
        public ActionResult _UserBuildingList()
        {
            ListTableViewModel tableList = new ListTableViewModel();
            tableList.tableList = new List<TableViewModel>();

            foreach (var item in _userBuildingService.GetUserBuilding())
            {
                if(!item.Owner)
                {
                    continue;
                }
                tableList.tableList.Add(new TableViewModel
                {
                    ID = item.ID,
                    User_ID = item.User_ID,
                    Login = item.Login,
                    X_pos = item.X_pos,
                    Y_pos = item.Y_pos,
                    Lvl = item.Lvl,
                    Building_ID = item.Building_ID,
                    Name = item.Building_Name,
                    Status = item.Status
                });
            }


            return View("~/Views/Admin/_UserBuildingList.cshtml", tableList);
        }

        [Authorize]
        public ActionResult _DealBuildings()
        {
            ListTableViewModel tableList = new ListTableViewModel();
            tableList.tableList = new List<TableViewModel>();
            tableList.tableView = new TableViewModel();

            foreach (var item in _userBuildingService.GetDealBuildingList(User.Identity.Name))
            {
                tableList.tableList.Add(new TableViewModel
                {
                    ID = item.ID,
                    Alias = item.Building_Name
                });
            }

            tableList.tableView.Value = _dolar.UserDolar(User.Identity.Name);

            return View("~/Views/Building/_DealBuildings.cshtml", tableList);
        }


        [HttpPost]
        [Authorize]
        public ActionResult Add(ListTableViewModel listView)
        {
            UserBuildingDto _userBuildingDto = new UserBuildingDto();

            _userBuildingDto.User_ID = listView.tableView.User_ID;
            _userBuildingDto.Login = listView.tableView.Login;
            _userBuildingDto.X_pos = listView.tableView.X_pos;
            _userBuildingDto.Y_pos = listView.tableView.Y_pos;
            _userBuildingDto.Lvl = listView.tableView.Lvl;
            _userBuildingDto.Building_ID = listView.tableView.Building_ID;
            _userBuildingDto.Building_Name = listView.tableView.Name;
            _userBuildingDto.Status = listView.tableView.Status;

            _userBuildingService.Add(_userBuildingDto);

            return View("~/Views/Admin/Admin.cshtml");
        }

        [HttpPost]
        [Authorize]
        public ActionResult Update(ListTableViewModel listView)
        {
            UserBuildingDto _userBuildingDto = new UserBuildingDto();

            _userBuildingDto.ID = listView.tableView.ID;
            _userBuildingDto.User_ID = listView.tableView.User_ID;
            _userBuildingDto.Login = listView.tableView.Login;
            _userBuildingDto.X_pos = listView.tableView.X_pos;
            _userBuildingDto.Y_pos = listView.tableView.Y_pos;
            _userBuildingDto.Lvl = listView.tableView.Lvl;
            _userBuildingDto.Building_ID = listView.tableView.Building_ID;
            _userBuildingDto.Building_Name = listView.tableView.Name;
            _userBuildingDto.Status = listView.tableView.Status;

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
    }
}