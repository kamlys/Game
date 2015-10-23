using Game.Dal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Dal
{
    public class UnitOfWork : IUnitOfWork
    {
        private gameDbContext _context;


        public UnitOfWork(IDbFactory factory)
        {
            _context = factory.Get();
        }


        public void Commit()
        {
            _context.SaveChanges();
        }
    }
}
