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
        private IRepository<Users> _users;
        private IUnitOfWork _unitOfWork;

        public MapService(IRepository<Maps> maps, IRepository<Users> users, IUnitOfWork unitOfWork)
        {
            _maps = maps;
            _users = users;
            _unitOfWork = unitOfWork;
        }
        public MapDto GetMap(string User)
        {
            if (User != String.Empty)
            {
                var map = _maps.GetAll().First(a => a.Users.Login == User);
                return new MapDto { Height = map.Height, Map_ID = map.Map_ID, User_ID = map.User_ID, Width = map.Width };
            }
            else
            {
                return null;
            }
        }


        public bool Add(MapDto map)
        {
            if (!_maps.GetAll().Any(i => i.Users.Login == map.Login) && map.Width >= 0 && map.Height >= 0)
            {
                _maps.Add(new Maps
                {
                    User_ID = _users.GetAll().First(i => i.Login == map.Login).ID,
                    Height = map.Height,
                    Width = map.Width
                });

                _unitOfWork.Commit();
                return true;
            }
            return false;
        }

        //public void Delete(int id)
        //{
        //    _maps.Delete(_maps.Get(id));
        //    _unitOfWork.Commit();
        //}

        public List<MapDto> GetMaps()
        {
            List<MapDto> mapDto = new List<MapDto>();
            foreach (var item in _maps.GetAll())
            {
                try
                {
                    mapDto.Add(new MapDto
                    {
                        Map_ID = item.Map_ID,
                        User_ID = item.User_ID,
                        Login = _users.Get(item.User_ID).Login,
                        Width = item.Width,
                        Height = item.Height
                    });
                }
                catch (Exception)
                {
                }
            }
            return mapDto;
        }

        public bool Update(MapDto map)
        {
            if (map.Width >= 0 && map.Height >= 0)
            {
                foreach (var item in _maps.GetAll().Where(i => i.Map_ID == map.Map_ID))
                {
                    item.User_ID = _users.GetAll().First(i => i.Login == map.Login).ID;
                    item.Height = map.Height;
                    item.Width = map.Width;
                }

                _unitOfWork.Commit();
                return true;
            }
            return false;
        }
    }
}
