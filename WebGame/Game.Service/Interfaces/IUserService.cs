﻿using Game.Core.DTO;
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
    }
}
