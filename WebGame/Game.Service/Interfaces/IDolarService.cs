using Game.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Service.Interfaces.TableInterface
{
    public interface IDolarService
    {
        List<DolarDto> GetDolars();
        void Add(DolarDto admin);
        void Update(DolarDto admin);
        void Delete(int id);

    }
}
