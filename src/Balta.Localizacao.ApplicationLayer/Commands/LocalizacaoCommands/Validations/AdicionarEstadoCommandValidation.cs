using FluentValidation;

namespace Balta.Localizacao.ApplicationLayer.Commands.LocalizacaoCommands.Validations
{
    public class AdicionarEstadoCommandValidation : AbstractValidator<AdicionarEstadoCommand>
    {
        public static string SiglaUfLengthErrorMessage = "O campo SiglaUf deve conter 2 caracteres.";
        public static string SiglaUfRequiredErrorMessage = "O campo SiglaUf é obrigatorio.";
        public static string NomeRequiredErrorMessage = "O campo nome UF é obrigatorio.";
        public static string NomeMaxLengthErrorMessage = "O campo nome UF deve conter até 150 caracteres.";
        public static string CodigoUfLengthErrorMessage = "O código UF deve conter 2 caracteres.";
        public static string CodigoUfRequiredErrorMessage = "O campo código UF é obrigatorio.";
        public AdicionarEstadoCommandValidation()
        {
            RuleFor(x => x.SiglaUf)
                .NotEmpty()
                .WithMessage(SiglaUfRequiredErrorMessage)
                .Length(2)
                .WithMessage(SiglaUfLengthErrorMessage);
            RuleFor(x => x.NomeUf)
                .NotEmpty()
                .WithMessage(NomeRequiredErrorMessage)
                .MaximumLength(150)
                .WithMessage(NomeMaxLengthErrorMessage);
            RuleFor(x => x.CodigoUf)
                .NotEmpty()
                .WithMessage(CodigoUfRequiredErrorMessage)
                .Length(2)
                .WithMessage(CodigoUfLengthErrorMessage);
        }
    }
}
