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
        private IRepository<Maps> _map;
        private IUnitOfWork _unitOfWork;
        private IHashPass _hassPass;

        public UserService(IRepository<Users> user, IUnitOfWork unitOfWork, IRepository<Maps> map, IHashPass hassPass)
        {
            _user = user;
            _map = map;
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
            if(_unitOfWork.Commit() > 0)
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
                _unitOfWork.Commit();
            }
            return success;
        }

        public bool LoginUser(UserDto userLogin)
        {
            return true;
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
    }
}
