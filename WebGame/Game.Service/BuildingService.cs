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
    public class BuildingService : IBuildingService
    {
        private IRepository<Buildings> _buildings;
        private IUnitOfWork _unitOfWork;

        public BuildingService(IRepository<Buildings> buildings, IUnitOfWork unitOfWork)
        {
            _buildings = buildings;
            _unitOfWork = unitOfWork;
        }
        public List<BuildingDto> GetBuildings()
        {
            List<BuildingDto> buildingsDto = new List<BuildingDto>();
            foreach (var item in _buildings.GetAll())
            {
                buildingsDto.Add(new BuildingDto { Name = item.Name, Price = item.Price } );
            }
            return buildingsDto;
        }
    }
}
