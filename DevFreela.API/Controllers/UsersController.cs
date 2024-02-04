using DevFreela.Application.InputModels;
using DevFreela.API.Models;
using DevFreela.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using DevFreela.Application.Commands.CreateUser;
using DevFreela.Application.Queries.GetUser;

namespace DevFreela.API.Controllers
{
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMediator _mediator;
        public UsersController(IUserService userService, IMediator mediator)
        {
            _userService = userService;
            _mediator = mediator;
        }

        // api/users/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetUserByIdQuery(id);

            //var user = _userService.GetById(id);   // usado do UserService

            var user = await _mediator.Send(query);

            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);                       
        }

        // api/users
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUserCommand createUserCommand)
        {
            //var id = _userService.Create(newUserInputModel);   // usado do UserService

            var id = await _mediator.Send(createUserCommand);

            return CreatedAtAction(nameof(GetById), new { id = id }, createUserCommand);
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
