using System.Threading.Tasks;
using EldritchDating.API.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EldritchDating.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IDatingRepository repository;

        public UsersController(IDatingRepository repo)
        {
            this.repository = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers() 
        {
            var users = repository.GetUsers();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = repository.GetUser(id);

            return Ok(user);
        }
    }
}