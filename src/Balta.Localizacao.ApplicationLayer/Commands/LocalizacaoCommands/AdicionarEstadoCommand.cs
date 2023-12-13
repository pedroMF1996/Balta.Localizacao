using Balta.Localizacao.ApplicationLayer.Commands.LocalizacaoCommands.Validations;
using Balta.Localizacao.Core.Messages;

namespace Balta.Localizacao.ApplicationLayer.Commands.LocalizacaoCommands
{
    public class AdicionarEstadoCommand : Command
    {
        public string CodigoUf { get; set; }
        public string SiglaUf { get; set; }
        public string NomeUf { get; set; }

        public AdicionarEstadoCommand()
        {

        }

        public override bool EhValido()
        {
            ValidationResult = new AdicionarEstadoCommandValidation().Validate(this);
            return base.EhValido();
        }
    }
}
