using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ProjectManagerEntities;
using ProjectManagerBusinessLayer;

namespace ProjectManagerApiService.API.Controllers
{
    [Route("api/[controller]")]   
    [EnableCors("CorsPolicy")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskBL _taskBL;

        public TaskController(ITaskBL taskBL)
        {
            _taskBL = taskBL;
        }

        // GET: api/Task
        [HttpGet]
        public IEnumerable<Task> Get()
        {
            return _taskBL.RetriveAllTasks();
        }

        // GET: api/Task/id
        [HttpGet]
        [Route("GetParentTasks")]
        public IEnumerable<Task> GetParentTasks()
        {
            return _taskBL.RetriveAllParentTasks();
        }

        // GET: api/Task/id
        [HttpGet]
        [Route("GetById")]
        public Task GetById(int id)
        {
            return _taskBL.SearchTaskById(id);
        }

        // GET: api/Task
        [HttpGet]
        [Route("GetByProjectId")]
        public IEnumerable<Task> GetByProjectId(int projectId)
        {
            return _taskBL.RetriveTasksByProjectId(projectId);
        }

        // POST: api/Task
        [HttpPost]
        public IActionResult Post([FromBody] Task task)
        {
            if (ModelState.IsValid)
            {
                _taskBL.CreateNewTask(task);
                return Ok();
            }
            return BadRequest();
        }

        // PUT: api/Task/5
        [HttpPut]
        public IActionResult Put([FromBody] Task task)
        {
            if (ModelState.IsValid)
            {
                _taskBL.UpdateTask(task);
                return Ok();
            }
            return BadRequest();
        }

        // DELETE: api/Task/5
        [HttpDelete()]
        public IActionResult Delete(int id)
        {
            if (id > 0)
            {
                _taskBL.CloseTask(id);
                return Ok();
            }
            return BadRequest();
        }
    }
}
