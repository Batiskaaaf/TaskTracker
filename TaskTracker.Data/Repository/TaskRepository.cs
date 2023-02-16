using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Data.Repository.IRepository;
using TaskTracker.Model;

namespace TaskTracker.Data.Repository
{
    public class TaskRepository : Repository<Model.Task>, ITaskRepository
    {

        private readonly TaskTrackerDbContext db;

        public TaskRepository(TaskTrackerDbContext db) : base(db)
        {
            this.db = db;
        }

        public Model.Task GetById(int id)
        {
            return GetFirstOrDefault(x => x.Id == id);
        }

        public bool isExist(int id)
        {
            return db.Tasks.Any(t => t.Id == id);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(Model.Task obj)
        {
            db.Tasks.Update(obj);
        }
    }
}
