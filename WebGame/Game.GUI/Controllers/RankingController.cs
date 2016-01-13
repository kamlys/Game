using Game.GUI.ViewModels;
using Game.Service.Interfaces.TableInterface;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Game.GUI.Controllers
{
    public class RankingController : Controller
    {
        private IDolarService _dolar;

        public RankingController(IDolarService dolar)
        {
            _dolar = dolar;
        }
        // GET: Ranking
        [Authorize]
        public ActionResult Index(int? Page_No)
        {
            //ListTableViewModel tableList = new ListTableViewModel();
            //tableList.tableList = new List<TableViewModel>();

            List<TableViewModel> tableList = new List<TableViewModel>();
            int Size_Of_Page = 10;
            int No_Of_Page = (Page_No ?? 1);
            int temp = No_Of_Page;
            foreach (var item in _dolar.GetToRank())
            {
                temp += 1;
                tableList.Add(new TableViewModel
                {
                    Login = item.Login,
                    Value = item.Value,
                    Number = temp-1
                });
            }

            return View(tableList.ToPagedList(No_Of_Page, Size_Of_Page));
        }
    }
}