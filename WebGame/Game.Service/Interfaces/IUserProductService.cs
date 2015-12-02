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
        void Add(UserProductDto userProduct);
        void Update(UserProductDto userProduct);
        void Delete(int id);
    }
}
