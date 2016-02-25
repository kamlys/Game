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
    public class QueueService : IQueueService
    {

        private IRepository<BuildingQueue> _buildingQueue;
        private IRepository<UserBuildings> _usersBuilding;
        private IRepository<Buildings> _building;
        private IRepository<Users> _user;
        private IUnitOfWork _unitOfWork;

        public QueueService(IRepository<BuildingQueue> buildingQueue,
            IRepository<UserBuildings> userBuilding,
            IRepository<Users> user,
            IRepository<Buildings> building,
            IUnitOfWork unitOfWork)
        {
            _buildingQueue = buildingQueue;
            _usersBuilding = userBuilding;
            _user = user;
            _building = building;
            _unitOfWork = unitOfWork;
        }


        public bool Add(BuildingQueueDto buildingQueue)
        {
            try
            {
                DateTime tempDate;
                if (buildingQueue.NewStatus.ToLower().Contains("budowa") || buildingQueue.NewStatus.ToLower().Contains("rozbudowa"))
                {
                    tempDate = DateTime.Now.AddSeconds(_usersBuilding.Get(buildingQueue.UserBuilding_ID).Buildings.BuildingTime);
                }
                else if (buildingQueue.NewStatus.ToLower().Contains("burzenie"))
                {
                    tempDate = DateTime.Now.AddSeconds(_usersBuilding.Get(buildingQueue.UserBuilding_ID).Buildings.DestructionTime);
                }
                else
                {
                    tempDate = DateTime.Now;
                }

                _buildingQueue.Add(new BuildingQueue
                {
                    User_ID = _user.GetAll().First(i => i.Login == buildingQueue.Login).ID,
                    UserBuilding_ID = buildingQueue.UserBuilding_ID,
                    FinishTime = /*(DateTime)tempDate*/ DateTime.Now,
                    NewStatus = buildingQueue.NewStatus
                });

                _unitOfWork.Commit();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool Delete(int id)
        {
            try
            {
                int userBuildingQueue = _buildingQueue.Get(id).UserBuilding_ID;
                foreach (var item in _usersBuilding.GetAll().Where(i => i.ID == userBuildingQueue))
                {
                    _usersBuilding.Get(item.ID).Status = "gotowy";
                    _unitOfWork.Commit();
                }

                _buildingQueue.Delete(_buildingQueue.Get(id));
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<BuildingQueueDto> GetQueue()
        {
            List<BuildingQueueDto> buildingQueueDto = new List<BuildingQueueDto>();
            foreach (var item in _buildingQueue.GetAll())
            {
                try
                {
                    buildingQueueDto.Add(new BuildingQueueDto
                    {
                        ID = item.ID,
                        Login = _user.Get(item.User_ID).Login,
                        User_ID = item.User_ID,
                        UserBuilding_ID = item.UserBuilding_ID,
                        BuildingName = _building.GetAll().First(i => i.ID == item.UserBuildings.Buildings.ID).Alias,
                        FinishTime = (DateTime)item.FinishTime,
                        NewStatus = item.NewStatus
                    });
                }
                catch (Exception)
                {
                }
            }
            return buildingQueueDto;
        }

        public bool Update(BuildingQueueDto buildingQueue)
        {
            try
            {
                foreach (var item in _buildingQueue.GetAll().Where(i => i.ID == buildingQueue.ID))
                {
                    item.User_ID = _user.GetAll().First(i => i.Login == buildingQueue.Login).ID;
                    item.UserBuilding_ID = buildingQueue.UserBuilding_ID;
                    item.FinishTime = buildingQueue.FinishTime;
                    item.NewStatus = buildingQueue.NewStatus;
                }

                _unitOfWork.Commit();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
