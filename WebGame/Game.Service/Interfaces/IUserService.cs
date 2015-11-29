using Game.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Service.Interfaces
{
    public interface IUserService
    {
        bool RegisterUser(UserDto user);
        bool LoginUser(UserDto user);
        List<UserDto> GetUser();
        void Add(UserDto admin);
        void Update(UserDto admin, int id);
        void Delete(int id);
    }
}
