using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskTracker.Data;
using TaskTracker.Model;
using TaskTracker.Model.DTO;

namespace TaskTracker.Api.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly ILogger<ProjectsController> logger;
        private readonly TaskTrackerDbContext context;
        private readonly IMapper mapper;

        public ProjectsController(TaskTrackerDbContext context
                                ,ILogger<ProjectsController> logger
                                ,IMapper mapper)
        {
            this.context = context;
            this.logger = logger;
            this.mapper = mapper;
        }

        // GET / Projects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectDTO>>> GetProjects()
        {
            var projects = await context.Projects.ToListAsync();
            if (projects == null)
                return NotFound();
            var projectsDTO = projects.Select(x => mapper.Map<ProjectDTO>(x));
            return Ok(projectsDTO);
        }

        // GET / Projects/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectDTO>> GetProject(int id)
        {
            var project = await context.Projects.FindAsync(id);
            if (project == null)
                return NotFound();
            var projectDTO = mapper.Map<ProjectDTO>(project);
            return projectDTO;
        }

        // POST / Projects
        [HttpPost]
        public async Task<ActionResult<ProjectDTO>> CreateProject (ProjectDTO projectDTO)
        {
            var project = mapper.Map<Project>(projectDTO);
            context.Projects.Add(project);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProject), new {id = project.Id}, projectDTO);
        }

        // DELETE / Projects/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProject (int id)
        {
            var project = await context.Projects.FindAsync(id);
            if (project == null)
                return NotFound();
            context.Projects.Remove(project);
            await context.SaveChangesAsync();
            return NoContent();
        }

        // PUT / Projects/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<ProjectDTO>> UpdateProject (int id, ProjectDTO projectDTO)
        {
            if(id != projectDTO.Id)
                return BadRequest();
            var project = await context.Projects.FindAsync(id);
            if (project == null)
                return NotFound();

            project.Status = projectDTO.Status;
            project.Description = projectDTO.Description;
            project.StartDate = projectDTO.StartDate;
            project.DueDate = projectDTO.DueDate;
            project.Name = projectDTO.Name;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!context.Projects.Any(x => x.Id == id))
            {
                return NotFound();
            }

            return CreatedAtAction(nameof(GetProject), new { id = projectDTO.Id }, projectDTO);

        }

    }
}
