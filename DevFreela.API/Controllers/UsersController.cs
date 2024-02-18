using DevFreela.Application.Commands.CreateUser;
using DevFreela.Application.Commands.LoginUser;
using DevFreela.Application.Queries.GetUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Route("api/users")]
    [Authorize]    // annotation que indica que os métodos precisam de um usuário autorizado para acessar
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // api/users/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetUserByIdQuery(id);

            //var user = _userService.GetById(id);    // usado do UserService
            var user = await _mediator.Send(query);   // usado no MediatR

            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // api/users
        [HttpPost]
        [AllowAnonymous]      // permite acesso anônimo (sobrescreve a annotation Authorize)
        public async Task<IActionResult> Post([FromBody] CreateUserCommand createUserCommand)
        {

            //var id = _userService.Create(newUserInputModel);   // usado do UserService
            var id = await _mediator.Send(createUserCommand);    // usado no MediatR

            return CreatedAtAction(nameof(GetById), new { id = id }, createUserCommand);
        }

        // api/users/login
        [HttpPut("login")]
        [AllowAnonymous]      // permite acesso anônimo (sobrescreve a annotation Authorize)
        public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
        {
            var loginUserViewModel = await _mediator.Send(command);

            if (loginUserViewModel == null)
            {
                return BadRequest();
            }

            return Ok(loginUserViewModel);
        }
    }
}
