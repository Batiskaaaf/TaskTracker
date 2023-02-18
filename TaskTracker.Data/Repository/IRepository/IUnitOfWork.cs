using TaskTracker.Data;

namespace TaskTracker.Data.Repository.IRepository
{
    public interface IUnitOfWork
    {
        public IProjectRepository Project { get; }
        public ITaskRepository Task { get; }
        void Save();
    }
}
