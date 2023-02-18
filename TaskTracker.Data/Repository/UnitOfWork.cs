using TaskTracker.Data.Repository.IRepository;

namespace TaskTracker.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TaskTrackerDbContext db;

        public IProjectRepository Project { get; private set; }
        public ITaskRepository Task {get; private set;}

        public UnitOfWork(TaskTrackerDbContext db)
        {
            this.db = db;
            Project = new ProjectRepository(db);
            Task = new TaskRepository(db);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
