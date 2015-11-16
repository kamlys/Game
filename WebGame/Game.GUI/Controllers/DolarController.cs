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
    public class DolarController : Controller
    {
        private IDolarTableService _dolarTableService;
        private IUserService _user;
        public DolarController(IDolarTableService dolarTableService, IUserService user)
        {
            _dolarTableService = dolarTableService;
            _user = user;
        }

        // GET: DolarTable
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _DolarList()
        {
            ListTableViewModel tableList = new ListTableViewModel();
            tableList.tableList = new List<TableViewModel>();

            foreach (var item in _dolarTableService.GetDolars())
            {
                tableList.tableList.Add(new TableViewModel
                {
                    User_ID = item.User_ID,
                    Value = item.Value
                });
            }


            return View("~/Views/Admin/_DolarList.cshtml", tableList);
        }

        [HttpPost]
        public ActionResult Add(ListTableViewModel listView)
        {
            DolarDto _dolarDto = new DolarDto();

            _dolarDto.User_ID = listView.tableView.User_ID;
            _dolarDto.Value = listView.tableView.Value;

            _dolarTableService.Add(_dolarDto);

            return View("~/Views/Admin/Admin.cshtml");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            _dolarTableService.Delete(id);
            return View("~/Views/Admin/Admin.cshtml");
        }


    }
}