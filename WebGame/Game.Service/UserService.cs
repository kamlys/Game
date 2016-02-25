using Game.Core.DTO;
using Game.Dal;
using Game.Dal.Model;
using Game.Dal.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using Game.Service.Interfaces;
using System.Net;
using System.Net.Mail;


namespace Game.Service
{
    public class UserService : IUserService
    {
        private IRepository<Users> _user;
        private IRepository<Dolars> _dolar;
        private IRepository<Maps> _map;
        private IRepository<Friends> _friend;
        private IRepository<Ignored> _ignored;
        private IRepository<RecoveryCodes> _recoveryCodes;
        private IUnitOfWork _unitOfWork;
        private IHashPass _hassPass;
        private IRepository<Bans> _ban;
        private IRepository<Tutorials> _tutorial;
        private IRepository<Deals> _deals;
        private IRepository<Admins> _admins;
        private IRepository<BuildingQueue> _queue;
        private IRepository<DealsBuildings> _dealBuildings;
        private IRepository<Market> _market;
        private IRepository<Messages> _messages;
        private IRepository<Notifications> _notifications;
        private IRepository<UserBuildings> _uBuildings;
        private IRepository<UserProducts> _uProducts;


        public UserService(
            IRepository<Users> user,
            IRepository<Dolars> dolar,
            IRepository<Maps> map,
            IRepository<Friends> friend,
            IRepository<Ignored> ignored,
            IRepository<RecoveryCodes> recoveryCodes,
            IUnitOfWork unitOfWork,
            IHashPass hassPass,
            IRepository<Bans> ban,
            IRepository<Tutorials> tutorial,
            IRepository<Deals> deals,
            IRepository<Admins> admins,
        IRepository<BuildingQueue> queue,
        IRepository<DealsBuildings> dealBuildings,
        IRepository<Market> market,
        IRepository<Messages> messages,
        IRepository<Notifications> notifications,
        IRepository<UserBuildings> uBuildings,
        IRepository<UserProducts> uProducts)
        {
            _user = user;
            _map = map;
            _dolar = dolar;
            _friend = friend;
            _ignored = ignored;
            _recoveryCodes = recoveryCodes;
            _unitOfWork = unitOfWork;
            _hassPass = hassPass;
            _ban = ban;
            _tutorial = tutorial;
            _deals = deals;
            _admins = admins;
            _queue = queue;
            _dealBuildings = dealBuildings;
            _market = market;
            _messages = messages;
            _notifications = notifications;
            _uBuildings = uBuildings;
            _uProducts = uProducts;
        }


        public int[] RegisterUser(UserDto user)
        {
            bool success = false;
            if (!_user.GetAll().Any(u => u.Login == user.Login) && !_user.GetAll().Any(u => u.Email == user.Email))
            {
                _user.Add(new Users
                {
                    Login = user.Login,
                    Email = user.Email,
                    Password = _hassPass.GeneratePassword(user.Password),
                    Registration_Date = DateTime.Now,
                    Last_Log = DateTime.Now,
                    Last_Update = DateTime.Now
                });
            }

            List<int> val = new List<int>();

            if (!_user.GetAll().Any(u => u.Login == user.Login) && !_user.GetAll().Any(u => u.Email == user.Email))
            {
                val.Add(0);
            }
            if (_user.GetAll().Any(u => u.Login == user.Login))
            {
                val.Add(1);
            }
            if (_user.GetAll().Any(u => u.Email == user.Email))
            {
                val.Add(2);
            }

            if (_unitOfWork.Commit() > 0)
            {
                success = true;
            }

            if (success)
            {
                int uID = _user.GetAll().First(u => u.Login == user.Login).ID;

                _map.Add(new Maps
                {
                    User_ID = uID,
                    Width = 10,
                    Height = 10
                });

                _dolar.Add(new Dolars
                {
                    User_ID = uID,
                    Value = 100000
                });

                _tutorial.Add(new Tutorials
                {
                    User_ID = uID,
                    allDiv = false,
                    cookies = false,
                    casinoDiv = false,
                    homeDiv = false,
                    marketDiv = false,
                    messageDiv = false,
                    officeDiv = false,
                    rankDiv = false,
                    setDiv = false
                });

                _unitOfWork.Commit();
            }
            if (user.Password.Length < 5 || user.Password.ToCharArray().All(char.IsDigit))
            {
                val.Add(3);
            }

            return val.ToArray();
        }

