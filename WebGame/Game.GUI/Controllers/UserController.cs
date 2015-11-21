using Game.Core.DTO;
using Game.GUI.ViewModels;
using Game.GUI.ViewModels.User;
using Game.Service;
using Game.Service.Interfaces;
using Game.Service.Interfaces.TableInterface;
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
            user.Password = register.Password;


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
            user.Password = modelLogin.Password;

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


        public ActionResult _UserList()
        {
            ListTableViewModel tableList = new ListTableViewModel();
            tableList.tableList = new List<TableViewModel>();

            foreach (var item in _userService.GetUser())
            {
                tableList.tableList.Add(new TableViewModel
                {
                    ID = item.ID,
                    Login = item.Login,
                    Password = item.Password,
                    Email = item.Email,
                    Last_Update = item.Last_Update,
                    Registration_Date = item.Registration_Date,
                    Last_Log = item.Last_Log
                });
            }


            return View("~/Views/Admin/_UserList.cshtml", tableList);
        }

        [HttpPost]
        public ActionResult Add(ListTableViewModel listView)
        {
            UserDto _userDto = new UserDto();

            _userDto.Login = listView.tableView.Login;
            _userDto.Password = listView.tableView.Password;
            _userDto.Email = listView.tableView.Email;
            _userDto.Last_Log = listView.tableView.Last_Log;
            _userDto.Registration_Date = listView.tableView.Registration_Date;
            _userDto.Last_Update = listView.tableView.Last_Update;

            _userService.Add(_userDto);

            return View("~/Views/Admin/Admin.cshtml");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            _userService.Delete(id);
            return View("~/Views/Admin/Admin.cshtml");
        }

    }
}