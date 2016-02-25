using Game.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Service.Interfaces.TableInterface
{
    public interface IBuildingService
    {
        List<BuildingDto> GetBuilding();
        bool Add(BuildingDto building);
        bool Update(BuildingDto building);
        bool Delete(int id);
    }
}
