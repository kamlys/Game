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
using System.Web.Security;

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


            if (_userService.RegisterUser(user))
            {
                FormsAuthentication.SetAuthCookie(user.Login, true);
                return RedirectToAction("Index", "Home");

            };

            ModelState.AddModelError("", "Nie udało się zarejestrować.");

            return View();
        }

        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel modelLogin, string returnUrl)
        {
            UserDto user = new UserDto();

            user.Login = modelLogin.Login;
            user.Password = Crypto.HashPassword(modelLogin.Password);

            if(_userService.LoginUser(user))
            {
                FormsAuthentication.SetAuthCookie(modelLogin.Login, true);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Login bądź hasło niepoprawne.");
            }

            return View(modelLogin);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();

            return RedirectToAction("Index", "Home");
        }
    }
}