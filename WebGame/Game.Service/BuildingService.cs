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
                buildingsDto.Add(new BuildingDto { 
                    Name = item.Name, 
                    Price = item.Price, 
                    ID = item.ID, 
                    Height = item.Height, 
                    Width = item.Width, 
                    Dest_price = item.Dest_price, 
                    Percent_price_per_lvl = item.Percent_price_per_lvl, 
                    Percent_product_per_lvl = item.Percent_product_per_lvl,
                    Product_ID = item.Product_ID, 
                    Product_per_sec = item.Product_per_sec} 
                    );
            }
            return buildingsDto;
        }
    }
}
