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
    public class BuildingQueueController : Controller
    {
        private IQueueTableService _queueTable;

        public BuildingQueueController(IQueueTableService queueTable)
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
            List<TableViewModel> adminViewModel = new List<TableViewModel>();

            foreach (var item in _queueTable.GetQueue())
            {
                adminViewModel.Add(new TableViewModel
                {
                    ID = item.ID,
                    User_ID = item.User_ID,
                    UserBuilding_ID = item.UserBuilding_ID,
                    Finish_Date = item.FinishTime,
                    NewStatus = item.NewStatus
                });
            }


            return View("~/Views/Admin/_BuildingQueueList.cshtml", adminViewModel);
        }

        [HttpPost]
        public ActionResult Add(int User_ID, int UserBuilding_ID, DateTime FinishTime, string NewStatus)
        {
            BuildingQueueDto _queueDto = new BuildingQueueDto();

            _queueDto.User_ID = User_ID;
            _queueDto.UserBuilding_ID = UserBuilding_ID;
            _queueDto.FinishTime = FinishTime;
            _queueDto.NewStatus = NewStatus;

            _queueTable.Add(_queueDto);

            return View("~/Views/Admin/Admin.cshtml");
        }
    }
}
