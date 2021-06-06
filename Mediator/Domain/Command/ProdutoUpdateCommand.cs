using MediatR;

namespace Mediator.Domain.Command
{
    public class ProdutoUpdateCommand : IRequest<string>
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public decimal Preco { get; private set; }
    }
}
