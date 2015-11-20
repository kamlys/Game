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


        public void Add(BuildingQueueDto buildingQueue)
        {
            _buildingQueue.Add(new BuildingQueue
            {
                User_ID = _user.Get(buildingQueue.ID).ID,
                UserBuilding_ID = _usersBuilding.GetAll().First(i => i.Buildings.Name == buildingQueue.BuildingName).ID,
                FinishTime = (DateTime)buildingQueue.FinishTime,
                NewStatus = buildingQueue.NewStatus
            });

            _unitOfWork.Commit();
        }

        public void Delete(int id)
        {
            _buildingQueue.Delete(_buildingQueue.Get(id));
            _unitOfWork.Commit();
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
                        BuildingName = _building.GetAll().First(i=> i.ID == item.UserBuildings.Buildings.ID).Alias,
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

        public void Update(BuildingQueueDto buildingQueue, int id)
        {
            throw new NotImplementedException();
        }
    }
}
