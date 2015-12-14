using Game.GUI.ViewModels;
using Game.Service.Interfaces.TableInterface;
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
        public ActionResult Index()
        {
            ListTableViewModel tableList = new ListTableViewModel();
            tableList.tableList = new List<TableViewModel>();

            foreach (var item in _dolar.GetToRank())
            {
                tableList.tableList.Add(new TableViewModel
                {
                    Login = item.Login,
                    Value = item.Value
                });
            }
            return View(tableList);
        }
    }
}