using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Model;

namespace TaskTracker.Data.Repository.IRepository
{
    public interface IProjectRepository : IRepository<Project>
    {
        void Update(Project obj);
        void Save();
        Project GetById(int id);
        ICollection<Model.Task> GetProjectTasks(int id);
        bool ProjectExist(int id);
    }
}
