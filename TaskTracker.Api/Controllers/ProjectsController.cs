using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskTracker.Data.Repository.IRepository;
using TaskTracker.Model;
using TaskTracker.Model.DTO;

namespace TaskTracker.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ProjectsController(IUnitOfWork unitOfWork
                                , IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        ///<summary>
        ///Get all available projects
        ///</summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectDTO>>> Get()
        {
            var projects = unitOfWork.Project.GetAll();
            var projectsDTO = mapper.Map<List<ProjectDTO>>(projects);

            return Ok(projectsDTO);
        }

        
        ///<summary>
        ///Get project by id
        ///</summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectDTO>> Get(int id)
        {
            var project = unitOfWork.Project.GetById(id);
            if (project == null)
                return NotFound();

            var projectDTO = mapper.Map<ProjectDTO>(project);
            return Ok(projectDTO);
        }

        ///<summary>
        ///Get tasks by project id
        ///</summary>
        [HttpGet("{id}/tasks")]
        public async Task<ActionResult<ICollection<TaskDTO>>> GetProjectTasks (int id)
        {
            if (!unitOfWork.Project.Exist(id))
                return BadRequest();

            var tasks = unitOfWork.Project.GetProjectTasks(id);
            var tasksDto = mapper.Map<ICollection<TaskDTO>>(tasks);
            return Ok(tasksDto);
        }

        ///<summary>
        ///Create new project
        ///</summary>
        [HttpPost]
        public async Task<ActionResult> Create ([FromBody] ProjectDTO projectDTO)
        {
            if (projectDTO == null)
                return BadRequest(ModelState);

            var project = mapper.Map<Project>(projectDTO);
            unitOfWork.Project.Add(project);
            unitOfWork.Save();
            return Ok("Project created succesfully");
        }

        ///<summary>
        ///Delete project by id
        ///</summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete (int id)
        {
            var project = unitOfWork.Project.GetById(id);
            if (project == null)
                return NotFound();

            unitOfWork.Project.Remove(project);
            unitOfWork.Save();
            return Ok("Project deleted succesfully");
        }

        ///<summary>
        ///Updates project by id
        ///</summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<ProjectDTO>> Update (int id, [FromBody] ProjectDTO projectDTO)
        {
            if(projectDTO == null || id != projectDTO.Id)
                return BadRequest();
            var project = unitOfWork.Project.GetById(id);
            if (project == null)
                return NotFound();

            project.Status = projectDTO.Status;
            project.Description = projectDTO.Description;
            project.StartDate = projectDTO.StartDate;
            project.DueDate = projectDTO.DueDate;
            project.Name = projectDTO.Name;

            try
            {
                unitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return Ok("Project edited succesfully");
        }

    }
}
