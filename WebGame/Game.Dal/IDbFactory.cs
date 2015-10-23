using Game.Dal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Dal
{
    public interface IDbFactory
    {
        gameDbContext Get();
    }
}
