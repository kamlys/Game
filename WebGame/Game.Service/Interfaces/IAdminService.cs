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
        void Add(AdminDto admin);
        void Update(AdminDto admin);
        void Delete(int id);

    }
}
