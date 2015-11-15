using Game.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Service.Interfaces.TableInterface
{
    public interface IUserProductTableService
    {
        List<UserProductDto> GetUserProduct();
        void Add(UserProductDto userProduct);
        void Update(UserProductDto userProduct, int id);
        void Delete(int id);
    }
}
