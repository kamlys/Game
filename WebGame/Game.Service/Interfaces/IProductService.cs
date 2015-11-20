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
        void Add(ProductDto product);
        void Update(ProductDto product, int id);
        void Delete(int id);
    }
}
