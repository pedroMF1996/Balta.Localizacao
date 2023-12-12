using Balta.Localizacao.ApplicationLayer.Commands.LocalizacaoCommands.Validations;
using Balta.Localizacao.Core.Messages;

namespace Balta.Localizacao.ApplicationLayer.Commands.LocalizacaoCommands
{
    public class RemoverEstadoCommand : Command
    {
        public Guid Id { get; set; }

        public RemoverEstadoCommand()
        {
            
        }

        public override bool EhValido()
        {
            ValidationResult = new RemoverEstadoCommandValidation().Validate(this);
            return base.EhValido();
        }
    }
}
