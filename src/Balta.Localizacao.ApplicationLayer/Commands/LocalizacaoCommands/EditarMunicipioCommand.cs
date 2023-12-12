using Balta.Localizacao.ApplicationLayer.Commands.LocalizacaoCommands.Validations;
using Balta.Localizacao.Core.Messages;

namespace Balta.Localizacao.ApplicationLayer.Commands.LocalizacaoCommands
{
    public class EditarMunicipioCommand : Command
    {
        public Guid Id { get; set; }
        public string Codigo { get; private set; }
        public string Nome { get; private set; }
        public string CodigoUf { get; private set; }

        public EditarMunicipioCommand()
        {

        }

        public override bool EhValido()
        {
            ValidationResult = new EditarMunicipioCommandValidation().Validate(this);
            return base.EhValido();
        }
    }
}
