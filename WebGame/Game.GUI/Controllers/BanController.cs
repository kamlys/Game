using Game.Core.DTO;
using Game.GUI.ViewModels.User.Ban;
using Game.Service.Interfaces.TableInterface;
using PagedList;
using System.Linq;
using System.Web.Mvc;

namespace Game.GUI.Controllers
{
    public class BanController : Controller
    {
        private IBanService _banTable;

        public BanController(IBanService banTable)
        {
            _banTable = banTable;
        }
        // GET: Building
        public ActionResult Index()
        {
            return View();
        }

      
    }
}