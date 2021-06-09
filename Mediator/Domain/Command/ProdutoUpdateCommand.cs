using MediatR;

namespace Mediator.Domain.Command
{
    public class ProdutoUpdateCommand : IRequest<string>
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
    }
}
