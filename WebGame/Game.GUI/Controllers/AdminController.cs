using Game.Core.DTO;
using Game.Service.Interfaces;
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
        private IUserService _user;

        public AdminController(IAdminService adminService, IUserService user)
        {
            _adminService = adminService;
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

        [HttpPost]
        public JsonResult adminmethod(string tabela, int id, string user)
        {
            _adminService.adminMethod(id, tabela, user);
            return new JsonResult { Data = true };
        }

        public ActionResult _UserList()
        {
            List<UserDto> list = _adminService.GetUsers();
            return View(list);
        }

        public ActionResult _AdminList()
        {
            List<AdminDto> list = _adminService.GetAdmin();
            return View(list);
        }

        public ActionResult _BanList()
        {
            List<BanDto> list = _adminService.GetBans();
            return View(list);
        }

        public ActionResult _BuildingQueueList()
        {
            List<BuildingQueueDto> list = _adminService.GetQueues();
            return View(list);
        }
        public ActionResult _BuildingList()
        {
            List<BuildingDto> list = _adminService.GetBuildings();
            return View(list);
        }

        public ActionResult _DolarList()
        {
            List<DolarDto> list = _adminService.GetDolars();
            return View(list);
        }

        public ActionResult _MapList()
        {
            List<MapDto> list = _adminService.GetMaps();
            return View(list);
        }

        public ActionResult _ProductList()
        {
            List<ProductDto> list = _adminService.GetProducts();
            return View(list);
        }

        public ActionResult _UserBuildingList()
        {
            List<UserBuildingDto> list = _adminService.GetUserBuildings();
            return View(list);
        }

        public ActionResult _UserProductList()
        {
            List<UserProductDto> list = _adminService.GetUserProducts();
            return View(list);
        }
    }
}
