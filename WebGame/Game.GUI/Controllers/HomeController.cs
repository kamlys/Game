using Game.GUI.ViewModels.User;
using Game.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Game.GUI.Controllers
{
    public class HomeController : Controller
    {
        private ITutorialService _tutorials;

        public HomeController(ITutorialService tutorials)
        {
            _tutorials = tutorials;
        }
        // GET: Home
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                LoginViewModel viewModel = new LoginViewModel();
                viewModel.allDiv = _tutorials.tutorialUser(User.Identity.Name).allDiv;
                viewModel.cookies = _tutorials.tutorialUser(User.Identity.Name).cookies;
                viewModel.homeDiv = _tutorials.tutorialUser(User.Identity.Name).homeDiv;
                return View(viewModel);
            }
            else
            {
                return View();
            }
        }
    }
}