using Game.Core.DTO;
using Game.Dal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Service.Interfaces
{
    public interface IMapService
    {
        MapDto GetMap(string User);
        List<MapDto> GetMaps();
        void Add(MapDto admin);
        void Update(MapDto map);
        void Delete(int id);
    }
}
