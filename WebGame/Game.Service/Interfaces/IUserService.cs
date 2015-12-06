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
        void Add(UserDto user);
        void Update(UserDto user);
        void Delete(int id);
        DolarDto UserDolar();
        UserDto Profil(string User);
        bool ChangePass(UserDto user, string User);
        bool ChangeEmail(UserDto user, string User);

    }
}
