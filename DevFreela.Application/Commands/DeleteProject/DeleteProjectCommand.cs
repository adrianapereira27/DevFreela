using MediatR;

namespace DevFreela.Application.Commands.DeleteProject
{
    // usado no CQRS
    public class DeleteProjectCommand : IRequest<Unit>
    {
        public DeleteProjectCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
