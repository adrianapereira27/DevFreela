using MediatR;

namespace DevFreela.Application.Commands.CreateComment
{
    //  usado no CQRS
    public class CreateCommentCommand : IRequest<Unit>  // Unit é igual void (sem retorno)
    {
        // mesmos campos do InputModel (CreateCommentInputModel)
        public string Content { get; set; }
        public int IdProject { get; set; }
        public int IdUser { get; set; }
    }
}
