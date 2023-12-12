using FluentValidation;

namespace Balta.Localizacao.ApplicationLayer.Commands.AutenticacaoCommands.Validations
{
    public class NovoUsuarioCommandValidation : AbstractValidator<NovoUsuarioCommand>
    {
        public static int MAX_LENGHT_PASSWORD = 100;
        public static int MIN_LENGHT_PASSWORD = 6;

        public static string NomeRequiredErrorMessage = "O campo Nome é obrigatório";
        public static string EmailRequiredErrorMessage = "O campo Email é obrigatório";
        public static string EmailAddressErrorMessage = "O campo Email está em formato inválido";
        public static string PasswordRequiredErrorMessage = "O campo Password é obrigatório";
        public static string PasswordLenghtErrorMessage = $"O campo Password precisa ter entre {MIN_LENGHT_PASSWORD} e {MAX_LENGHT_PASSWORD} caracteres";
        public static string ConfirmPasswordErrorMessage = "As senhas não conferem.";


        public NovoUsuarioCommandValidation()
        {
            RuleFor(c => c.Nome)
                .NotEmpty()
                .WithMessage(NomeRequiredErrorMessage);

            RuleFor(c => c.Email)
               .NotEmpty()
               .WithMessage(EmailRequiredErrorMessage)
               .EmailAddress()
               .WithMessage(EmailAddressErrorMessage);

            RuleFor(c => c.Password)
                .NotEmpty()
                .WithMessage(PasswordRequiredErrorMessage)
                .MaximumLength(MAX_LENGHT_PASSWORD)
                .MinimumLength(MIN_LENGHT_PASSWORD)
                .WithMessage(PasswordLenghtErrorMessage);

            RuleFor(c => c.ConfirmPassword)
                .Equal(c => c.Password)
                .WithMessage(ConfirmPasswordErrorMessage);
        }
    }
}
