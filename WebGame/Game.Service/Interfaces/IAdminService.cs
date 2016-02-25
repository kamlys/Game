using Game.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Service.Interfaces
{
    public interface IAdminService
    {
        bool ifAdmin(string User);
        List<AdminDto> GetAdmin();
        bool Add(AdminDto admin);
        bool Update(AdminDto admin);
        bool Delete(int id);

    }
}
