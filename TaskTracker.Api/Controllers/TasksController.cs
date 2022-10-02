using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskTracker.Data;
using TaskTracker.Model;

namespace TaskTracker.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ILogger logger;
        private readonly TaskTrackerDbContext context;
        
        public TasksController(ILogger logger, TaskTrackerDbContext context)
        {
            this.logger = logger;
            this.context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Model.Task>> GetTask(int id)
        {
            var task = await context.Tasks.FirstOrDefaultAsync(t => t.Id == id);
            if (task == null)
                return NotFound();
            return Ok(task);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Model.Task>>> GetTasksForProject (int projectId)
        {
            var tasks = context.Tasks.Where(t => t.ProjectId == projectId).OrderBy(t => t.CreatedDate).ToListAsync();
            if (tasks == null)
                return NotFound();
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Model.Task>>> GetSubtasksForTask (int taskId)
        {
            var tasks = context.Tasks.Where(t => t.FatherTaskId == taskId).OrderBy(t => t.CreatedDate).ToListAsync();
            if (tasks == null)
                return NotFound();
            return Ok(tasks);
        }

        [HttpPost]
        public async Task<ActionResult<Model.Task>> CreateTask (Model.Task task)
        {
            context.Tasks.Add(task);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTask), new {id = task.Id}, task);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTask (int id)
        {
            var task = await context.Tasks.FindAsync(id);
            if (task == null)
                return NotFound();
            context.Tasks.Remove(task);
            await context.SaveChangesAsync();
            return NoContent();
        }
        
        [HttpPut("{id}")]

        public async Task<ActionResult<Model.Task>> UpdateTask(int id, Model.Task task)
        {
            if (id == task.Id)
                return BadRequest();
            var taskFromDb = await context.Tasks.FindAsync(id);
            if (taskFromDb == null)
                return NotFound();
            
            taskFromDb.Name = task.Name;
            taskFromDb.Description = task.Description;
            taskFromDb.StartDate = task.StartDate;
            taskFromDb.DueDate = task.DueDate;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!context.Tasks.Any(x => x.Id == id))
            {
                return NotFound();
            }

            return CreatedAtAction(nameof(GetTask), new { id = taskFromDb.Id }, taskFromDb);
        }
    }
}
