using Balta.Localizacao.Core.Messages;
using FluentValidation.Results;

namespace Balta.Localizacao.Core.Mediatr
{
    public interface IMediatorHandler
    {
        Task<ValidationResult> EnviarComando<T>(T comando) where T : Command;
    }
}
