using Mediator.Domain.Command;
using Mediator.Domain.Entity;
using Mediator.Notifications;
using Mediator.Repository;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Mediator.Domain.Handler
{
    public class ProdutoUpdateCommandHandler : IRequestHandler<ProdutoUpdateCommand, string>
    {
        private readonly IMediator _mediator;
        private readonly IRepository<Produto> _repository;

        public ProdutoUpdateCommandHandler(IMediator mediator, IRepository<Produto> repository)
        {
            _mediator = mediator;
            _repository = repository;
        }

        public async Task<string> Handle(ProdutoUpdateCommand request, CancellationToken cancellationToken)
        {
            var produto = new Produto { Id = request.Id, Nome = request.Nome, Preco = request.Preco };

            try
            {
                await _repository.Edit(produto);
                await _mediator.Publish(new ProdutoUpdateNotification { Id = produto.Id, Nome = produto.Nome, Preco = produto.Preco });

                return await Task.FromResult("Produto alterado com sucesso");
            }
            catch (Exception ex)
            {
                await _mediator.Publish(new ProdutoUpdateNotification { Id = produto.Id, Nome = produto.Nome, Preco = produto.Preco });
                await _mediator.Publish(new ErroNotification { Erro = ex.Message, PilhaErro = ex.StackTrace });

                return await Task.FromResult("Ocorreu um erro nomomento da alteração");
            }
        }
    }
}
