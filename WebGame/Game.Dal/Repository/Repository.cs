using Game.Dal.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Dal.Repository
{
    public class Repository<T> : IRepository<T>
        where T : class
    {
        private gameDbContext _context;
        private IDbSet<T> _dbSet;


        public Repository(IDbFactory factory)
        {
            _context = factory.Get();
            _dbSet = _context.Set<T>();
        }


        public void Add(T entry)
        {
            _dbSet.Add(entry);
        }


        public void Update(T entry)
        {
            //_dbSet.Attach(entry);
            _context.Entry(entry).State = EntityState.Modified;
            _context.SaveChanges();
        }


        public void Delete(T entry)
        {
            _dbSet.Attach(entry);
            _dbSet.Remove(entry);
        }


        public void Delete(System.Linq.Expressions.Expression<Func<T, bool>> query)
        {
            foreach (var item in _dbSet.Where(query))
                Delete(item);
        }


        public T Get(System.Linq.Expressions.Expression<Func<T, bool>> single)
        {
            return _dbSet.SingleOrDefault(single);
        }


        public T Get(int id)
        {
            return _dbSet.Find(id);
        }


        public IQueryable<T> GetAll()
        {
            return _dbSet;
        }
    }
}
