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
    }
}
