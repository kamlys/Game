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


namespace Game.Service
{
    public class UserService : IUserService
    {
        private IRepository<Users> _user;
        private IRepository<Maps> _map;
        private IUnitOfWork _unitOfWork;

        public UserService(IRepository<Users> user, IUnitOfWork unitOfWork, IRepository<Maps> map)
        {
            _user = user;
            _map = map;
            _unitOfWork = unitOfWork;
        }


        public void RegisterUser(UserDto user)
        {
            if (!_user.GetAll().Any(u => u.Login == user.Login) && !_user.GetAll().Any(u => u.Email == user.Email))
            {
                _user.Add(new Users
                {
                    Login = user.Login,
                    Email = user.Email,
                    Password = user.Password,
                    Registration_Date = DateTime.Now,
                    Last_Log = DateTime.Now
                });
            }
            _unitOfWork.Commit();

            int uID = _user.GetAll().First(u => u.Login == user.Login).ID;

            _map.Add(new Maps
            {
                User_ID = uID,
                Width = 10,
                Height = 10
            });
            _unitOfWork.Commit();

        }
    }
}
