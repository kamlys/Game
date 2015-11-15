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
        private IBanTableService _banTable;

        public BanController(IBanTableService banTable)
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
            List<TableViewModel> adminViewModel = new List<TableViewModel>();

            foreach (var item in _banTable.GetBan())
            {
                adminViewModel.Add(new TableViewModel
                {
                    ID = item.ID,
                    User_ID = item.User_ID
                });
            }


            return View("~/Views/Admin/_BanList.cshtml", adminViewModel);
        }

        [HttpPost]
        public ActionResult Add(int User_ID)
        {
            BanDto _banDto = new BanDto();

            _banDto.User_ID = User_ID;

            _banTable.Add(_banDto);

            return View("~/Views/Admin/Admin.cshtml");
        }
    }
}