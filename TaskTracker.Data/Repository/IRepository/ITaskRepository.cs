using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Model;

namespace TaskTracker.Data.Repository.IRepository
{
    public interface ITaskRepository : IRepository<Model.Task>
    {
        void Update(Model.Task obj);
    }
}
