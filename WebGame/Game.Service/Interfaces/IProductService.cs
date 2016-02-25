using Game.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Service.Interfaces
{
    public interface IProductService
    {
        void UpdateUserProduct(string User);
        List<ProductDto> GetProduct();
        bool Add(ProductDto product);
        bool Update(ProductDto product);
        bool Delete(int id);
    }
}
