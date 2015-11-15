using Game.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Service.Interfaces.TableInterface
{
    public interface IMapTableService
    {
        List<MapDto> GetMaps();
        void Add(MapDto admin);
        void Update(MapDto admin, int id);
        void Delete(int id);
    }
}
