using FluentValidation;

namespace Balta.Localizacao.ApplicationLayer.Commands.LocalizacaoCommands.Validations
{
    public class RemoverEstadoCommandValidation : AbstractValidator<RemoverEstadoCommand>
    {
        public static string IdRequiredErrorMessage = "O campo Id é obrigatorio.";
        public RemoverEstadoCommandValidation()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty)
                .WithMessage(IdRequiredErrorMessage);
        }
    }
}
