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
        private IRepository<Dolars> _dolars;
        private IRepository<Buildings> _buildigns;
        private IRepository<Users> _users;
        private IUnitOfWork _unitOfWork;

        public UserBuildingService(
            IRepository<UserBuildings> userBuildings, 
            IRepository<Users> users,
            IRepository<Buildings> buildings,
            IRepository<Dolars> dolars, 
            IUnitOfWork unitOfWork)
        {
            _userBuildings = userBuildings;
            _unitOfWork = unitOfWork;
            _users = users;
            _buildigns = buildings;
            _dolars = dolars;
        }
        

        public bool Build(int id, int col, int row, string user)
        {
            int uID = _users.GetAll().First(a => a.Login == user).ID;
            int buildPrice = _buildigns.GetAll().First(b => b.ID == id).Price;
            var dolarsAccount = _dolars.GetAll().First(u => u.User_ID == uID).Value;

            if (dolarsAccount >= buildPrice)
            {
                _userBuildings.Add(new UserBuildings
                {
                    Building_ID = id,
                    Lvl = 1,
                    X_pos = col,
                    Y_pos = row,
                    User_ID = uID
                });

                _dolars.GetAll().First(u => u.User_ID == uID).Value -= buildPrice;
                _unitOfWork.Commit();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
