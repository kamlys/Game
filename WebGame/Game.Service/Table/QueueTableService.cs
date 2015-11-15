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
    public class QueueTableService : IQueueTableService
    {

        private IRepository<BuildingQueue> _buildingQueue;
        private IUnitOfWork _unitOfWork;

        public QueueTableService(IRepository<BuildingQueue> buildingQueue, IUnitOfWork unitOfWork)
        {
            _buildingQueue = buildingQueue;
            _unitOfWork = unitOfWork;
        }


        public void Add(BuildingQueueDto buildingQueue)
        {
            _buildingQueue.Add(new BuildingQueue
            {
                User_ID = buildingQueue.User_ID,
                UserBuilding_ID = buildingQueue.UserBuilding_ID,
                FinishTime = (DateTime)buildingQueue.FinishTime,
                NewStatus = buildingQueue.NewStatus
            });

            _unitOfWork.Commit();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
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
                        User_ID = item.User_ID,
                        UserBuilding_ID = item.UserBuilding_ID,
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
