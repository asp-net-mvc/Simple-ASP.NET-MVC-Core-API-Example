using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LinkedInDemoProjext.WebApi.Models;
using LinkedInDemoProjext.WebApi.Models.Contexts;
using LinkedInDemoProjext.WebApi.Models.DataManagers;
using LinkedInDemoProjext.WebApi.Models.Repositories;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using LinkedInDemoProjext.WebApi.Helpers;
using Microsoft.Extensions.Configuration;

namespace LinkedInDemoProjext.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : WebBaseController
    {
        private readonly IDataRepository<Users> _dataRepository;

        private readonly UserManager<Users> _userManager;

        private readonly IConfiguration _config;

        public UsersController(IDataRepository<Users> dataRepository, UserManager<Users> userManager, IConfiguration config)
        {
            _dataRepository = dataRepository;
            _userManager = userManager;
            //_usersContext = context;
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            var user = _userManager.Users.FirstOrDefault(u => u.UserName == loginModel.Username);
            var password = _userManager.Users.FirstOrDefault(u => u.PasswordHash == PasswordHash.CreateMd5Hash(loginModel.Password));

            if (user != null && password != null)
            {
                var authClaims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var appSettingJwtToken = _config.GetValue<string>("JwtToken");

                var authSigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettingJwtToken));

                var token = new JwtSecurityToken(
                    issuer: "http://localhost:57930",
                    audience: "http://localhost:57930",
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256)
                    );

                var userToken = new JwtSecurityTokenHandler().WriteToken(token);
                user.Token = userToken;
                _dataRepository.Update(user, user);

                return Ok(
                    new {
                        token = userToken,
                        expiration = token.ValidTo
                    });
            }

            return Unauthorized();
        }

        // GET: api/Users
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Users> users = _dataRepository.GetAll();
            return Ok(users);
        }

        // GET: api/Users/5
        [HttpGet("{id}", Name = "GetUser")]
        public IActionResult Get(string id)
        {
            Users user = _dataRepository.Get(id);

            if(user == null)
            {
                return NotFound("User record couldn't be found.");
            }

            return Ok(user);
        }

        // POST: api/Users
        [HttpPost]
        public IActionResult Add([FromBody] Users user)
        {
            if(user == null)
            {
                return BadRequest("User is Null");
            }

            user.Id = Guid.NewGuid().ToString();
            _dataRepository.Add(user);

            return CreatedAtAction("Get", new { Id = user.Id }, user);
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] Users user)
        {
            if(user == null)
            {
                return BadRequest("User is Null");
            }

            Users userToUpdate = _dataRepository.Get(id);
            if(userToUpdate == null)
            {
                return NotFound("User record couldn't be found.");
            }

            _dataRepository.Update(userToUpdate, user);
            return NoContent();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            Users user = _dataRepository.Get(id);
            if(user == null)
            {
                return NotFound("User record couldn't be found.");
            }

            _dataRepository.Delete(user);
            return NoContent();
        }
    }
}
