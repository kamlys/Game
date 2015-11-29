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

        public ActionResult _BanList()
        {
            ListTableViewModel tableList = new ListTableViewModel();
            tableList.tableList = new List<TableViewModel>();

            foreach (var item in _banTable.GetBan())
            {
                tableList.tableList.Add(new TableViewModel
                {
                    ID = item.ID,
                    Login = item.Login,
                    Description = item.Description,
                    Start_Date = item.Start_Date,
                    Finish_Date = item.Finish_Date
                });
            }


            return View("~/Views/Admin/_BanList.cshtml", tableList);
        }

        [HttpPost]
        public ActionResult Add(ListTableViewModel viewList)
        {
            BanDto _banDto = new BanDto();

            _banDto.Login = viewList.tableView.Login;
            _banDto.Description = viewList.tableView.Description;
            _banDto.Finish_Date = viewList.tableView.Finish_Date;

            _banTable.Add(_banDto);

            return View("~/Views/Admin/Admin.cshtml");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            _banTable.Delete(id);
            return View("~/Views/Admin/Admin.cshtml");
        }
    }
}