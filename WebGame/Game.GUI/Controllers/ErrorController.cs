using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Game.GUI.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _505()
        {
            return View();
        }

        public ActionResult _500()
        {
            return View();
        }

        public ActionResult _404()
        {
            return View();
        }

        public ActionResult _Blocked()
        {
            return View();
        }
    }
}