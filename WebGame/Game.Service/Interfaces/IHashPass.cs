using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Service.Interfaces
{
    public interface IHashPass
    {
        string GetPassword(string message);
        string GeneratePassword(string password);
        bool ValidationPassword(string password, string hashPassword);
        string DescryptPassword(string pass);
    }
}
