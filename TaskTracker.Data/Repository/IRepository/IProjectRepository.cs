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
        Project GetById(int id);
        void Update(Project obj);

        void Save();
    }
}
