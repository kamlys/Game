using Game.Dal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Dal
{
    public class DbFactory : IDbFactory
    {
        private gameDbContext _context;

        public Model.gameDbContext Get()
        {
            return _context ?? (_context = new gameDbContext());
        }
    }
}
