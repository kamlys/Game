﻿using Game.Core.DTO;
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
        private IUserService _user;

        public AdminController(IAdminService adminService, IUserService user)
        {
            _adminService = adminService;
            _user = user;
        }
        // GET: Building
        [Authorize]
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

        [Authorize]
        public ActionResult _AdminList()
        {
            ListTableViewModel tableList = new ListTableViewModel();
            tableList.tableList = new List<TableViewModel>();

            foreach (var item in _adminService.GetAdmin())
            {
                tableList.tableList.Add(new TableViewModel
                {
                    ID = item.ID,
                    Login = item.Login
                });
            }


            return View("~/Views/Admin/_AdminList.cshtml", tableList);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Add(ListTableViewModel viewList)
        {
            AdminDto _adminDto = new AdminDto();

            _adminDto.Login = viewList.tableView.Login;

            _adminService.Add(_adminDto);

            return View("~/Views/Admin/Admin.cshtml");
        }

        [HttpPost]
        [Authorize]
        public ActionResult Update(ListTableViewModel viewList)
        {
            AdminDto _adminDto = new AdminDto();

            _adminDto.ID = viewList.tableView.ID;
            _adminDto.Login = viewList.tableView.Login;

            _adminService.Update(_adminDto);

            return View("~/Views/Admin/Admin.cshtml");
        }


        [HttpGet]
        [Authorize]
        public ActionResult Delete(int id)
        {
            _adminService.Delete(id);
            return View("~/Views/Admin/Admin.cshtml");
        }

    }
}
