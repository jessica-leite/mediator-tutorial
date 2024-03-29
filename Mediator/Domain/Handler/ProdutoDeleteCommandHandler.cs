﻿using Mediator.Domain.Command;
using Mediator.Domain.Entity;
using Mediator.Notifications;
using Mediator.Repository;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Mediator.Domain.Handler
{
    public class ProdutoDeleteCommandHandler : IRequestHandler<ProdutoDeleteCommand, string>
    {
        private readonly IMediator _mediator;
        private readonly IRepository<Produto> _repository;

        public ProdutoDeleteCommandHandler(IMediator mediator, IRepository<Produto> repository)
        {
            _mediator = mediator;
            _repository = repository;
        }

        public async Task<string> Handle(ProdutoDeleteCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _repository.Delete(request.Id);
                await _mediator.Publish(new ProdutoDeleteNotification { Id = request.Id, IsConcluido = true });

                return await Task.FromResult("Produto excluído com sucesso");
            }
            catch (Exception ex)
            {
                await _mediator.Publish(new ProdutoDeleteNotification { Id = request.Id, IsConcluido = false });
                await _mediator.Publish(new ErroNotification { Erro = ex.Message, PilhaErro = ex.StackTrace });

                return await Task.FromResult("Ocorreu um erro no momento da exlcusão");
            }
        }
    }
}
