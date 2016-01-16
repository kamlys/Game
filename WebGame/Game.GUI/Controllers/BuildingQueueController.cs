using Game.Core.DTO;
using Game.GUI.ViewModels.Building.QueueBuilding;
using Game.Service.Interfaces.TableInterface;
using PagedList;
using System;
using System.Linq;
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
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult _BuildingQueueList(int? Page_No)
        {
            QueueViewModel queueModel = new QueueViewModel();
            int Size_Of_Page = 10;
            int No_Of_Page = (Page_No ?? 1);

            queueModel.listModel = _queueTable.GetQueue().Select(x => new ItemQueueViewModel
            {
                ID = x.ID,
                User_ID = x.User_ID,
                User_Login = x.Login,
                UserBuilding_ID = x.UserBuilding_ID,
                BuildingName = x.BuildingName,
                FinishDate = x.FinishTime,
                NewStatus = x.NewStatus
            }).ToList().ToPagedList(No_Of_Page, Size_Of_Page);

            return View("~/Views/Admin/_BuildingQueueList.cshtml", queueModel);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Add(QueueViewModel queueModel)
        {
            BuildingQueueDto _queueDto = new BuildingQueueDto();

            _queueDto.Login = queueModel.viewModel.User_Login;
            _queueDto.UserBuilding_ID = queueModel.viewModel.UserBuilding_ID;
            _queueDto.NewStatus = queueModel.viewModel.NewStatus;
            _queueDto.FinishTime = DateTime.Now.AddSeconds(queueModel.viewModel.Second);

            _queueTable.Add(_queueDto);

            return View("~/Views/Admin/Admin.cshtml");
        }

        [HttpPost]
        [Authorize]
        public ActionResult Update(QueueViewModel queueModel)
        {
            BuildingQueueDto _buildingQueueDto = new BuildingQueueDto();

            _buildingQueueDto.ID = queueModel.viewModel.ID;
            _buildingQueueDto.Login = queueModel.viewModel.User_Login;

            _queueTable.Update(_buildingQueueDto);

            return View("~/Views/Admin/Admin.cshtml");
        }

        [HttpGet]
        [Authorize]
        public ActionResult Delete(int id)
        {
            _queueTable.Delete(id);
            return View("~/Views/Admin/Admin.cshtml");
        }
    }
}
