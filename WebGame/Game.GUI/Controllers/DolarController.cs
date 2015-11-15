using Game.Core.DTO;
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
            List<TableViewModel> adminViewModel = new List<TableViewModel>();

            foreach (var item in _dolarTableService.GetDolars())
            {
                adminViewModel.Add(new TableViewModel
                {
                    User_ID = item.User_ID,
                    Value = item.Value
                });
            }


            return View("~/Views/Admin/_DolarList.cshtml", adminViewModel);
        }

        [HttpPost]
        public ActionResult Add(int User_ID, int Value)
        {
            DolarDto _dolarDto = new DolarDto();

            _dolarDto.User_ID = User_ID;
            _dolarDto.Value = Value;

            _dolarTableService.Add(_dolarDto);

            return View("~/Views/Admin/Admin.cshtml");
        }


    }
}