        public int LoginUser(UserDto userLogin)
        {
            if (_user.GetAll().Any(u => u.Login == userLogin.Login))
            {
                int uID = _user.GetAll().First(i => i.Login == userLogin.Login).ID;

                if (_ban.GetAll().Any(i => i.User_ID == uID && i.Finish_Date > DateTime.Now))
                {
                    return 4;
                }
                else if (_ban.GetAll().Any(i => i.User_ID == uID))
                {
                    int id = _ban.GetAll().First(i => i.User_ID == uID).ID;

                    _ban.Delete(_ban.Get(id));
                    _unitOfWork.Commit();
                }

                var user = _user.GetAll().First(u => u.Login == userLogin.Login);

                if (_hassPass.ValidationPassword(userLogin.Password, user.Password))
                {
                    user.Last_Log = DateTime.Now;
                    _unitOfWork.Commit();
                    return 1;
                }
                else
                {
                    return 2;
                }
            }
            return 3;
        }

        public bool Add(UserDto user)
        {
            if (!_user.GetAll().Any(i => i.Login == user.Login || i.Email == user.Email))
            {
                try
                {
                    _user.Add(new Users
                    {
                        Login = user.Login,
                        Password = _hassPass.GeneratePassword(user.Password),
                        Email = user.Email,
                        Last_Log = (DateTime)user.Last_Log,
                        Last_Update = (DateTime)user.Last_Update,
                        Registration_Date = (DateTime)user.Registration_Date
                    });

                    _unitOfWork.Commit();

                    int uID = _user.GetAll().First(u => u.Login == user.Login).ID;

                    _map.Add(new Maps
                    {
                        User_ID = uID,
                        Width = 10,
                        Height = 10
                    });

                    _dolar.Add(new Dolars
                    {
                        User_ID = uID,
                        Value = 100000
                    });

                    _tutorial.Add(new Tutorials
                    {
                        User_ID = uID,
                        allDiv = false,
                        cookies = false,
                        casinoDiv = false,
                        homeDiv = false,
                        marketDiv = false,
                        messageDiv = false,
                        officeDiv = false,
                        rankDiv = false,
                        setDiv = false
                    });

                    _unitOfWork.Commit();

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }

        public bool Delete(int id)
        {
            if (!_deals.GetAll().Any(i => i.User1_ID == id || i.User2_ID == id))
            {
                try
                {
                    if (_admins.GetAll().Any(i => i.User_ID == id))
                    {
                        int ID = _admins.GetAll().First(i => i.User_ID == id).ID;
                        _admins.Delete(_admins.Get(ID));
                    }
                    if (_ban.GetAll().Any(i => i.User_ID == id))
                    {
                        int ID = _ban.GetAll().First(i => i.User_ID == id).ID;
                        _ban.Delete(_ban.Get(ID));
                    }
                    if (_queue.GetAll().Any(i => i.User_ID == id))
                    {
                        int ID = _queue.GetAll().First(i => i.User_ID == id).ID;
                        _queue.Delete(_queue.Get(ID));
                    }
                    if (_queue.GetAll().Any(i => i.User_ID == id))
                    {
                        int ID = _queue.GetAll().First(i => i.User_ID == id).ID;
                        _queue.Delete(_queue.Get(ID));
                    }
                    if (_dolar.GetAll().Any(i => i.User_ID == id))
                    {
                        int ID = _dolar.GetAll().First(i => i.User_ID == id).ID;
                        _dolar.Delete(_dolar.Get(ID));
                    }
                    foreach (var item in _friend.GetAll().Where(i => i.User_ID == id || i.Friend_ID == id))
                    {
                        _friend.Delete(_friend.Get(item.Id));
                        _unitOfWork.Commit();
                    }
                    foreach (var item in _ignored.GetAll().Where(i => i.User_ID == id || i.Ignored_ID == id))
                    {
                        _ignored.Delete(_ignored.Get(item.ID));
                        _unitOfWork.Commit();
                    }
                    if (_map.GetAll().Any(i => i.User_ID == id))
                    {
                        int ID = _map.GetAll().First(i => i.User_ID == id).Map_ID;
                        _map.Delete(_map.Get(ID));
                    }
                    foreach (var item in _market.GetAll().Where(i => i.User_ID == id))
                    {
                        _market.Delete(_market.Get(item.ID));
                        _unitOfWork.Commit();
                    }
                    foreach (var item in _messages.GetAll().Where(i => i.Customer_ID == id || i.Sender_ID == id))
                    {
                        _messages.Delete(_messages.Get(item.ID));
                        _unitOfWork.Commit();
                    }
                    foreach (var item in _notifications.GetAll().Where(i => i.User_ID == id))
                    {
                        _notifications.Delete(_notifications.Get(item.ID));
                        _unitOfWork.Commit();
                    }
                    foreach (var item in _recoveryCodes.GetAll().Where(i => i.User_ID == id))
                    {
                        _recoveryCodes.Delete(_recoveryCodes.Get(item.ID));
                        _unitOfWork.Commit();
                    }
                    if (_tutorial.GetAll().Any(i => i.User_ID == id))
                    {
                        int ID = _tutorial.GetAll().First(i => i.User_ID == id).ID;
                        _tutorial.Delete(_tutorial.Get(ID));
                    }
                    foreach (var item in _uBuildings.GetAll().Where(i => i.User_ID == id))
                    {
                        _uBuildings.Delete(_uBuildings.Get(item.ID));
                        _unitOfWork.Commit();
                    }
                    foreach (var item in _uProducts.GetAll().Where(i => i.User_ID == id))
                    {
                        _uProducts.Delete(_uProducts.Get(item.ID));
                        _unitOfWork.Commit();
                    }

                    _user.Delete(_user.Get(id));
                    _unitOfWork.Commit();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }

        public List<UserDto> GetUser()
        {
            List<UserDto> userDto = new List<UserDto>();
            foreach (var item in _user.GetAll())
            {
                try
                {
                    userDto.Add(new UserDto
                    {
                        ID = item.ID,
                        Login = item.Login,
                        Password = item.Password,
                        Email = item.Email,
                        Last_Log = (DateTime)item.Last_Log,
                        Last_Update = (DateTime)item.Last_Update,
                        Registration_Date = (DateTime)item.Registration_Date,
                    });
                }
                catch (Exception)
                {
                }

            }
            return userDto;
        }

        public bool Update(UserDto user)
        {
            try
            {
                foreach (var item in _user.GetAll().Where(i => i.ID == user.ID))
                {
                    if (_user.GetAll().Any(i => i.Login == user.Login || i.Email == user.Email))
                    {
                        return false;
                    }
                    else
                    {
                        item.Login = user.Login;
                        item.Password = _user.GetAll().First(i => i.Login == user.Login).Password;
                        item.Email = user.Email;
                        item.Last_Log = user.Last_Log;
                        item.Registration_Date = user.Registration_Date;
                        item.Last_Update = user.Last_Update;
                    }
                }
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public DolarDto UserDolar()
        {
            DolarDto dolar = new DolarDto();

            foreach (var item in _user.GetAll().Where(i => i.ID == dolar.User_ID))
            {
                dolar.Value = _dolar.GetAll().First(i => i.User_ID == item.ID).Value;
            }

            return dolar;
        }

        public UserDto Profil(string User)
        {
            UserDto userDto = new UserDto();

            foreach (var item in _user.GetAll().Where(i => i.Login == User))
            {
                userDto.Login = item.Login;
                userDto.Email = item.Email;
                userDto.Registration_Date = (DateTime)item.Registration_Date;
                userDto.Last_Log = (DateTime)item.Last_Log;
            }
            return userDto;
        }

        public bool ChangePass(UserDto user, string User)
        {
            foreach (var item in _user.GetAll().Where(i => i.Login == User))
            {
                if (_hassPass.ValidationPassword(user.OldPassword, item.Password))
                {
                    _user.GetAll().First(i => i.Login == User).Password = _hassPass.GeneratePassword(user.Password);
                    _unitOfWork.Commit();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public bool ChangeEmail(UserDto user, string User)
        {
            bool temp = false;
            foreach (var item in _user.GetAll())
            {
                if (item.Email == user.Email)
                {
                    temp = true;
                }
            }

            if (!temp)
            {
                _user.GetAll().First(i => i.Login == User).Email = user.Email;
                _unitOfWork.Commit();
                return true;
            }
            return false;
        }

        public List<FriendDto> GetFriendList(string User)
        {
            List<FriendDto> friend = new List<FriendDto>();
            int uID = _user.GetAll().First(i => i.Login == User).ID;

            foreach (var item in _friend.GetAll().Where(i => i.User_ID == uID || i.Friend_ID == uID))
            {
                friend.Add(new FriendDto
                {
                    ID = item.Id,
                    User_ID = item.User_ID,
                    User_Login = _user.Get(item.User_ID).Login,
                    Friend_ID = item.Friend_ID,
                    Friend_Login = _user.Get(item.Friend_ID).Login,
                    OrAccepted = item.OrAccepted
                });
            }
            return friend;
        }

        public void AddFriend(FriendDto friend)
        {
            _friend.Add(new Friends
            {
                Friend_ID = _user.GetAll().First(i => i.Login == friend.Friend_Login).ID,
                User_ID = _user.GetAll().First(i => i.Login == friend.User_Login).ID,
                OrAccepted = false
            });

            _unitOfWork.Commit();
        }

        public void AcceptFriend(int id)
        {
            _friend.Get(id).OrAccepted = true;
            _unitOfWork.Commit();
        }

        public void DontAcceptFriend(int id)
        {
            _friend.Delete(_friend.Get(id));
            _unitOfWork.Commit();
        }

        public int ifFriend(string user, string friend)
        {
            int uID = _user.GetAll().First(i => i.Login == user).ID;
            int fID = _user.GetAll().First(i => i.Login == friend).ID;

            foreach (var item in _friend.GetAll().Where(i => (i.User_ID == uID && i.Friend_ID == fID) || (i.User_ID == fID && i.Friend_ID == uID)))
            {
                if (item.OrAccepted == true)
                {
                    return 1;
                }
                if (item.OrAccepted == false)
                {
                    return 2;
                }
            }
            return 3;
        }

        public void DeleteFriend(string userLogin, string friendLogin)
        {
            int uID = _user.GetAll().First(i => i.Login == userLogin).ID;
            int fID = _user.GetAll().First(i => i.Login == friendLogin).ID;
            int id = _friend.GetAll().First(i => (i.User_ID == uID && i.Friend_ID == fID) || (i.User_ID == fID && i.Friend_ID == uID)).Id;
            _friend.Delete(_friend.Get(id));
            _unitOfWork.Commit();
        }

        public bool Ignored(string user_login, string ignored_login)
        {
            int uID = _user.GetAll().First(i => i.Login == user_login).ID;
            int iID = _user.GetAll().First(i => i.Login == ignored_login).ID;

            if (_ignored.GetAll().Any(i => i.Ignored_ID == iID && i.User_ID == uID))
            {
                return true;
            }
            return false;
        }

        public void AddIgnore(string userlogin, string ignorelogin)
        {
            int uID = _user.GetAll().First(i => i.Login == userlogin).ID;
            int iID = _user.GetAll().First(i => i.Login == ignorelogin).ID;
            _ignored.Add(new Ignored
            {
                User_ID = uID,
                Ignored_ID = iID
            });

            _unitOfWork.Commit();
        }

        public void DeleteIgnore(string userlogin, string ignorelogin)
        {
            int uID = _user.GetAll().First(i => i.Login == userlogin).ID;
            int iID = _user.GetAll().First(i => i.Login == ignorelogin).ID;
            int ID = _ignored.GetAll().First(i => i.User_ID == uID && i.Ignored_ID == iID).ID;
            _ignored.Delete(_ignored.Get(ID));

            _unitOfWork.Commit();
        }

        public List<string> GetIgnored(string user)
        {
            int uID = _user.GetAll().First(i => i.Login == user).ID;

            List<string> logins = new List<string>();

            foreach (var item in _ignored.GetAll().Where(i => i.User_ID == uID))
            {
                logins.Add(_user.Get(item.Ignored_ID).Login);
            }

            return logins;
        }



        public bool ForgetPassword(string email)
        {
            if (_user.GetAll().Any(i => i.Email == email))
            {
                string code = RandomString();
                string user = _user.GetAll().First(i => i.Email == email).Login;
                int uID = _user.GetAll().First(i => i.Email == email).ID;
                var date = DateTime.Now.AddMinutes(10);
                bool temp = _recoveryCodes.GetAll().Any(i => i.User_ID == uID && i.LiveTime.CompareTo(DateTime.Now) > 0);
                if (!temp)
                {
                    _recoveryCodes.Add(new RecoveryCodes
                    {
                        User_ID = uID,
                        LiveTime = DateTime.Now.AddMinutes(10),
                        Code = code
                    });
                    _unitOfWork.Commit();
                    string content = @"<div style='background:#2c3e50;width:95%;min-height:100%;color:#fff;padding: 20px 20px 20px;font-size: 16px;'>
                            <img src='game.webserwer.pl/Content/Images/logo.png' alt='Pierwszy Milion' tittle='Pierszy Milion' width='30%'/><br>
                            Została wysłana prośba o odzyskanie hasła w grze dla konta <span style='color:#4dd0e1; font-weighth:bold; font-size: 18'>" + user
                        + "</span>. Twój kod to <b>" + code + "</b> i będzie on ważny przez 10 minut. <br><br><br> Jeśli nie wysyłałeś takiej prośby zignoruj tę wiadomość. <br><br><span style='margin-top: 20px; color: #fff'>#: <a href='wwww.game.webserwer.pl' style='text-decoration: underline; color:#fff'>wwww.game.webserwer.pl</a>  @: <a href='mailto:game@game.webserwer.pl' style='text-decoration: underline;color:#fff'>game@game.webserwer.pl</a></span></div>";

                    SendMail(email, content, "Pierwszy Milion - odzyskiwanie hasła");
                    return true;
                }
            }
            return false;
        }

        public int RecoveryPass(UserDto user)
        {
            int uID = _user.GetAll().First(i => i.Email == user.Email).ID;
            var code = _recoveryCodes.GetAll().Any(i => i.User_ID == uID && i.LiveTime.CompareTo(DateTime.Now) > 0);

            if (code)
            {
                if (_recoveryCodes.GetAll().First(i => i.User_ID == uID && i.LiveTime.CompareTo(DateTime.Now) > 0).Code == user.Code && _user.Get(uID).Email == user.Email)
                {
                    _user.Get(uID).Password = _hassPass.GeneratePassword(user.Password);
                    _recoveryCodes.Delete(_recoveryCodes.GetAll().First(i => i.Code == user.Code));
                    _unitOfWork.Commit();

                    return 1; //Wszystko ok
                }
                else
                {
                    return 2; // Kod się nie zgadza
                }
            }
            else
            {
                return 3; //Kod przedawniony;
            }
        }

        public static string RandomString()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, 5)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private void SendMail(string to, string body, string subject)
        {
            string email = "game@game.webserwer.pl";
            string password = "HaD1FfF1To$4";
            var loginInfo = new NetworkCredential(email, password);
            var msg = new MailMessage();
            var smtpClient = new SmtpClient("poczta.webserwer.pl", 587);

            msg.From = new MailAddress(email);
            msg.To.Add(new MailAddress(to));
            msg.Subject = subject;
            msg.Body = body;
            msg.IsBodyHtml = true;

            smtpClient.EnableSsl = true;
            smtpClient.Send(msg);
        }

        public List<FriendDto> GetFriends()
        {
            List<FriendDto> friendDto = new List<FriendDto>();

            foreach (var item in _friend.GetAll())
            {
                friendDto.Add(new FriendDto
                {
                    ID = item.Id,
                    Friend_Login = _user.GetAll().First(i => i.ID == item.Friend_ID).Login,
                    Friend_ID = item.Friend_ID,
                    User_ID = item.User_ID,
                    User_Login = _user.GetAll().First(i => i.ID == item.User_ID).Login,
                    OrAccepted = item.OrAccepted
                });
            }

            return friendDto;
        }

        public bool UpdateFriendAdmin(FriendDto friendDto)
        {
            if (_user.GetAll().Any(i => i.Login == friendDto.Friend_Login && i.Login == friendDto.User_Login)
                && friendDto.User_Login != friendDto.User_Login)
            {

                foreach (var item in _friend.GetAll().Where(i => i.Id == friendDto.ID))
                {
                    item.Id = friendDto.ID;
                    item.Friend_ID = _user.GetAll().First(i => i.Login == friendDto.Friend_Login).ID;
                    item.User_ID = _user.GetAll().First(i => i.Login == friendDto.User_Login).ID;
                    item.OrAccepted = friendDto.OrAccepted;
                };

                _unitOfWork.Commit();
                return true;
            }
            return false;
        }

        public bool DeleteFriendAdmin(int id)
        {
            _friend.Delete(_friend.Get(id));
            _unitOfWork.Commit();
            return true;
        }

        public bool AddFriendAdmin(FriendDto friendDto)
        {
            if (_user.GetAll().Any(i => i.Login == friendDto.Friend_Login && i.Login == friendDto.User_Login)
                && friendDto.User_Login != friendDto.User_Login)
            {
                _friend.Add(new Friends
                {
                    Friend_ID = _user.GetAll().First(i => i.Login == friendDto.Friend_Login).ID,
                    User_ID = _user.GetAll().First(i => i.Login == friendDto.User_Login).ID,
                    OrAccepted = friendDto.OrAccepted
                });

                _unitOfWork.Commit();
                return true;
            }
            return false;
        }

        public List<IgnoredDto> GetIgnored()
        {
            List<IgnoredDto> ignoredDto = new List<IgnoredDto>();

            foreach (var item in _ignored.GetAll())
            {
                ignoredDto.Add(new IgnoredDto
                {
                    ID = item.ID,
                    Ignored_ID = item.Ignored_ID,
                    Ignored_Login = _user.GetAll().First(i => i.ID == item.Ignored_ID).Login,
                    User_ID = item.User_ID,
                    User_Login = _user.GetAll().First(i => i.ID == item.User_ID).Login
                });
            }

            return ignoredDto;
        }

        public bool UpdateIgnoredAdmin(IgnoredDto ignoredDto)
        {
            if (_user.GetAll().Any(i => i.Login == ignoredDto.User_Login && i.Login == ignoredDto.Ignored_Login)
                && (ignoredDto.Ignored_Login != ignoredDto.User_Login))
            {
                foreach (var item in _ignored.GetAll().Where(i => i.ID == ignoredDto.ID))
                {
                    item.Ignored_ID = _user.GetAll().First(i => i.Login == ignoredDto.Ignored_Login).ID;
                    item.User_ID = _user.GetAll().First(i => i.Login == ignoredDto.User_Login).ID;
                }

                _unitOfWork.Commit();
                return true;
            }
            return false;
        }

        public bool DeleteIgnoredAdmin(int id)
        {
            try
            {
                _ignored.Delete(_ignored.Get(id));
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool AddIgnoredAdmin(IgnoredDto ignoredDto)
        {
            if (_user.GetAll().Any(i => i.Login == ignoredDto.User_Login && i.Login == ignoredDto.Ignored_Login))
            {
                _ignored.Add(new Ignored
                {
                    Ignored_ID = _user.GetAll().First(i => i.Login == ignoredDto.Ignored_Login).ID,
                    User_ID = _user.GetAll().First(i => i.Login == ignoredDto.User_Login).ID
                });
                _unitOfWork.Commit();
                return true;
            }
            return false;
        }


        public void HelpMessage(string email, string content, string theme)
        {
            string t = "Support -" + theme;
            string userEmail = email;
            SendSupport(userEmail, content, t);
        }

        private void SendSupport(string userEmail, string body, string subject)
        {
            string email = "game@game.webserwer.pl";
            string password = "HaD1FfF1To$4";
            var loginInfo = new NetworkCredential(email, password);
            var msg = new MailMessage();
            var smtpClient = new SmtpClient("poczta.webserwer.pl", 587);

            msg.From = new MailAddress(userEmail);
            msg.To.Add(new MailAddress(email));
            msg.Subject = subject;
            msg.Body = body;
            msg.IsBodyHtml = true;

            smtpClient.EnableSsl = true;
            smtpClient.Send(msg);
        }


    }
}
