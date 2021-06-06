using MediatR;

namespace Mediator.Domain.Command
{
    public class ProdutoDeleteCommand : IRequest<string>
    {
        public int Id { get; set; }
    }
}
