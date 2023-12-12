using Balta.Localizacao.Core.DomainObjects;
using Balta.Localizacao.Domain.Entities.Validations;

namespace Balta.Localizacao.Domain.Entities
{
    public class Municipio : Entity
    {
        public string Codigo { get; private set; }
        public string Nome { get; private set; }
        public string CodigoUf { get; private set; }
        public Estado Estado { get; private set; }

        protected Municipio()
        {
        }

        public Municipio(string codigo, string nome, string codigoUf)
        {
            Codigo = codigo;
            Nome = nome;
            CodigoUf = codigoUf;
        }

        public void AssociarEstado(Estado estado)
        {
            CodigoUf = estado.CodigoUf;
            Estado = estado;
        }

        public override bool EhValido()
        {
            ValidationResult = new MunicipioValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
