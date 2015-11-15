using Game.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Service.Interfaces.TableInterface
{
    public interface IAdminTableService
    {
        List<AdminDto> GetAdmins();
        void Add(AdminDto admin);
        void Update(AdminDto admin, int id);
        void Delete(int id);

    }
}
