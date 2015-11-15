using Game.Core.DTO;
using Game.GUI.ViewModels;
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
        private IUserBuildingTableService _userBuildingTable;

        public UserBuildingController(IUserBuildingTableService userBuildingTable)
        {
            _userBuildingTable = userBuildingTable;
        }

        // GET: UserBuilding
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _UserBuildingList()
        {
            List<TableViewModel> adminViewModel = new List<TableViewModel>();

            foreach (var item in _userBuildingTable.GetUserBuilding())
            {
                adminViewModel.Add(new TableViewModel
                {
                    User_ID = item.User_ID,
                    X_pos = item.X_pos,
                    Y_pos = item.Y_pos,
                    Lvl = item.Lvl,
                    Building_ID = item.Building_ID,
                    Status = item.Status
                });
            }


            return View("~/Views/Admin/_UserBuildingList.cshtml", adminViewModel);
        }

        [HttpPost]
        public ActionResult Add(int User_ID, int X_pos, int Y_pos, int Lvl, int Building_ID, string Status)
        {
            UserBuildingDto _userBuildingDto = new UserBuildingDto();

            _userBuildingDto.User_ID = User_ID;
            _userBuildingDto.X_pos = X_pos;
            _userBuildingDto.Y_pos = Y_pos;
            _userBuildingDto.Lvl = Lvl;
            _userBuildingDto.Building_ID = Building_ID;
            _userBuildingDto.Status = Status;

            _userBuildingTable.Add(_userBuildingDto);

            return View("~/Views/Admin/Admin.cshtml");
        }


    }
}