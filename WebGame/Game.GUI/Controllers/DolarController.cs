using Game.Core.DTO;
using Game.GUI.ViewModels;
using Game.GUI.ViewModels.Dolar;
using Game.Service.Interfaces;
using Game.Service.Interfaces.TableInterface;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Game.GUI.Controllers
{
    public class DolarController : Controller
    {
        private IDolarService _dolarTableService;
        private IUserService _user;
        public DolarController(IDolarService dolarTableService, IUserService user)
        {
            _dolarTableService = dolarTableService;
            _user = user;
        }

        
        public ActionResult dolar()
        {
            int result = getuserdolar();
            return Content(result.ToString());
        }

        [NonAction]
        private int getuserdolar()
        {
            return _dolarTableService.UserDolar(User.Identity.Name); ;
        }
    }
}
