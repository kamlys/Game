using Game.Core.DTO;
using Game.GUI.ViewModels.User;
using Game.Service;
using Game.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Game.GUI.Controllers
{
    public class UserController : Controller
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Register(RegisterViewModel register)
        {
            UserDto user = new UserDto();

            user.Login = register.Login;
            user.Email = register.Email;
            user.Password = Crypto.HashPassword(register.Password);

            _userService.RegisterUser(user);

            return RedirectToAction("Index", "Home");
        }
    }
}