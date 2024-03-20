using FluentValidation.AspNetCore;
using FluentValidation;
using MediatR;
using System.Reflection;
using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Commands.CreateUser;
using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Application.ViewModels;
using DevFreela.Application.Queries.GetProjectById;
using DevFreela.Application.Commands.UpdateProject;
using DevFreela.Application.Commands.DeleteProject;
using DevFreela.Application.Commands.StartProject;
using DevFreela.Application.Commands.FinishProject;
using DevFreela.Application.Queries.GetUser;
using DevFreela.Application.Queries.GetAllSkills;
using DevFreela.Core.DTOs;
using DevFreela.Application.Commands.CreateComment;
using DevFreela.Application.Commands.LoginUser;

namespace DevFreela.API
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediator()
                .AddValidator();

            return services;
        }

        private static IServiceCollection AddMediator(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });
            services.AddScoped<IRequestHandler<CreateProjectCommand, int>, CreateProjectCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateProjectCommand, Unit>, UpdateProjectCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteProjectCommand, Unit>, DeleteProjectCommandHandler>();
            services.AddScoped<IRequestHandler<StartProjectCommand, Unit>, StartProjectCommandHandler>();
            services.AddScoped<IRequestHandler<FinishProjectCommand, bool>, FinishProjectCommandHandler>();
            services.AddScoped<IRequestHandler<GetAllProjectsQuery, List<ProjectViewModel>>, GetAllProjectsQueryHandler>();
            services.AddScoped<IRequestHandler<GetProjectByIdQuery, ProjectDetailsViewModel>, GetProjectByIdQueryHandler>();
            services.AddScoped<IRequestHandler<CreateCommentCommand, Unit>, CreateCommentCommandHandler>();
            services.AddScoped<IRequestHandler<CreateUserCommand, int>, CreateUserCommandHandler>();
            services.AddScoped<IRequestHandler<GetUserByIdQuery, UserDetailsViewModel>, GetUserByIdQueryHandler>();
            services.AddScoped<IRequestHandler<GetAllSkillsQuery, List<SkillDTO>>, GetAllSkillsQueryHandler>();
            services.AddScoped<IRequestHandler<LoginUserCommand, LoginUserViewModel>, LoginUserCommandHandler>();
            return services;
        }
        private static IServiceCollection AddValidator(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), ServiceLifetime.Transient);
            services.AddFluentValidationAutoValidation();

            return services;
        }
    }
}
