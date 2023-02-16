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
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TasksController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TasksController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TasksController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
