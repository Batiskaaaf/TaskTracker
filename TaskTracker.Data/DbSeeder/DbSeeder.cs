using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Model;
using TaskTracker.Utility;

namespace TaskTracker.Data.DbSeeder
{
    public class DbSeeder : IDbSeeder
    {
        private readonly TaskTrackerDbContext context;
        public DbSeeder(TaskTrackerDbContext context)
        {
            this.context = context;
        }
        public void Seed()
        {
            if (context.Projects.Any())
                return;
            var project = new Project() {
                Name = "Main Project",
                Status = Constants.Task_NotStarted 
            };
            var task1 = new Model.Task()
            {
                Name = "First Task",
                Description = "First Task Description",
                Status = Constants.Task_NotStarted,
                Project = project
            };
            var subtask1 = new Model.Task()
            {
                Name = "First task subtask one",
                Description = "First Task Subtask one Description",
                Status = Constants.Task_NotStarted,
                Project = project,
                FatherTask = task1,
            };
            var subtask2 = new Model.Task()
            {
                Name = "First task subtask two",
                Description = "First Task Subtask two Description",
                Status = Constants.Task_NotStarted,
                Project = project,
                FatherTask = task1,
            };            
            var task2 = new Model.Task()
            {
                Name = "Second Task",
                Description = "Second Task Description",
                Status = Constants.Task_NotStarted,
                Project = project
            };
            var list = new List<Model.Task>() { subtask1, subtask2 };
            context.Projects.Add(project);
            context.Tasks.Add(task1);
            context.Tasks.Add(task2);
            context.SaveChanges();
        }
    }
}
