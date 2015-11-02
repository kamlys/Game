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
    public class UserBuildingService : IUserBuildingService
    {
        private IRepository<UserBuildings> _userBuildings;
        private IRepository<Users> _users;
        private IUnitOfWork _unitOfWork;

        public UserBuildingService(IRepository<UserBuildings> userBuildings, IRepository<Users> users, IUnitOfWork unitOfWork)
        {
            _userBuildings = userBuildings;
            _unitOfWork = unitOfWork;
            _users = users;
        }
        public List<UserBuildingDto> GetBuildings(string User)
        {
            var buildings = _userBuildings.GetAll().Where(a => a.Users.Login == User);
            List<UserBuildingDto> list = new List<UserBuildingDto>();
            foreach(var a in buildings) {
                list.Add(
                    new UserBuildingDto {  
                        Building_ID = a.Building_ID, 
                        ID = a.ID, 
                        Lvl = a.Lvl, 
                        User_ID = a.User_ID, 
                        X_pos = a.X_pos,
                        Y_pos = a.Y_pos
                    }
                );
            }
            return list;
        }

        public bool Build(int id, int col, int row, string user)
        {
            int userId = _users.GetAll().First(a => a.Login == user).ID;
            _userBuildings.Add(new UserBuildings { Building_ID = id, Lvl = 1, X_pos = col, Y_pos = row, User_ID = userId });
            _unitOfWork.Commit();
            return true;
        }
    }
}
