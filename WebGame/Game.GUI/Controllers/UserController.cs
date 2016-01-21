using Game.Core.DTO;
using Game.GUI.ViewModels.User;
using Game.GUI.ViewModels.User.Friend;
using Game.Service.Interfaces;
using Game.Service.Interfaces.TableInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.SessionState;

namespace Game.GUI.Controllers
{
    [SessionState(SessionStateBehavior.Required)]
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
            List<string> errors;
            if (Session["val"] != null)
            {
                errors = ((string[])Session["val"]).ToList();
            }
            else
            {
                errors = new List<string>();
            }

            UserDto user = new UserDto();

            user.Login = register.Login;
            user.Email = register.Email;
            user.Password = register.Password;

            foreach (var item in _userService.RegisterUser(user))
            {
                if (item == 0)
                {
                    FormsAuthentication.SetAuthCookie(user.Login, true);
                    return RedirectToAction("Index", "Home");
                }
                else if (item == 1)
                {
                    errors.Add("Login już istnieje");
                }
                else if (item == 2)
                {
                    errors.Add("Email już istnieje");
                }

            }

            Session["val"] = errors.ToArray<string>();

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
            List<string> errors;
            if (Session["val"] != null)
            {
                errors = ((string[])Session["val"]).ToList();
            }
            else
            {
                errors = new List<string>();
            }
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
                errors.Add("Login bądź hasło niepoprawne.");
            }
            Session["val"] = errors.ToArray<string>();

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
            UserViewModel userModel = new UserViewModel();

            userModel.listModel = _userService.GetUser().Select(x => new ItemUserViewModel
            {
                ID = x.ID,
                User_Login = x.Login,
                Password = x.Password,
                Email = x.Email,
                LastUpdate = x.Last_Update,
                RegistrationDate = x.Registration_Date,
                LastLog = x.Last_Log
            }).ToList();

            return View("~/Views/Admin/_UserList.cshtml", userModel);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Add(UserViewModel userModel)
        {
            UserDto _userDto = new UserDto();

            _userDto.Login = userModel.viewModel.User_Login;
            _userDto.Password = userModel.viewModel.Password;
            _userDto.Email = userModel.viewModel.Email;
            _userDto.Last_Log = userModel.viewModel.LastLog;
            _userDto.Registration_Date = userModel.viewModel.RegistrationDate;
            _userDto.Last_Update = userModel.viewModel.LastUpdate;

            _userService.Add(_userDto);

            return View("~/Views/Admin/Admin.cshtml");
        }

        [HttpPost]
        [Authorize]
        public ActionResult Update(UserViewModel userModel)
        {
            UserDto _userDto = new UserDto();

            _userDto.ID = userModel.viewModel.ID;
            _userDto.Login = userModel.viewModel.User_Login;
            _userDto.Password = userModel.viewModel.Password;
            _userDto.Email = userModel.viewModel.Email;
            _userDto.Last_Log = userModel.viewModel.LastLog;
            _userDto.Registration_Date = userModel.viewModel.RegistrationDate;
            _userDto.Last_Update = userModel.viewModel.LastUpdate;

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
            ProfileViewModel profilView = new ProfileViewModel();

            int buildings = 0;
            var userDto = _userService.Profil(user);

            profilView.User_ID = userDto.ID;

            profilView.Login = userDto.Login;
            profilView.EmailAddress = userDto.Email;
            profilView.RegDays = (DateTime.Now - userDto.Registration_Date).Days;
            profilView.LogDays = (DateTime.Now - userDto.Last_Log).Days;
            profilView.Value = _userService.ifFriend(User.Identity.Name, user);
            profilView.IfIgnored = _userService.Ignored(user, User.Identity.Name);
            profilView.Ignor = _userService.Ignored(User.Identity.Name, user);


            profilView.Ignored_Login = _userService.GetIgnored(User.Identity.Name);

            profilView.Friend_Login = new List<string>();
            foreach (var item in _userService.GetFriendList(User.Identity.Name))
            {
                if (item.Friend_Login == User.Identity.Name)
                {
                    profilView.Friend_Login.Add(item.User_Login);
                }
                else
                {
                    profilView.Friend_Login.Add(item.Friend_Login);
                }
            }

            foreach (var item in _userBuildingService.GetUserBuildingList(user))
            {
                buildings += 1;
            }

            profilView.Number = buildings;
            return View(profilView);
        }

        [HttpPost]
        [Authorize]
        public ActionResult ChangePass(ProfileViewModel pass)
        {
            List<string> errors;
            if (Session["val"] != null)
            {
                errors = ((string[])Session["val"]).ToList();
            }
            else
            {
                errors = new List<string>();
            }

            UserDto user = new UserDto();

            user.OldPassword = pass.OldPassword;
            user.Password = pass.Password;

            if (_userService.ChangePass(user, User.Identity.Name))
            {
                errors.Add("Hasło zostało zmienione");
                Session["val"] = errors.ToArray<string>();
                return RedirectToAction("Profil", new
                {
                    User = User.Identity.Name
                });
            }
            else
            {
                errors.Add("Błąd. Hasło nie zostało zmienione.");
                Session["val"] = errors.ToArray<string>();
                return RedirectToAction("Profil", new
                {
                    User = User.Identity.Name
                });
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult ChangeEmail(ProfileViewModel email)
        {
            List<string> errors;
            if (Session["val"] != null)
            {
                errors = ((string[])Session["val"]).ToList();
            }
            else
            {
                errors = new List<string>();
            }
            UserDto user = new UserDto();

            user.Email = email.EmailAddress;

            if (_userService.ChangeEmail(user, User.Identity.Name))
            {
                errors.Add("Email został zmieniony.");
                Session["val"] = errors.ToArray<string>();
                return RedirectToAction("Profil", new
                {
                    User = User.Identity.Name
                });
            }
            else
            {
                errors.Add("Błąd. Email nie został zmieniony.");
                Session["val"] = errors.ToArray<string>();
                return RedirectToAction("Profil", new
                {
                    User = User.Identity.Name
                });
            }
        }

        [Authorize]
        public ActionResult _FriendList()
        {
            FriendViewModel friendModel = new FriendViewModel();

            friendModel.listModel = _userService.GetFriendList(User.Identity.Name).Select(x => new ItemFriendViewModel
            {
                ID = x.ID,
                User_Login = x.User_Login,
                User_ID = x.User_ID,
                Friend_ID = x.Friend_ID,
                Friend_Login = x.Friend_Login,
                OrAccepted = x.OrAccepted
            }).ToList();

            return View(friendModel);
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
                SenderLogin = User.Identity.Name,
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

        public ActionResult RecoveryPassword()
        {
            return View();
        }

        public void ForgetPassword(string email)
        {
            _userService.ForgetPassword(email);
        }

        public ActionResult RecoveryPass(RegisterViewModel user)
        {
            List<string> errors;
            if (Session["val"] != null)
            {
                errors = ((string[])Session["val"]).ToList();
            }
            else
            {
                errors = new List<string>();
            }


            UserDto userDto = new UserDto();
            userDto.Password = user.Password;
            userDto.Code = user.RecoveryCode;
            userDto.Email = user.Email;

            if (_userService.RecoveryPass(userDto) == 1)
            {
                errors.Add("Hasło zostało zmienione.");
            }
            else if (_userService.RecoveryPass(userDto) == 2)
            {
                errors.Add("Niepoprawny kod.");
            }
            else if (_userService.RecoveryPass(userDto) == 3)
            {
                errors.Add("Kod stracił ważność.");
            }

            Session["val"] = errors.ToArray<string>();

            return View("~/Views/User/Login.cshtml");
        }

    }
}