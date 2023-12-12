using Balta.Localizacao.ApplicationLayer.Commands.LocalizacaoCommands.Validations;
using Balta.Localizacao.Core.Messages;

namespace Balta.Localizacao.ApplicationLayer.Commands.LocalizacaoCommands
{
    public class EditarEstadoCommand : Command
    {
        public Guid Id { get; set; }
        public string CodigoUf { get; set; }
        public string SiglaUf { get; set; }
        public string NomeUf { get; set; }

        public EditarEstadoCommand()
        {

        }

        public override bool EhValido()
        {
            ValidationResult = new EditarEstadoCommandValidation().Validate(this);
            return base.EhValido();
        }
    }
}
