using Balta.Localizacao.ApplicationLayer.Commands.AutenticacaoCommands.Validations;
using Balta.Localizacao.Core.Messages;

namespace Balta.Localizacao.ApplicationLayer.Commands.AutenticacaoCommands
{
    public class LoginCommand : Command
    {
        public string Email { get; private set; }

        public string Password { get; private set; }

        public LoginCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public override bool EhValido()
        {
            ValidationResult = new LoginCommandValidation().Validate(this);
            return base.EhValido();
        }
    }
}
