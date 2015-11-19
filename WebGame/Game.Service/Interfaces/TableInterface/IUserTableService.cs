using Game.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Service.Interfaces.TableInterface
{
    public interface IUserTableService
    {
        List<UserDto> GetUser();
        void Add(UserDto admin);
        void Update(UserDto admin, int id);
        void Delete(int id);
    }
}
