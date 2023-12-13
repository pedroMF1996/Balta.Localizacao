using Balta.Localizacao.ApplicationLayer.Commands.LocalizacaoCommands.Validations;
using Balta.Localizacao.Core.Messages;
using FluentValidation;

namespace Balta.Localizacao.ApplicationLayer.Commands.LocalizacaoCommands
{
    public class AdicionarMunicipioCommand : Command
    {
        public string Codigo { get;  set; }
        public string Nome { get; set; }
        public string CodigoUf { get; set; }

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
