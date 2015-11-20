using Game.Service.Interfaces.TableInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Core.DTO;
using Game.Dal.Repository;
using Game.Dal;
using Game.Dal.Model;

namespace Game.Service.Table
{
    public class BuildingService : IBuildingService
    {
        private IRepository<Buildings> _buildings;
        private IRepository<Products> _products;
        private IUnitOfWork _unitOfWork;

        public BuildingService(IRepository<Buildings> buildings, IRepository<Products> products, IUnitOfWork unitOfWork)
        {
            _buildings = buildings;
            _products = products;
            _unitOfWork = unitOfWork;
        }

        public void Add(BuildingDto building)
        {
            _buildings.Add(new Buildings
            {
                Name = building.Name,
                Price = building.Price,
                ID = building.ID,
                Height = building.Height,
                Width = building.Width,
                Dest_price = building.Dest_price,
                Percent_price_per_lvl = building.Percent_price_per_lvl,
                Percent_product_per_lvl = building.Percent_product_per_lvl,
                Product_ID = _products.Get(building.Product_ID).ID,
                Product_per_sec = building.Product_per_sec,
                Alias = building.Alias,
                BuildingTime = building.BuildingTime,
                DestructionTime = building.DestructionTime
            });

            _unitOfWork.Commit();
        }

        public void Delete(int id)
        {
            _buildings.Delete(_buildings.Get(id));
            _unitOfWork.Commit();
        }

        public List<BuildingDto> GetBuilding()
        {
            List<BuildingDto> buildingDto = new List<BuildingDto>();
            foreach (var item in _buildings.GetAll())
            {
                try
                {
                    buildingDto.Add(new BuildingDto
                    {
                        ID = item.ID,
                        Name = item.Name,
                        Price = item.Price,
                        Percent_price_per_lvl = item.Percent_price_per_lvl,
                        Width = item.Width,
                        Height = item.Height,
                        Product_per_sec = item.Product_per_sec,
                        Dest_price = item.Dest_price,
                        Percent_product_per_lvl = item.Percent_product_per_lvl,
                        Product_ID = item.Product_ID,
                        Product_Name = _products.Get(item.Product_ID).Name,
                        Alias = item.Alias,
                        BuildingTime = item.BuildingTime,
                        DestructionTime = item.DestructionTime
                    });
                }
                catch (Exception)
                {
                }
            }
            return buildingDto;
        }

        public void Update(BuildingDto building, int id)
        {
            throw new NotImplementedException();
        }
    }
}
