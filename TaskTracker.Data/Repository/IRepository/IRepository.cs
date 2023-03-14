using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracker.Data.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        // T - Category (for example)

        T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperies = null, bool tracked = true);
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperies = null);
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);
        bool Exist(int id);
    }
}
