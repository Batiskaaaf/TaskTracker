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
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskRepository repository;
        private readonly IMapper mapper;

        public TasksController(ITaskRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        // GET api/<TasksController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskDTO>> Get(int id)
        {
            var task = repository.GetById(id);
            if(task == null)
                return NotFound();
            var taskDTO = mapper.Map<TaskDTO>(task);
            return Ok(taskDTO);
        }

        // POST api/<TasksController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TaskDTO taskDTO)
        {
            if (taskDTO == null)
                return BadRequest(ModelState);

            if (!repository.ProjectExist(taskDTO.ProjectId))
                return NotFound("Project does not exist");

            var task = mapper.Map<Model.Task>(taskDTO);
            repository.Add(task);
            repository.Save();

            return Ok("Task created succesfully");
        }

        // PUT api/<TasksController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<TaskDTO>> Update(int id, [FromBody] TaskDTO taskDTO)
        {
            if (taskDTO == null || id != taskDTO.Id)
                return BadRequest(ModelState);

            var task = repository.GetById(id);
            if (task == null)
                return NotFound();

            task.Name = taskDTO.Name;
            task.Description = taskDTO.Description;
            task.Status = taskDTO.Status;
            task.DueDate = taskDTO.DueDate;
            task.StartDate = taskDTO.StartDate;

            repository.Save();

            try
            {
                repository.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return Ok("Task edited succesfully");
        }

        // DELETE api/<TasksController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var task = repository.GetById(id);
            if (task == null)
                return NotFound();

            repository.Remove(task);
            repository.Save();
            return Ok("Task deleted succesfully");
        }
    }
}
