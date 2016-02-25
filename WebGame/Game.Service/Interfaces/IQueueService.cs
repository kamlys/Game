using Game.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Service.Interfaces.TableInterface
{
    public interface IQueueService
    {
        List<BuildingQueueDto> GetQueue();
        bool Add(BuildingQueueDto buildingQueue);
        bool Update(BuildingQueueDto buildingQueue);
        bool Delete(int id);
    }
}
