﻿using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.CreateProject
{
    // usado no CQRS
    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, int>
    {
        private readonly IProjectRepository _projectRepository;
        public CreateProjectCommandHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }
        public async Task<int> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {            
            var project = new Project(request.Title, request.Description, request.IdCliente, request.IdFreelancer, request.TotalCoast);

            await _projectRepository.AddAsync(project);

            return project.Id;
        }
    }
}
