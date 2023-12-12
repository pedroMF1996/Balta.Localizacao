using Balta.Localizacao.ApplicationLayer.Commands.AutenticacaoCommands.Validations;
using Balta.Localizacao.Core.Messages;

namespace Balta.Localizacao.ApplicationLayer.Commands.AutenticacaoCommands
{
    public class LoginCommand : Command
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public LoginCommand()
        {

        }

        public override bool EhValido()
        {
            ValidationResult = new LoginCommandValidation().Validate(this);
            return base.EhValido();
        }
    }
}
