using DevFreela.Application.InputModels;
using DevFreela.API.Models;
using DevFreela.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // api/users/1
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _userService.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);                       
        }

        // api/users
        [HttpPost]
        public IActionResult Post([FromBody] NewUserInputModel newUserInputModel)
        {
            var id = _userService.Create(newUserInputModel);

            return CreatedAtAction(nameof(GetById), new { id = id }, newUserInputModel);
        }

        // api/users/1/login
        [HttpPut("{id}/login")]
        public IActionResult Login(int id, [FromBody] LoginModel login)
        {
            // TODO: Para módulo de autenticação e autorização

            return NoContent();
        }
    }
}
