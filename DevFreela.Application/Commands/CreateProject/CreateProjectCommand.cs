using MediatR;

namespace DevFreela.Application.Commands.CreateProject
{
    // usado no padrão CQRS
    public class CreateProjectCommand : IRequest<int>
    {
        // mesmos campos do InputModel (NewProjectInputModel)
        public string Title { get; set; }
        public string Description { get; set; }
        public int IdCliente { get; set; }
        public int IdFreelancer { get; set; }
        public decimal TotalCoast { get; set; }
    }
}
