using Game.GUI.ViewModels;
using Game.GUI.ViewModels.Ranking;
using Game.Service.Interfaces;
using Game.Service.Interfaces.TableInterface;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Game.GUI.Controllers
{
    public class RankingController : Controller
    {
        private IDolarService _dolar;
        private IUserService _user;

        public RankingController(IDolarService dolar, IUserService user)
        {
            _dolar = dolar;
            _user = user;
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
                    temp += 1;
                    rankingModel.Add(new RankingViewModel
                    {
                        User_Login = item.Login,
                        UserDolar = item.Value,
                        Position = temp - 1
                    });
                }
            return View(rankingModel.ToPagedList(No_Of_Page, Size_Of_Page));
        }

        [HttpPost]
        [Authorize]
        public JsonResult goToProfile(string searchString)
        {
            if (_user.GetUser().Any(i => i.Login.ToLower() == searchString.ToLower()))
            {
                return new JsonResult { Data = true };
            }
            else
            {
                List<string> errors;
                if (Session["val"] != null)
                {
                    errors = ((string[])Session["val"]).ToList();
                }
                else
                {
                    errors = new List<string>();
                }

                errors.Add("Taki użytkownik nie istnieje.");
                Session["val"] = errors.ToArray<string>();

                return new JsonResult { Data = false };
            }
        }
    }
}