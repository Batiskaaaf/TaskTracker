using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskTracker.Data;
using TaskTracker.Data.Repository.IRepository;
using TaskTracker.Model;
using TaskTracker.Model.DTO;
using System.Linq;

namespace TaskTracker.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectRepository repository;
        private readonly IMapper mapper;

        public ProjectsController(IProjectRepository repository
                                , IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectDTO>>> Get()
        {
            var projects = repository.GetAll();
            var projectsDTO = mapper.Map<List<ProjectDTO>>(projects);

            return Ok(projectsDTO);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectDTO>> Get(int id)
        {
            var project = repository.GetById(id);
            if (project == null)
                return NotFound();

            var projectDTO = mapper.Map<ProjectDTO>(project);
            return Ok(projectDTO);
        }

        [HttpGet("{id}/tasks")]
        public async Task<ActionResult<ICollection<TaskDTO>>> GetProjectTasks (int id)
        {
            if (!repository.ProjectExist(id))
                return BadRequest();

            var tasks = repository.GetProjectTasks(id);
            var tasksDto = mapper.Map<ICollection<TaskDTO>>(tasks);
            return Ok(tasksDto);
        }

        [HttpPost]
        public async Task<ActionResult> Create ([FromBody] ProjectDTO projectDTO)
        {
            if (projectDTO == null)
                return BadRequest(ModelState);

            var project = mapper.Map<Project>(projectDTO);
            repository.Add(project);
            repository.Save();
            return Ok("Project created succesfully");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete (int id)
        {
            var project = repository.GetById(id);
            if (project == null)
                return NotFound();

            repository.Remove(project);
            repository.Save();
            return Ok("Project deleted succesfully");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProjectDTO>> Update (int id, [FromBody] ProjectDTO projectDTO)
        {
            if(projectDTO == null || id != projectDTO.Id)
                return BadRequest();
            var project = repository.GetById(id);
            if (project == null)
                return NotFound();

            project.Status = projectDTO.Status;
            project.Description = projectDTO.Description;
            project.StartDate = projectDTO.StartDate;
            project.DueDate = projectDTO.DueDate;
            project.Name = projectDTO.Name;

            try
            {
                repository.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return Ok("Project edited succesfully");
        }

    }
}
