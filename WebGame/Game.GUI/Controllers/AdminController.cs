using Game.Core.DTO;
using Game.GUI.ViewModels.User.Admin;
using Game.Service.Interfaces;
using PagedList;
using System.Linq;
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
        public ActionResult _AdminList(int? Page_No)
        {
            AdminViewModel adminModel = new AdminViewModel();
            int Size_Of_Page = 10;
            int No_Of_Page = (Page_No ?? 1);
            adminModel.listModel = _adminService.GetAdmin().Select(x => new ItemAdminViewModel
            {
                ID = x.ID,
                User_Login = x.Login
            }).ToList().ToPagedList(No_Of_Page, Size_Of_Page);

            return View("~/Views/Admin/_AdminList.cshtml", adminModel);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Add(AdminViewModel viewModel)
        {
            AdminDto _adminDto = new AdminDto();

            _adminDto.Login = viewModel.viewModel.User_Login;

            _adminService.Add(_adminDto);

            return View("~/Views/Admin/Admin.cshtml");
        }

        [HttpPost]
        [Authorize]
        public ActionResult Update(AdminViewModel viewModel)
        {
            AdminDto _adminDto = new AdminDto();

            _adminDto.ID = viewModel.viewModel.ID;
            _adminDto.Login = viewModel.viewModel.User_Login;

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
