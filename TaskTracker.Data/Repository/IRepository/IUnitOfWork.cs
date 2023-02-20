using TaskTracker.Data;

namespace TaskTracker.Data.Repository.IRepository
{
    public interface IUnitOfWork
    {
        public IProjectRepository Project { get; }
        public ITaskRepository Task { get; }
        public IAccountRepository Account { get; }
        void Save();
    }
}
