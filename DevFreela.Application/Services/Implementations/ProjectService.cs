using Dapper;
using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DevFreela.Application.Services.Implementations
{
    public class ProjectService : IProjectService
    {
        private readonly DevFreelaDbContext _dbContext;
        private readonly string _connectionString;   
        public ProjectService(DevFreelaDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _connectionString = configuration.GetConnectionString("DevFreelaCs"); // usado no Dapper
        }
        // comentado, porque será usado no padrão CQRS (MediatR)
        /*public int Create(NewProjectInputModel inputModel)
        {
            var project = new Project(inputModel.Title, inputModel.Description, inputModel.idCliente, inputModel.idFreelancer, inputModel.TotalCoast);
            
            _dbContext.Projects.Add(project);
            _dbContext.SaveChanges();

            return project.Id;
        }*/

        // comentado, porque será usado no padrão CQRS (MediatR)
        /*public void CreateComment(CreateCommentInputModel inputModel)
        {
            var comment = new ProjectComment(inputModel.Content, inputModel.IdProject, inputModel.IdUser);

            _dbContext.ProjectComments.Add(comment);

            _dbContext.SaveChanges();
        }*/

        /*public void Delete(int id)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);

            if (project != null)
            {
                project.Cancel();
                _dbContext.SaveChanges();
            }
        }*/

        /*public void Finish(int id)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);

            if (project != null)
            {
                project.Finish();
                _dbContext.SaveChanges();
            }
        }*/

        // comentado, porque será usado no padrão CQRS (MediatR)
        /*public List<ProjectViewModel> GetAll(string query)
        {
            var projects = _dbContext.Projects;

            var projectsViewModel = projects.Select(p => new ProjectViewModel(p.Id, p.Title, p.CreatedAt))
                .ToList();

            return projectsViewModel;
        }*/

        //comentado, porque será usado no padrão CQRS(MediatR)
        /*public ProjectDetailsViewModel GetById(int id)
        {            
            var project = _dbContext.Projects
                .Include(p => p.Client)     // inclui o User Client da classe Project.cs (retorna a informação na consulta)
                .Include(p => p.Freelancer) // inclui o User Freelancer da classe Project.cs (retorna a informação na consulta)
                .SingleOrDefault(p => p.Id == id);

            if (project == null) return null;

            var projectDetailsViewModel = new ProjectDetailsViewModel(
                project.Id,
                project.Title,
                project.Description,
                project.TotalCost,
                project.StartedAt,
                project.FinishedAt,
                project.Client.FullName,
                project.Freelancer.FullName
                );
            return projectDetailsViewModel;
        }*/

        /*public void Start(int id)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);

            if (project != null)
            {
                project.Start();
                // _dbContext.SaveChanges();  // usado no EntityFrameworkCore
            }

            using (var sqlConnection = new SqlConnection(_connectionString))  // dapper
            {
                sqlConnection.Open();

                var script = "UPDATE Projects SET Status = @status, StartedAt = @startedAt WHERE Id = @id";

                sqlConnection.Execute(script, new { status = project.Status, startedAt = project.StartedAt, id });
            
            }
        }*/

        /*public void Update(UpdateProjectInputModel updateModel)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == updateModel.Id);

            if (project != null)
            {
                project.Update(updateModel.Title, updateModel.Description, updateModel.TotalCoast);
                _dbContext.SaveChanges();
            }
        }*/
    }
}
