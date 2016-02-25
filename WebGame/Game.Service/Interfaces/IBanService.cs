using Game.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Service.Interfaces.TableInterface
{
    public interface IBanService
    {
        List<BanDto> GetBan();
        bool Add(BanDto admin);
        bool Update(BanDto admin);
        bool Delete(int id);
    }
}
