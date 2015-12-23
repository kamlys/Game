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
        private IUserProductService _userProductService;
        private IUserBuildingService _userBuildingService;
        private INotificationService _notificationService;

        public UserController(IUserService userService, IUserProductService userProductService, IUserBuildingService userBuildingService, INotificationService notificationService)
        {
            _userService = userService;
            _userProductService = userProductService;
            _userBuildingService = userBuildingService;
            _notificationService = notificationService;
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

            if (_userService.LoginUser(user))
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


        [Authorize]
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
        [Authorize]
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

        [HttpPost]
        [Authorize]
        public ActionResult Update(ListTableViewModel listView)
        {
            UserDto _userDto = new UserDto();

            _userDto.ID = listView.tableView.ID;
            _userDto.Login = listView.tableView.Login;
            _userDto.Password = listView.tableView.Password;
            _userDto.Email = listView.tableView.Email;
            _userDto.Last_Log = listView.tableView.Last_Log;
            _userDto.Registration_Date = listView.tableView.Registration_Date;
            _userDto.Last_Update = listView.tableView.Last_Update;

            _userService.Update(_userDto);

            return View("~/Views/Admin/Admin.cshtml");
        }

        [HttpGet]
        [Authorize]
        public ActionResult Delete(int id)
        {
            _userService.Delete(id);
            return View("~/Views/Admin/Admin.cshtml");
        }


        [HttpGet]
        [Authorize]
        public ActionResult Profil(string user)
        {
            ListTableViewModel tableList = new ListTableViewModel();
            tableList.tableList = new List<TableViewModel>();
            tableList.tableView = new TableViewModel();

            int buildings = 0;
            var userDto = _userService.Profil(user);

            tableList.tableView.User_ID = userDto.ID;
            tableList.tableView.Login = userDto.Login;
            tableList.tableView.Email = userDto.Email;
            tableList.tableView.RegDays = (DateTime.Now - userDto.Registration_Date).Days;
            tableList.tableView.LogDays = (DateTime.Now - userDto.Last_Log).Days;
            tableList.tableView.Value = _userService.ifFriend(User.Identity.Name,user);
            tableList.tableView.IfIgnored = _userService.Ignored(user, User.Identity.Name);
            tableList.tableView.Ignor = _userService.Ignored(User.Identity.Name, user);

            foreach (var item in _userService.GetIgnored(User.Identity.Name))
            {
                tableList.tableList.Add(new TableViewModel{
                    Login = item
                });
            }

            foreach (var item in _userService.GetFriendList(User.Identity.Name))
            {
                tableList.tableList.Add(new TableViewModel
                {
                    Friend_Login = item.Friend_Login
                });
            }

            foreach (var item in _userBuildingService.GetUserBuildingList(user))
            {
                buildings += 1;
            }

            tableList.tableView.Number = buildings;
            return View(tableList);
        }

        [HttpPost]
        [Authorize]
        public ActionResult ChangePass(ListTableViewModel pass)
        {
            UserDto user = new UserDto();

            user.OldPassword = pass.tableView.OldPassword;
            user.Password = pass.tableView.Password;

            if (_userService.ChangePass(user, User.Identity.Name))
            {
                ViewData["changeInfo"] = "Hasło zostało zmienione";
                return RedirectToAction("Profil", new
                {
                    User = User.Identity.Name
                });
            }
            else
            {
                ViewData["changeInfo"] = "Coś poszło nie tak, spróbuj ponownie";
                return RedirectToAction("Profil", new
                {
                    User = User.Identity.Name
                });
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult ChangeEmail(ListTableViewModel email)
        {
            UserDto user = new UserDto();

            user.Email = email.tableView.Email;

            if (_userService.ChangeEmail(user, User.Identity.Name))
            {
                ViewData["changeInfo"] = "Adres E-mail został zmienione";
                return RedirectToAction("Profil", new
                {
                    User = User.Identity.Name
                });
            }
            else
            {
                ViewData["changeInfo"] = "Coś poszło nie tak, spróbuj ponownie";
                return RedirectToAction("Profil", new
                {
                    User = User.Identity.Name
                });
            }
        }

        [Authorize]
        public ActionResult _FriendList()
        {
            ListTableViewModel tableList = new ListTableViewModel();
            tableList.tableList = new List<TableViewModel>();

            foreach (var item in _userService.GetFriendList(User.Identity.Name))
            {
                tableList.tableList.Add(new TableViewModel
                {
                    ID = item.ID,
                    Login = item.User_Login,
                    User_ID = item.User_ID,
                    Friend_ID = item.Friend_ID,
                    Friend_Login = item.Friend_Login,
                    OrAccepted = item.OrAccepted
                });
            }
            return View(tableList);
        }

        [HttpPost]
        [Authorize]
        public JsonResult AddFriend(string a)
        {
            FriendDto addFriend = new FriendDto();
            addFriend.Friend_Login = a;
            addFriend.User_Login = User.Identity.Name;

            _userService.AddFriend(addFriend);

            _notificationService.SentNotification(new NotificationDto
            {
                User_Login = a,
                Description = "Nowa zaproszenie do znajomych",
            });


            return new JsonResult { Data = true };
        }

        public void AcceptFriend(int a)
        {
            _userService.AcceptFriend(a);
        }

        public void DontAcceptFriend(int a)
        {
            _userService.DontAcceptFriend(a);
        }

        [HttpPost]
        public void DeleteFriend(string loginfriend)
        {
            _userService.DeleteFriend(User.Identity.Name, loginfriend);
        }

        public void AddIgnore(string ignorlogin)
        {
            _userService.AddIgnore(User.Identity.Name, ignorlogin);
        }

        public void DeleteIgnore(string ignorlogin)
        {
            _userService.DeleteIgnore(User.Identity.Name, ignorlogin);
        }
    }
}