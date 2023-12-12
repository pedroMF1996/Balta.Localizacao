using FluentValidation;

namespace Balta.Localizacao.Domain.Entities.Validations
{
    public class EstadoValidation : AbstractValidator<Estado>
    {
        public static string SiglaUfLengthErrorMessage = "O código do município deve conter 2 caracteres.";
        public static string SiglaUfRequiredErrorMessage = "O campo código do município é obrigatorio.";
        public static string NomeRequiredErrorMessage = "O campo nome UF é obrigatorio.";
        public static string NomeMaxLengthErrorMessage = "O campo nome UF deve conter até 150 caracteres.";
        public static string CodigoUfLengthErrorMessage = "O código UF deve conter 2 caracteres.";
        public static string CodigoUfRequiredErrorMessage = "O campo código UF é obrigatorio.";

        public EstadoValidation()
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
