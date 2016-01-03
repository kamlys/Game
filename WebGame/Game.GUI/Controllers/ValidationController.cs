using Game.GUI.ViewModels.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Game.GUI.Controllers
{
    public class ValidationController : Controller
    {
        // GET: Validation
        public ActionResult _Validation()
        {
            ValidationViewModel val = new ValidationViewModel();
            if (Session["val"] != null)
            {
                val.ValidationText = ((string[])Session["val"]);
            }

            Session.Remove("val");

            return View(val);
        }
    }
}