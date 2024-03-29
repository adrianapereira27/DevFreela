﻿using DevFreela.Application.Commands.CreateComment;
using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Commands.DeleteProject;
using DevFreela.Application.Commands.FinishProject;
using DevFreela.Application.Commands.StartProject;
using DevFreela.Application.Commands.UpdateProject;
using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Application.Queries.GetProjectById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Route("api/projects")]
    [Authorize]     // annotation que indica que os métodos precisam de um usuário autorizado para acessar
    public class ProjectsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // api/projects?query=net core
        [HttpGet]
        [Authorize(Roles = "client,freelancer")]  // annotation que indica que os métodos precisam de um usuário autorizado para acessar
        public async Task<IActionResult> Get(string query)
        {
            // var projects = _projectService.GetAll(query);    // usado do ProjectService

            var getAllProjectsQuery = new GetAllProjectsQuery(query);

            var projects = await _mediator.Send(getAllProjectsQuery);

            return Ok(projects);
        }

        // api/projects/3
        [HttpGet("{id}")]
        [Authorize(Roles = "client,freelancer")]  // annotation que indica que os métodos precisam de um usuário autorizado para acessar
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetProjectByIdQuery(id);

            //var project = _projectService.GetById(id);  // usado do ProjectService

            var project = await _mediator.Send(query);

            if (project == null)
            {
                return NotFound();
            }
            return Ok(project);
        }

        [HttpPost]
        [Authorize(Roles = "client")]   // annotation que indica que os métodos precisam de um usuário autorizado para acessar
        public async Task<IActionResult> Post([FromBody] CreateProjectCommand command)
        {
            
            //var id = _projectService.Create(createProject);  // usado do ProjectService
            var id = await _mediator.Send(command);   // usado no MediatR

            return CreatedAtAction(nameof(GetById), new { id = id }, command);
        }

        // api/projects/2
        [HttpPut("{id}")]
        [Authorize(Roles = "client")]   // annotation que indica que os métodos precisam de um usuário autorizado para acessar
        public async Task<IActionResult> Put(int id, [FromBody] UpdateProjectCommand command)
        {
            if (command.Description.Length > 200)
            {
                return BadRequest();
            }
            // _projectService.Update(updateProject);   // usado do ProjectService
            await _mediator.Send(command);        // usado no MediatR

            return NoContent();
        }

        // api/projects/3   DELETE
        [HttpDelete("{id}")]
        [Authorize(Roles = "client")]   // annotation que indica que os métodos precisam de um usuário autorizado para acessar
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteProjectCommand(id);

            // _projectService.Delete(id);    // usado do ProjectService
            await _mediator.Send(command);    // usado no MediatR

            return NoContent();
        }

        // api/projects/1/comments  POST
        [HttpPost("{id}/comments")]
        [Authorize(Roles = "client,freelancer")]   // annotation que indica que os métodos precisam de um usuário autorizado para acessar
        public async Task<IActionResult> PostComment(int id, [FromBody] CreateCommentCommand command)
        {
            // _projectService.CreateComment(inputModel);  // usado do ProjectService

            await _mediator.Send(command);   // usado no MediatR

            return NoContent();
        }

        // api/projects/1/start
        [HttpPut("{id}/start")]
        [Authorize(Roles = "client")]  // annotation que indica que os métodos precisam de um usuário autorizado para acessar
        public async Task<IActionResult> Start(int id)
        {
            var command = new StartProjectCommand(id);

            // _projectService.Start(id);     // usado do ProjectService
            await _mediator.Send(command);    // usado no MediatR

            return NoContent();
        }

        // api/projects/1/finish
        [HttpPut("{id}/finish")]
        [Authorize(Roles = "client")]   // annotation que indica que os métodos precisam de um usuário autorizado para acessar
        public async Task<IActionResult> Finish(int id, [FromBody] FinishProjectCommand command)
        {
            command.Id = id;

            // _projectService.Finish(id);    // usado do ProjectService
            var result = await _mediator.Send(command);    // usado no MediatR

            if (!result)
            {
                return BadRequest("O pagamento não pôde ser processado.");
            }

            return Accepted();
        }
    }
}
