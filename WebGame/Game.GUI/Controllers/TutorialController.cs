using Game.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Game.GUI.Controllers
{
    public class TutorialController : Controller
    {
        private ITutorialService _tutorialService;

        public TutorialController(ITutorialService tutorialService)
        {
            _tutorialService = tutorialService;
        }

        // GET: Tutorial
        public void closeDivTutorial(string divname)
        {
            _tutorialService.closeTutorial(User.Identity.Name, divname);
        }

        public void closeTutorial()
        {
            _tutorialService.noTutorial(User.Identity.Name);
        }
    }
}