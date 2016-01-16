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

        [Authorize]
        public ActionResult _BanList(int? Page_No)
        {
            BanViewModel banModel = new BanViewModel();
            int Size_Of_Page = 10;
            int No_Of_Page = (Page_No ?? 1);
            banModel.listModel = _banTable.GetBan().Select(x => new ItemBanViewModel
            {
                ID = x.ID,
                User_Login = x.Login,
                Description = x.Description,
                StartDate = x.Start_Date,
                FinishDate = x.Finish_Date
            }).ToList().ToPagedList(No_Of_Page, Size_Of_Page);

            return View("~/Views/Admin/_BanList.cshtml", banModel);
        }


        [HttpPost]
        [Authorize]
        public ActionResult Add(BanViewModel banModel)
        {
            BanDto _banDto = new BanDto();

            _banDto.Login = banModel.viewModel.User_Login;
            _banDto.Description = banModel.viewModel.Description;
            _banDto.Finish_Date = banModel.viewModel.FinishDate;

            _banTable.Add(_banDto);

            return View("~/Views/Admin/Admin.cshtml");
        }


        [HttpPost]
        [Authorize]
        public ActionResult Update(BanViewModel banModel)
        {
            BanDto _banDto = new BanDto();

            _banDto.ID = banModel.viewModel.ID;
            _banDto.Login = banModel.viewModel.User_Login;
            _banDto.Description = banModel.viewModel.Description;
            _banDto.Finish_Date = banModel.viewModel.FinishDate;

            _banTable.Update(_banDto);

            return View("~/Views/Admin/Admin.cshtml");
        }

        [HttpGet]
        [Authorize]
        public ActionResult Delete(int id)
        {
            _banTable.Delete(id);
            return View("~/Views/Admin/Admin.cshtml");
        }
    }
}