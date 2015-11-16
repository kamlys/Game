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
            ListTableViewModel tableList = new ListTableViewModel();
            tableList.tableList = new List<TableViewModel>();

            foreach (var item in _userBuildingTable.GetUserBuilding())
            {
                tableList.tableList.Add(new TableViewModel
                {
                    User_ID = item.User_ID,
                    X_pos = item.X_pos,
                    Y_pos = item.Y_pos,
                    Lvl = item.Lvl,
                    Building_ID = item.Building_ID,
                    Status = item.Status
                });
            }


            return View("~/Views/Admin/_UserBuildingList.cshtml", tableList);
        }

        [HttpPost]
        public ActionResult Add(ListTableViewModel listView)
        {
            UserBuildingDto _userBuildingDto = new UserBuildingDto();

            _userBuildingDto.User_ID = listView.tableView.User_ID;
            _userBuildingDto.X_pos = listView.tableView.X_pos;
            _userBuildingDto.Y_pos = listView.tableView.Y_pos;
            _userBuildingDto.Lvl = listView.tableView.Lvl;
            _userBuildingDto.Building_ID = listView.tableView.Building_ID;
            _userBuildingDto.Status = listView.tableView.Status;

            _userBuildingTable.Add(_userBuildingDto);

            return View("~/Views/Admin/Admin.cshtml");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            _userBuildingTable.Delete(id);
            return View("~/Views/Admin/Admin.cshtml");
        }


    }
}