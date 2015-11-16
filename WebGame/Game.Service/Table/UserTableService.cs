using Game.Service.Interfaces.TableInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Core.DTO;
using Game.Dal.Repository;
using Game.Dal.Model;
using Game.Dal;
using Game.Service.Interfaces;

namespace Game.Service.Table
{
    public class UserTableService : IUserTableService
    {
        private IRepository<Users> _users;
        private IHashPass _hashPass;
        private IUnitOfWork _unitOfWork;
        
        
        public UserTableService(IRepository<Users> users, IUnitOfWork unitOfWork, IHashPass hahsPass)
        {
            _users = users;
            _hashPass = hahsPass;
            _unitOfWork = unitOfWork;
        } 

        public void Add(UserDto user)
        {
            _users.Add(new Users
            {
                Login = user.Login,
                Password = _hashPass.GeneratePassword(user.Password),
                Email = user.Email,
                Last_Log = (DateTime)user.Last_Log,
                Last_Update = (DateTime)user.Last_Update,
                Registration_Date = (DateTime)user.Registration_Date
            });

            _unitOfWork.Commit();
        }

        public void Delete(int id)
        {
            _users.Delete(_users.Get(id));
            _unitOfWork.Commit();
        }

        public List<UserDto> GetUser()
        {
            List<UserDto> userDto = new List<UserDto>();
            foreach (var item in _users.GetAll())
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

        public void Update(UserDto admin, int id)
        {
            throw new NotImplementedException();
        }
    }
}
