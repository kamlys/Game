﻿using Game.Core.DTO;
using Game.GUI.ViewModels;
using Game.Service.Interfaces.TableInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Game.GUI.Controllers
{
    public class BuildingQueueController : Controller
    {
        private IQueueService _queueTable;

        public BuildingQueueController(IQueueService queueTable)
        {
            _queueTable = queueTable;
        }

        // GET: BuildingQueue
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _BuildingQueueList()
        {
            ListTableViewModel tableList = new ListTableViewModel();
            tableList.tableList = new List<TableViewModel>();

            foreach (var item in _queueTable.GetQueue())
            {
                tableList.tableList.Add(new TableViewModel
                {
                    ID = item.ID,
                    User_ID = item.User_ID,
                    Login = item.Login,
                    UserBuilding_ID = item.UserBuilding_ID,
                    Name = item.BuildingName,
                    Finish_Date = item.FinishTime,
                    NewStatus = item.NewStatus
                });
            }


            return View("~/Views/Admin/_BuildingQueueList.cshtml", tableList);
        }

        [HttpPost]
        public ActionResult Add(ListTableViewModel listView)
        {
            BuildingQueueDto _queueDto = new BuildingQueueDto();

            _queueDto.Login = listView.tableView.Login;
            _queueDto.UserBuilding_ID = listView.tableView.UserBuilding_ID;
            _queueDto.NewStatus = listView.tableView.NewStatus;
            _queueDto.FinishTime = DateTime.Now.AddSeconds(listView.tableView.Value);

            _queueTable.Add(_queueDto);

            return View("~/Views/Admin/Admin.cshtml");
        }

        [HttpPost]
        public ActionResult Update(ListTableViewModel viewList)
        {
            BuildingQueueDto _buildingQueueDto = new BuildingQueueDto();

            _buildingQueueDto.ID = viewList.tableView.ID;
            _buildingQueueDto.Login = viewList.tableView.Login;

            _queueTable.Update(_buildingQueueDto);

            return View("~/Views/Admin/Admin.cshtml");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            _queueTable.Delete(id);
            return View("~/Views/Admin/Admin.cshtml");
        }
    }
}
