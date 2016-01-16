using Game.Core.DTO;
using Game.GUI.ViewModels;
using Game.GUI.ViewModels.Dolar;
using Game.Service.Interfaces;
using Game.Service.Interfaces.TableInterface;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Game.GUI.Controllers
{
    public class DolarController : Controller
    {
        private IDolarService _dolarTableService;
        private IUserService _user;
        public DolarController(IDolarService dolarTableService, IUserService user)
        {
            _dolarTableService = dolarTableService;
            _user = user;
        }

        [Authorize]
        public ActionResult _DolarList(int? Page_No)
        {
            DolarViewModel dolarModel = new DolarViewModel();

            int Size_Of_Page = 10;
            int No_Of_Page = (Page_No ?? 1);

            dolarModel.listModel = _dolarTableService.GetDolars().Select(x => new ItemDolarViewModel
            {
                ID = x.ID,
                User_ID = x.User_ID,
                User_Login = x.Login,
                DolarValue = x.Value
            }).ToList().ToPagedList(No_Of_Page, Size_Of_Page);

            return View("~/Views/Admin/_DolarList.cshtml", dolarModel);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Add(DolarViewModel dolarModel)
        {
            DolarDto _dolarDto = new DolarDto();

            _dolarDto.User_ID = dolarModel.viewModel.User_ID;
            _dolarDto.Login = dolarModel.viewModel.User_Login;
            _dolarDto.Value = dolarModel.viewModel.DolarValue;

            _dolarTableService.Add(_dolarDto);

            return View("~/Views/Admin/Admin.cshtml");
        }

        [HttpPost]
        [Authorize]
        public ActionResult Update(DolarViewModel dolarModel)
        {
            DolarDto _dolarDto = new DolarDto();

            _dolarDto.ID = dolarModel.viewModel.ID;
            _dolarDto.User_ID = dolarModel.viewModel.User_ID;
            _dolarDto.Login = dolarModel.viewModel.User_Login;
            _dolarDto.Value = dolarModel.viewModel.DolarValue;

            _dolarTableService.Update(_dolarDto);

            return View("~/Views/Admin/Admin.cshtml");
        }

        [HttpGet]
        [Authorize]
        public ActionResult Delete(int id)
        {
            _dolarTableService.Delete(id);
            return View("~/Views/Admin/Admin.cshtml");
        }

        public ActionResult dolar()
        {
            int result = getuserdolar();
            return Content(result.ToString());
        }

        [NonAction]
        private int getuserdolar()
        {
            return _dolarTableService.UserDolar(User.Identity.Name); ;
        }
    }
}
