using Balta.Localizacao.Core.Messages;
using FluentValidation.Results;
using MediatR;

namespace Balta.Localizacao.Core.Mediatr
{
    public class MediatorHandler : IMediatorHandler
    {
        public readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ValidationResult> EnviarComando<T>(T comando) where T : Command //teste de subida
        {
            return await _mediator.Send(comando);
        }
    }
}
