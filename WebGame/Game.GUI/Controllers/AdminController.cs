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
    public class AdminController : Controller
    {
        private IAdminService _adminService;
        private IAdminTableService _adminTable;
        private IUserService _user;

        public AdminController(IAdminService adminService, IAdminTableService adminTable, IUserService user)
        {
            _adminService = adminService;
            _adminTable = adminTable;
            _user = user;
        }
        // GET: Building
        public ActionResult Admin()
        {
            if (_adminService.ifAdmin(User.Identity.Name))
            {
                return View();
            }
            else
            {
                return View("~/Views/Home/Index.cshtml");
            }
        }

        public ActionResult _AdminList()
        {
            ListTableViewModel tableList = new ListTableViewModel();
            tableList.tableList = new List<TableViewModel>();

            foreach (var item in _adminTable.GetAdmins())
            {
                tableList.tableList.Add(new TableViewModel
                {
                    ID = item.ID,
                    User_ID = item.User_ID
                });
            }


            return View("~/Views/Admin/_AdminList.cshtml", tableList);
        }

        [HttpPost]
        public ActionResult Add(ListTableViewModel viewList)
        {
            AdminDto _adminDto = new AdminDto();

            _adminDto.User_ID = viewList.tableView.User_ID;

            _adminTable.Add(_adminDto);

            return View("~/Views/Admin/Admin.cshtml");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            _adminTable.Delete(id);
            return View("~/Views/Admin/Admin.cshtml");
        }

    }
}
