using Balta.Localizacao.ApplicationLayer.Commands.LocalizacaoCommands.Validations;
using Balta.Localizacao.Core.Messages;

namespace Balta.Localizacao.ApplicationLayer.Commands.LocalizacaoCommands
{
    public class EditarMunicipioCommand : Command
    {
        public Guid Id { get; set; }
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string CodigoUf { get; set; }

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
