using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinkedInDemoProjext.WebApi.Models;
using LinkedInDemoProjext.WebApi.Models.Contexts;
using LinkedInDemoProjext.WebApi.Models.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LinkedInDemoProjext.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserPostsController : WebBaseController
    {
        private readonly IDataRepository<UserPosts> _dataRepository;
        private readonly UsersContext _usersContext;
        public UserPostsController(IDataRepository<UserPosts> dataRepository, UsersContext usersContext)
        {
            _dataRepository = dataRepository;
            _usersContext = usersContext;
        }
        // GET: api/UserPosts
        [HttpGet]
        public IActionResult Get()
        {
            string userId = _usersContext.Users.FirstOrDefault(u => u.Token == UserIdentityToken).Id;

            IEnumerable<UserPosts> UserPosts = _dataRepository.GetAll(userId);
            return Ok(UserPosts);
        }

        // GET: api/UserPosts/5
        [HttpGet("{id}", Name = "GetUserPost")]
        public IActionResult Get(string id)
        {
            UserPosts userPost = _dataRepository.Get(id);

            if (userPost == null)
            {
                return NotFound("User post record couldn't be found.");
            }

            return Ok(userPost);
        }

        // POST: api/UserPosts
        [HttpPost]
        public IActionResult Add([FromBody] UserPosts userPost)
        {
            if (userPost == null)
            {
                return BadRequest("User post is Null");
            }

            string userId = _usersContext.Users.FirstOrDefault(u => u.Token == UserIdentityToken).Id;

            userPost.refUserId = userId;
            userPost.Id = Guid.NewGuid().ToString();
            _dataRepository.Add(userPost);

            return CreatedAtAction("Get", new { Id = userPost.Id }, userPost);
        }

        // PUT: api/UserPosts/5
        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] UserPosts userPost)
        {
            if (userPost == null)
            {
                return BadRequest("User post is Null");
            }

            UserPosts userPostToUpdate = _dataRepository.Get(id);
            if (userPostToUpdate == null)
            {
                return NotFound("User post record couldn't be found.");
            }

            _dataRepository.Update(userPostToUpdate, userPost);
            return NoContent();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            UserPosts userPost = _dataRepository.Get(id);
            if (userPost == null)
            {
                return NotFound("User post record couldn't be found.");
            }

            _dataRepository.Delete(userPost);
            return NoContent();
        }
    }
}
