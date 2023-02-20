using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using TaskTracker.Data.Repository.IRepository;
using TaskTracker.Model;

namespace TaskTracker.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    { 
        private readonly TaskTrackerDbContext db;
        public IProjectRepository Project { get; private set; }
        public ITaskRepository Task {get; private set;}
        public IAccountRepository Account {get; private set;}

        public UnitOfWork(TaskTrackerDbContext db,
                        UserManager<ApplicationUser> userManager,
                        SignInManager<ApplicationUser> signInManager,
                        IConfiguration configuration)
        {
            this.db = db;
            Project = new ProjectRepository(db);
            Task = new TaskRepository(db);
            Account = new AccountRepository(userManager, signInManager,configuration);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
