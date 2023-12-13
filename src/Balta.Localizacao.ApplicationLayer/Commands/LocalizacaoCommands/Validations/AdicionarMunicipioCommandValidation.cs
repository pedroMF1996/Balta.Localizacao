using FluentValidation;

namespace Balta.Localizacao.ApplicationLayer.Commands.LocalizacaoCommands.Validations
{
    public class AdicionarMunicipioCommandValidation : AbstractValidator<AdicionarMunicipioCommand>
    {
        public static string CodigoLengthErrorMessage = "O código do município deve conter 7 caracteres.";
        public static string CodigoRequiredErrorMessage = "O campo código do município é obrigatorio.";
        public static string NomeRequiredErrorMessage = "O campo nome é obrigatorio.";
        public static string NomeMaxLengthErrorMessage = "O campo nome deve conter até 150 caracteres.";
        public static string CodigoUfLengthErrorMessage = "O código UF deve conter 2 caracteres.";
        public static string CodigoUfRequiredErrorMessage = "O campo código UF é obrigatorio.";
        public AdicionarMunicipioCommandValidation()
        {
            RuleFor(x => x.Codigo)
                .NotEmpty()
                .WithMessage(CodigoRequiredErrorMessage)
                .Length(7)
                .WithMessage(CodigoLengthErrorMessage);
            RuleFor(x => x.Nome)
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
