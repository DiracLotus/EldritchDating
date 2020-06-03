using System.Threading.Tasks;
using EldritchDating.API.Data;
using EldritchDating.API.DTOs;
using EldritchDating.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace EldritchDating.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository repo;

        public AuthController(IAuthRepository repo)
        {
            this.repo = repo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto) {
            // TODO validate

            userForRegisterDto.Username = userForRegisterDto.Username.ToLower();

            if (await repo.UserExists(userForRegisterDto.Username))
                return BadRequest("Username already exists");

            var userToCreate = new User {
                UserName = userForRegisterDto.Username
            };

            var createdUser = await repo.Register(userToCreate, userForRegisterDto.Password);

            // TODO createdatroute
            return StatusCode(201);
        }
    }
}