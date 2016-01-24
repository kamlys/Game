﻿using Game.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Service.Interfaces
{
    public interface IProductRequirementsService
    {
        List<ProductRequirementDto> GetCanUserProducts(string user);
        List<ProductRequirementDto> GetProducts();
        void AddProduct(ProductRequirementDto productDto);
        void UpdateProduct(ProductRequirementDto productDto);
        void DeleteProduct(int id);


    }
}
