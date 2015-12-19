using Game.Core.DTO;
using Game.Dal;
using Game.Dal.Model;
using Game.Dal.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Service.Interfaces;
using Game.Service.Helpers;

namespace Game.Service
{
    public class UserService : IUserService
    {
        private IRepository<Users> _user;
        private IRepository<Dolars> _dolar;
        private IRepository<Maps> _map;
        private IRepository<Friends> _friend;
        private IUnitOfWork _unitOfWork;
        private IHashPass _hassPass;

        public UserService(
            IRepository<Users> user,
            IRepository<Dolars> dolar,
            IRepository<Maps> map,
            IRepository<Friends> friend,
            IUnitOfWork unitOfWork,
            IHashPass hassPass)
        {
            _user = user;
            _map = map;
            _dolar = dolar;
            _friend = friend;
            _unitOfWork = unitOfWork;
            _hassPass = hassPass;
        }


        public bool RegisterUser(UserDto user)
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
                    Last_Log = DateTime.Now
                });
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
                    Value = 1000
                });

                _unitOfWork.Commit();
            }
            return success;
        }

        public bool LoginUser(UserDto userLogin)
        {
            if (_user.GetAll().Any(u => u.Login == userLogin.Login))
            {
                var user = _user.GetAll().First(u => u.Login == userLogin.Login);
                if (_hassPass.ValidationPassword(userLogin.Password, user.Password))
                {
                    user.Last_Log = DateTime.Now;
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

        public void Add(UserDto user)
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
        }

        public void Delete(int id)
        {
            _user.Delete(_user.Get(id));
            _unitOfWork.Commit();
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
                        Registration_Date = (DateTime)item.Registration_Date

                    });
                }
                catch (Exception)
                {
                }

            }
            return userDto;
        }

        public void Update(UserDto user)
        {
            foreach (var item in _user.GetAll().Where(i => i.ID == user.ID))
            {
                item.Login = user.Login;
                item.Password = _hassPass.GeneratePassword(user.Password);
                item.Email = user.Email;
                item.Last_Log = user.Last_Log;
                item.Registration_Date = user.Registration_Date;
                item.Last_Update = user.Last_Update;
            }

            _unitOfWork.Commit();
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
                Friend_ID = _user.GetAll().First(i=> i.Login==friend.Friend_Login).ID,
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
    }
}
