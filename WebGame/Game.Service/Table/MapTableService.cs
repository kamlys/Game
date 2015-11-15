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
    public class MapTableService : IMapTableService
    {

        private IRepository<Maps> _maps;
        private IUnitOfWork _unitOfWork;

        public MapTableService(IRepository<Maps> maps, IUnitOfWork unitOfWork)
        {
            _maps = maps;
            _unitOfWork = unitOfWork;
        }

        public void Add(MapDto map)
        {
            _maps.Add(new Maps
            {
                User_ID = map.User_ID,
                Height = map.Height,
                Width = map.Width
            });

            _unitOfWork.Commit();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

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

        public void Update(MapDto admin, int id)
        {
            throw new NotImplementedException();
        }
    }
}
