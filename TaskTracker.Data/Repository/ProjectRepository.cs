using Microsoft.EntityFrameworkCore;
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
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {

        private readonly TaskTrackerDbContext db;

        public ProjectRepository(TaskTrackerDbContext db) : base(db)
        {
            this.db = db;
        }

        public Project GetById(int id)
        {
            return GetFirstOrDefault(x => x.Id == id);
        }

        public ICollection<Model.Task> GetProjectTasks(int id)
        {
            return db.Projects.Where(p => p.Id == id).Include(x => x.Tasks).FirstOrDefault().Tasks;
        }

        public bool ProjectExist(int id)
        {
            return db.Projects.Any(p => p.Id == id);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(Project obj)
        {
            db.Projects.Update(obj);
        }
    }
}
