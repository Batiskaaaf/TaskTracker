using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskTracker.Data;
using TaskTracker.Data.Repository.IRepository;
using TaskTracker.Model;
using TaskTracker.Model.DTO;

namespace TaskTracker.Api.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectRepository repo;
        private readonly IMapper mapper;

        public ProjectsController(IProjectRepository repository
                                ,IMapper mapper)
        {
            this.repo = repository;
            this.mapper = mapper;
        }

        // GET / Projects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectDTO>>> Get()
        {
            var projects = repo.GetAll();
            var projectsDTO = mapper.Map<List<ProjectDTO>>(projects);

            return Ok(projectsDTO);
        }

        // GET / Projects/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectDTO>> Get(int id)
        {
            var project = repo.GetById(id);
            if (project == null)
                return NotFound();

            var projectDTO = mapper.Map<ProjectDTO>(project);
            return Ok(projectDTO);
        }

        // POST / Projects
        [HttpPost]
        public async Task<ActionResult> Create ([FromBody] ProjectDTO projectDTO)
        {
            if (projectDTO == null)
                return BadRequest(ModelState);

            var project = mapper.Map<Project>(projectDTO);
            repo.Add(project);
            repo.Save();
            return Ok("Project Created Succesfully");
        }

        // DELETE / Projects/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete (int id)
        {
            var project = repo.GetById(id);
            if (project == null)
                return NotFound();

            repo.Remove(project);
            repo.Save();
            return Ok("Project deleted succesfully");
        }

        // PUT / Projects/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<ProjectDTO>> Edit (int id, [FromBody] ProjectDTO projectDTO)
        {
            if(projectDTO == null || id != projectDTO.Id)
                return BadRequest();
            var project = repo.GetById(id);
            if (project == null)
                return NotFound();

            project.Status = projectDTO.Status;
            project.Description = projectDTO.Description;
            project.StartDate = projectDTO.StartDate;
            project.DueDate = projectDTO.DueDate;
            project.Name = projectDTO.Name;

            try
            {
                repo.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return CreatedAtAction(nameof(Get), new { id = projectDTO.Id }, projectDTO);

        }

    }
}
