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

namespace Game.Service.Table
{
    public class UserBuildingTableService : IUserBuildingTableService
    {
        private IRepository<UserBuildings> _userBuildings;
        private IUnitOfWork _unitOfWork;

        public UserBuildingTableService(IRepository<UserBuildings> userBuildings, IUnitOfWork unitOfWork)
        {
            _userBuildings = userBuildings;
            _unitOfWork = unitOfWork;
        }


        public void Add(UserBuildingDto userBuilding)
        {
            _userBuildings.Add(new UserBuildings
            {
                User_ID = userBuilding.User_ID,
                X_pos = userBuilding.X_pos,
                Y_pos = userBuilding.Y_pos,
                Lvl = userBuilding.Lvl,
                Building_ID = userBuilding.Building_ID,
                Status = userBuilding.Status
            });

            _unitOfWork.Commit();
        }

        public void Delete(int id)
        {
            _userBuildings.Delete(_userBuildings.Get(id));
            _unitOfWork.Commit();
        }

        public List<UserBuildingDto> GetUserBuilding()
        {
            List<UserBuildingDto> userBuildingsDto = new List<UserBuildingDto>();
            foreach (var item in _userBuildings.GetAll())
            {
                try
                {
                    userBuildingsDto.Add(new UserBuildingDto
                    {
                        ID = item.ID,
                        User_ID = item.User_ID,
                        X_pos = item.X_pos,
                        Y_pos = item.Y_pos,
                        Lvl = item.Lvl,
                        Building_ID = item.Building_ID,
                        Status = item.Status
                    });
                }
                catch (Exception)
                {
                }
            }
            return userBuildingsDto;
        }

        public void Update(UserBuildingDto userBuilding, int id)
        {
            throw new NotImplementedException();
        }
    }
}
