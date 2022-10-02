using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskTracker.Data;
using TaskTracker.Model;

namespace TaskTracker.Api.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly ILogger<ProjectsController> logger;
        private readonly TaskTrackerDbContext context;

        public ProjectsController(TaskTrackerDbContext context
                                ,ILogger<ProjectsController> logger)
        {
            this.context = context;
            this.logger = logger;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
        {
            var projects = await context.Projects.ToListAsync();
            if (projects == null)
                return NotFound();
            return Ok(projects);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProject(int id)
        {
            var project = await context.Projects.FindAsync(id);
            if (project == null)
                return NotFound();
            return Ok(project);
        }

        [HttpPost]
        public async Task<ActionResult<Project>> CreateProject (Project project)
        {
            context.Projects.Add(project);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProject), new {id = project.Id}, project);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject (int id)
        {
            var project = await context.Projects.FindAsync(id);
            if (project == null)
                return NotFound();
            context.Projects.Remove(project);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Project>> UpdateProject (int id, Project project)
        {
            if(id != project.Id)
                return BadRequest();
            var projectFromDb = await context.Projects.FindAsync(id);
            if (projectFromDb == null)
                return NotFound();

            projectFromDb.Status = project.Status;
            projectFromDb.Description = project.Description;
            projectFromDb.StartDate = project.StartDate;
            projectFromDb.DueDate = project.DueDate;
            projectFromDb.Name = project.Name;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!context.Projects.Any(x => x.Id == id))
            {
                return NotFound();
            }

            return CreatedAtAction(nameof(GetProject), new { id = projectFromDb.Id }, projectFromDb);

        }

    }
}
