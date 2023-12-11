using Balta.Localizacao.ApplicationLayer.Commands.AutenticacaoCommands.Validations;
using Balta.Localizacao.Core.Messages;

namespace Balta.Localizacao.ApplicationLayer.Commands.AutenticacaoCommands
{
    public class NovoUsuarioCommand : Command
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public NovoUsuarioCommand()
        {}

        public override bool EhValido()
        {
            ValidationResult = new NovoUsuarioCommandValidation().Validate(this);
            return base.EhValido();
        }
    }
}
