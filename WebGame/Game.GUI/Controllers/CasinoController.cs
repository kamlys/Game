using Game.Core.DTO;
using Game.GUI.ViewModels.User.Ban;
using Game.Service.Interfaces;
using Game.Service.Interfaces.TableInterface;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Game.GUI.Controllers
{
    public class CasinoController : Controller
    {
        private IDolarService _dolarTableService;

        public CasinoController(IDolarService dolarTableService)
        {
            _dolarTableService = dolarTableService;
        }

        [Authorize]
        // GET: Casino
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public string Slots(int bet)
        {
            if (bet > (int)_dolarTableService.UserDolar(User.Identity.Name))
            {
                return "error";
            }
            _dolarTableService.PayForBet(User.Identity.Name, bet);

            Random r = new Random();
            int s1 = r.Next(0, 7);
            int s2 = r.Next(0, 7);
            int s3 = r.Next(0, 7);
            var win = 0;
            if (s1 == s2 && s2 == s3)
            {
                win = _dolarTableService.AddFromBet(User.Identity.Name, bet, 10);
            }
            else if(s1 == s2 || s2 == s3 || s3 == s1){
                win = _dolarTableService.AddFromBet(User.Identity.Name, bet, 3);
            }
            var currDolars = _dolarTableService.UserDolar(User.Identity.Name);
            return "[" + s1.ToString() + "," + s2.ToString() + "," + s3.ToString() + "," + win.ToString() + "," + currDolars + "]";
        }

    }
}