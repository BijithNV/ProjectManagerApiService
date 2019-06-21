using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ProjectManagerBusinessLayer;
using ProjectManagerEntities;


namespace ProjectManagerApiService.API.Controllers
{
    [Route("api/[controller]")]   
    [EnableCors("CorsPolicy")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectMangerBL<Project> _projectBL;

        public ProjectController(IProjectMangerBL<Project> projectBL)
        {
            _projectBL = projectBL;
        }

        // GET: api/Project
        [HttpGet]
        public IEnumerable<Project> Get()
        {
            return _projectBL.RetrieveAllData();
        }

        // GET: api/Project/GetById
        [HttpGet]
        [Route("GetById")]
        public Project GetById(int id)
        {
            return _projectBL.GetById(id);
        }

        // GET: api/Project/Search
        [HttpGet]
        [Route("Search")]
        public IEnumerable<Project> Search(string searchText)
        {
            return _projectBL.SearchByKey(searchText);
        }

        // POST: api/Project
        [HttpPost]
        public IActionResult Post([FromBody] Project project)
        {
            if (ModelState.IsValid)
            {
                _projectBL.CreateNew(project);
                return Ok();
            }
            return BadRequest();
        }

        // PUT: api/Project/5
        [HttpPut]
        public IActionResult Put([FromBody] Project project)
        {
            if (ModelState.IsValid)
            {
                _projectBL.Update(project);
                return Ok();
            }
            return BadRequest();
        }

        // DELETE: api/Project/5
        [HttpDelete()]
        public IActionResult Delete(int id)
        {
            if (id > 0)
            {
                _projectBL.Delete(id);
                return Ok();
            }
            return BadRequest();
        }
    }
}
