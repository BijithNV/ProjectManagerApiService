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
    public class UserController : ControllerBase
    {
        private readonly IProjectMangerBL<User> _userBL;

        public UserController(IProjectMangerBL<User> userBL)
        {
            _userBL = userBL;
        }

        // GET: api/User
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _userBL.RetrieveAllData();
        }

        // GET: api/User/Search
        [HttpGet]
        [Route("Search")]
        public IEnumerable<User> Search(string searchText)
        {
            return _userBL.SearchByKey(searchText);
        }

        // POST: api/User
        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            if (ModelState.IsValid)
            {
                _userBL.CreateNew(user);
                return Ok();
            }
            return BadRequest();
        }

        // PUT: api/User/5
        [HttpPut]
        public IActionResult Put([FromBody] User user)
        {
            if (ModelState.IsValid)
            {
                _userBL.Update(user);
                return Ok();
            }
            return BadRequest();
        }

        // DELETE: api/User/5
        [HttpDelete()]
        public IActionResult Delete(int id)
        {
            if (id > 0)
            {
                _userBL.Delete(id);
                return Ok();
            }
            return BadRequest();
        }
    }
}
