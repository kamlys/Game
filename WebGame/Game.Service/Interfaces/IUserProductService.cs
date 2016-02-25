using Game.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Service.Interfaces.TableInterface
{
    public interface IUserProductService
    {
        List<UserProductDto> GetUserProduct();
        List<UserProductDto> GetUserProductList(string User);
        bool Add(UserProductDto userProduct);
        bool Update(UserProductDto userProduct);
        bool Delete(int id);
        void CreateProduct(int value, string productNem, string user);
    }
}
