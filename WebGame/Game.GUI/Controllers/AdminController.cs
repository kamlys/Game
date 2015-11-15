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
            List<TableViewModel> adminViewModel = new List<TableViewModel>();

            foreach (var item in _adminTable.GetAdmins())
            {
                adminViewModel.Add(new TableViewModel
                {
                    ID = item.ID,
                    User_ID = item.User_ID
                });
            }


            return View("~/Views/Admin/_AdminList.cshtml", adminViewModel);
        }

        [HttpPost]
        public ActionResult Add(int User_ID)
        {
            AdminDto _adminDto = new AdminDto();

            _adminDto.User_ID = User_ID;

            _adminTable.Add(_adminDto);

            return View("~/Views/Admin/Admin.cshtml");
        }





        //public ActionResult _UserList()
        //{
        //    List<UserDto> list = _adminService.GetUsers();
        //    return View(list);
        //}

        //public ActionResult _AdminList()
        //{
        //    List<AdminDto> list = _adminService.GetAdmin();
        //    return View(list);
        //}

        //public ActionResult _BanList()
        //{
        //    List<BanDto> list = _adminService.GetBans();
        //    return View(list);
        //}

        //public ActionResult _BuildingQueueList()
        //{
        //    List<BuildingQueueDto> list = _adminService.GetQueues();
        //    return View(list);
        //}
        //public ActionResult _BuildingList()
        //{
        //    List<BuildingDto> list = _adminService.GetBuildings();
        //    return View(list);
        //}

        //public ActionResult _DolarList()
        //{
        //    List<DolarDto> list = _adminService.GetDolars();
        //    return View(list);
        //}

        //public ActionResult _MapList()
        //{
        //    List<MapDto> list = _adminService.GetMaps();
        //    return View(list);
        //}

        //public ActionResult _ProductList()
        //{
        //    List<ProductDto> list = _adminService.GetProducts();
        //    return View(list);
        //}

        //public ActionResult _UserBuildingList()
        //{
        //    List<UserBuildingDto> list = _adminService.GetUserBuildings();
        //    return View(list);
        //}

        //public ActionResult _UserProductList()
        //{
        //    List<UserProductDto> list = _adminService.GetUserProducts();
        //    return View(list);
        //}
    }
}
