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
    public class MapService : IMapService
    {
        private IRepository<Maps> _maps;
        private IUnitOfWork _unitOfWork;

        public MapService(IRepository<Maps> maps, IUnitOfWork unitOfWork)
        {
            _maps = maps;
            _unitOfWork = unitOfWork;
        }
        public MapDto GetMap(string User)
        {
            var map = _maps.GetAll().First(a => a.Users.Login == User);
            return new MapDto { Height = map.Height, Map_ID = map.Map_ID, User_ID = map.User_ID, Width = map.Width };
        }
    }
}
