using Balta.Localizacao.ApplicationLayer.Commands.LocalizacaoCommands.Validations;
using Balta.Localizacao.Core.Messages;

namespace Balta.Localizacao.ApplicationLayer.Commands.LocalizacaoCommands
{
    public class RemoverMunicipioCommand : Command
    {
        public Guid Id { get; set; }

        public RemoverMunicipioCommand()
        {
            
        }

        public override bool EhValido()
        {
            ValidationResult = new RemoverMunicipioCommandValidation().Validate(this);
            return base.EhValido();
        }
    }
}
