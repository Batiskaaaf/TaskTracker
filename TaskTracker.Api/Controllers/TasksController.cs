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
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public TasksController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        ///<summary>
        ///Get task by id
        ///</summary>
        [HttpGet("{id}")]
        public ActionResult<TaskDTO> Get(int id)
        {
            var task = unitOfWork.Task.GetFirstOrDefault(t => t.Id == id);
            if(task == null)
                return NotFound();
            var taskDTO = mapper.Map<TaskDTO>(task);
            return Ok(taskDTO);
        }

        ///<summary>
        ///Create new task
        ///</summary>
        [HttpPost]
        public ActionResult Create([FromBody] TaskDTO taskDTO)
        {
            if (taskDTO == null)
                return BadRequest(ModelState);

            var task = mapper.Map<Model.Task>(taskDTO);
            unitOfWork.Task.Add(task);
            unitOfWork.Save();

            return Ok("Task created succesfully");
        }

        ///<summary>
        ///Update task by id
        ///</summary>
        [HttpPut("{id}")]
        public ActionResult<TaskDTO> Update(int id, [FromBody] TaskDTO taskDTO)
        {
            if (taskDTO == null || id != taskDTO.Id)
                return BadRequest(ModelState);

            if(!unitOfWork.Task.Exist(id))
                return NotFound();


            var taskToUpdate = mapper.Map<Model.Task>(taskDTO);
            unitOfWork.Task.Update(taskToUpdate);
            unitOfWork.Save();

            try
            {
                unitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return Ok("Task edited succesfully");
        }

        ///<summary>
        ///Delete task by id
        ///</summary>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var task = unitOfWork.Task.GetFirstOrDefault(t => t.Id == id);
            if (task == null)
                return NotFound();

            unitOfWork.Task.Remove(task);
            unitOfWork.Save();
            return Ok("Task deleted succesfully");
        }
    }
}
