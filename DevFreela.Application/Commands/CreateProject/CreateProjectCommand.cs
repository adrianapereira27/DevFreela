using MediatR;

namespace DevFreela.Application.Commands.CreateProject
{
    // usado no CQRS
    public class CreateProjectCommand : IRequest<int>
    {
        // mesmos campos do InputModel (NewProjectInputModel)
        public string Title { get; set; }
        public string Description { get; set; }
        public int idCliente { get; set; }
        public int idFreelancer { get; set; }
        public decimal TotalCoast { get; set; }
    }
}
