using Game.Core.DTO;
using Game.GUI.ViewModels.Casino;
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
        private ITutorialService _tutorialService;

        public CasinoController(IDolarService dolarTableService, ITutorialService tutorialService)
        {
            _dolarTableService = dolarTableService;
            _tutorialService = tutorialService;
        }

        [Authorize]
        // GET: Casino
        public ActionResult Index()
        {
            CasinoViewModel casinoModel = new CasinoViewModel();
            casinoModel.allDiv = _tutorialService.tutorialUser(User.Identity.Name).allDiv;
            casinoModel.casinoDiv= _tutorialService.tutorialUser(User.Identity.Name).casinoDiv;
            return View(casinoModel);
        }

        [Authorize]
        public string Slots(int bet)
        {
            if (bet > (int)_dolarTableService.UserDolar(User.Identity.Name) && bet>1000)
            {
                return "error";
            }
            _dolarTableService.PayForBet(User.Identity.Name, bet);

            Random r = new Random();
            int s1 = r.Next(0, 5);
            int s2 = r.Next(0, 5);
            int s3 = r.Next(0, 5);
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