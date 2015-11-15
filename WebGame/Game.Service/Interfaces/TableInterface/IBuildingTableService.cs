using Game.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Service.Interfaces.TableInterface
{
    public interface IBuildingTableService
    {
        List<BuildingDto> GetBuilding();
        void Add(BuildingDto building);
        void Update(BuildingDto building, int id);
        void Delete(int id);
    }
}
