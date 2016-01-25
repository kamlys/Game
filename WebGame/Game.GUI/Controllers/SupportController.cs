using Game.GUI.ViewModels.Message;
using Game.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Game.GUI.Controllers
{
    public class SupportController : Controller
    {
        private IUserService _userService;

        public SupportController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: Support
        public ActionResult Help()
        {
            return View();
        }

        public ActionResult HelpSupport(MessageViewModel model)
        {
            _userService.HelpMessage(model.viewModel.Customer_Login, model.viewModel.Content, model.viewModel.Theme);
            return RedirectToAction("Help");
        }
    }
}