using FluentValidation;

namespace Balta.Localizacao.ApplicationLayer.Commands.LocalizacaoCommands.Validations
{
    public class RemoverMunicipioCommandValidation : AbstractValidator<RemoverMunicipioCommand>
    {
        public static string IdRequiredErrorMessage = "O campo Id é obrigatorio.";
        public RemoverMunicipioCommandValidation()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty)
                .WithMessage(IdRequiredErrorMessage);
        }
    }
}
