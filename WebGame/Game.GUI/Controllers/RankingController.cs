using Game.GUI.ViewModels;
using Game.GUI.ViewModels.Ranking;
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
            List<RankingViewModel> rankingModel = new List<RankingViewModel>();

            int Size_Of_Page = 10;
            int No_Of_Page = (Page_No ?? 1);
            int temp = No_Of_Page;

            foreach (var item in _dolar.GetToRank())
            {
                temp+=1;
                rankingModel.Add(new RankingViewModel
                {
                    User_Login = item.Login,
                    UserDolar = item.Value,
                    Position = temp - 1
                });
            }

            //rankingModel = _dolar.GetToRank().Select(x => new RankingViewModel
            //{
            //    User_Login = x.Login,
            //    UserDolar = x.Value,
            //    Position = temp - 1
            //}).ToList();

            return View(rankingModel.ToPagedList(No_Of_Page,Size_Of_Page));
        }
    }
}