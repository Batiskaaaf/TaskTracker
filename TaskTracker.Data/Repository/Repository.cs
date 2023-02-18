using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Data.Repository.IRepository;

namespace TaskTracker.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly TaskTrackerDbContext db;
        internal DbSet<T> dbSet;

        public Repository(TaskTrackerDbContext db)
        {
            this.db = db;
            this.dbSet = db.Set<T>();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            dbSet.AddRange(entities);
        }

        public bool Exist(int id)
        {
            return dbSet.Find(id) != null;
        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query = dbSet;
            return query.ToList();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperies = null)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
                query = query.Where(filter);

            if (includeProperies != null)
            {
                foreach (var property in includeProperies.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }
            return query.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperies = null, bool tracked = true)
        {
            IQueryable<T> query = dbSet;

            if (tracked)
                query = dbSet;
            else
                query = dbSet.AsNoTracking();

            query = query.Where(filter);
            if (includeProperies != null)
            {
                foreach (var property in includeProperies.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }
            return query.FirstOrDefault();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }
    }
}
