using Game.Core.DTO;
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
        bool AddProduct(ProductRequirementDto productDto);
        bool UpdateProduct(ProductRequirementDto productDto);
        bool DeleteProduct(int id);


    }
}
