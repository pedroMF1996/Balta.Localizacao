using Balta.Localizacao.ApplicationLayer.Commands.LocalizacaoCommands.Validations;
using Balta.Localizacao.Core.Messages;
using FluentValidation;

namespace Balta.Localizacao.ApplicationLayer.Commands.LocalizacaoCommands
{
    public class AdicionarMunicipioCommand : Command
    {
        public string Codigo { get; private set; }
        public string Nome { get; private set; }
        public string CodigoUf { get; private set; }

        public AdicionarMunicipioCommand()
        {

        }

        public override bool EhValido()
        {
            ValidationResult = new AdicionarMunicipioCommandValidation().Validate(this);
            return base.EhValido();
        }
    }
}
