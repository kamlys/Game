﻿using Game.Service.Interfaces.TableInterface;
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

        public bool Add(BuildingDto building)
        {
            if (!_buildings.GetAll().Any(i => i.Name == building.Name && i.Alias == building.Alias))
            {
                try
                {
                    _buildings.Add(new Buildings
                    {
                        Name = building.Name,
                        Price = building.Price,
                        Height = building.Height,
                        Width = building.Width,
                        Dest_price = building.Dest_price,
                        Percent_price_per_lvl = building.Percent_price_per_lvl,
                        Percent_product_per_lvl = building.Percent_product_per_lvl,
                        Product_ID = _products.GetAll().First(i => i.Alias == building.Product_Name).ID,
                        Product_per_sec = building.Product_per_sec,
                        Alias = building.Alias,
                        BuildingTime = building.BuildingTime,
                        DestructionTime = building.DestructionTime,
                        Stock = building.Stock
                    });

                    _unitOfWork.Commit();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }

        public bool Delete(int id)
        {
            try
            {
                _buildings.Delete(_buildings.Get(id));
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

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
                        Product_Name = _products.Get(item.Product_ID).Alias,
                        Alias = item.Alias,
                        BuildingTime = item.BuildingTime,
                        DestructionTime = item.DestructionTime,
                        Stock = item.Stock
                    });
                }
                catch (Exception)
                {
                }
            }
            return buildingDto;
        }

        public bool Update(BuildingDto building)
        {
            if (_buildings.GetAll().Any(i => i.ID == building.ID))
            {
                try
                {
                    foreach (var item in _buildings.GetAll().Where(i => i.ID == building.ID))
                    {
                        item.Name = building.Name;
                        item.Price = building.Price;
                        item.Percent_price_per_lvl = building.Percent_price_per_lvl;
                        item.Width = building.Width;
                        item.Height = building.Height;
                        item.Product_per_sec = building.Product_per_sec;
                        item.Dest_price = building.Dest_price;
                        item.Percent_product_per_lvl = building.Percent_product_per_lvl;
                        item.Product_ID = _products.GetAll().First(i => i.Alias == building.Product_Name).ID;
                        item.Alias = building.Alias;
                        item.BuildingTime = building.BuildingTime;
                        item.DestructionTime = building.DestructionTime;
                        item.Stock = building.Stock;
                    }
                    _unitOfWork.Commit();

                    return true;
                }
                catch (Exception ex)
                {
                }
            }
            return false;
        }
    }
}
