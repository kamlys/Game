using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Game.Dal.Repository
{
    public interface IRepository<T>
    {
        void Add(T entry);
        void Update(T entry);
        void Delete(T entry);

        void Delete(Expression<Func<T, bool>> query);
        T Get(Expression<Func<T, bool>> query);

        T Get(int id);
        IQueryable<T> GetAll();
    }
}
