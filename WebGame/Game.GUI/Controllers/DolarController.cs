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
        private IDolarService _dolarTableService;
        private IUserService _user;
        public DolarController(IDolarService dolarTableService, IUserService user)
        {
            _dolarTableService = dolarTableService;
            _user = user;
        }

        [Authorize]
        // GET: DolarTable
        //public ActionResult Index()
        //{
        //    return View();
        //}

        [Authorize]
        public ActionResult _DolarList()
        {
            ListTableViewModel tableList = new ListTableViewModel();
            tableList.tableList = new List<TableViewModel>();

            foreach (var item in _dolarTableService.GetDolars())
            {
                tableList.tableList.Add(new TableViewModel
                {
                    ID = item.ID,
                    User_ID = item.User_ID,
                    Login = item.Login,
                    Value = item.Value
                });
            }


            return View("~/Views/Admin/_DolarList.cshtml", tableList);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Add(ListTableViewModel listView)
        {
            DolarDto _dolarDto = new DolarDto();

            _dolarDto.User_ID = listView.tableView.User_ID;
            _dolarDto.Login = listView.tableView.Login;
            _dolarDto.Value = listView.tableView.Value;

            _dolarTableService.Add(_dolarDto);

            return View("~/Views/Admin/Admin.cshtml");
        }

        [HttpPost]
        [Authorize]
        public ActionResult Update(ListTableViewModel listView)
        {
            DolarDto _dolarDto = new DolarDto();

            _dolarDto.ID = listView.tableView.ID;
            _dolarDto.User_ID = listView.tableView.User_ID;
            _dolarDto.Login = listView.tableView.Login;
            _dolarDto.Value = listView.tableView.Value;

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
