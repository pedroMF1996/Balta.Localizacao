using FluentValidation;

namespace Balta.Localizacao.ApplicationLayer.Commands.AutenticacaoCommands.Validations
{
    public class LoginCommandValidation : AbstractValidator<LoginCommand>
    {
        public static int MAX_LENGHT_PASSWORD = 100;
        public static int MIN_LENGHT_PASSWORD = 6;

        public static string EmailRequiredErrorMessage = "O campo Email é obrigatório";
        public static string EmailAddressErrorMessage = "O campo Email está em formato inválido";
        public static string PasswordRequiredErrorMessage = "O campo Password é obrigatório";
        public static string PasswordLenghtErrorMessage = $"O campo Password precisa ter entre {MIN_LENGHT_PASSWORD} e {MAX_LENGHT_PASSWORD} caracteres";

        public LoginCommandValidation()
        {
            RuleFor(c => c.Email)
                .NotEmpty()
                .NotNull()
                .WithMessage(EmailRequiredErrorMessage)
                .EmailAddress()
                .WithMessage(EmailAddressErrorMessage);

            RuleFor(c => c.Password)
                .NotEmpty()
                .NotNull()
                .WithMessage(PasswordRequiredErrorMessage)
                .MaximumLength(MAX_LENGHT_PASSWORD)
                .MinimumLength(MIN_LENGHT_PASSWORD)
                .WithMessage(PasswordLenghtErrorMessage);
        }
    }
}
