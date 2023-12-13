using Balta.Localizacao.Core.DomainObjects;
using Balta.Localizacao.Domain.Entities.Spacifications;
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

        public Municipio(string codigo, string nome)
        {
            Codigo = codigo;
            Nome = nome;
            CodigoUf = string.Empty;
        }

        public void AlterarMunicipio(Municipio municipio)
        {
            if(VerificarAlterarCodigo(municipio)) 
                AlterarCodigo(municipio.Codigo);

            if(VerificarAlterarCodigoUf(municipio)) 
                AlterarCodigoUf(municipio.CodigoUf);

            if(VerificarAlterarNome(municipio)) 
                AlterarNome(municipio.Nome);
        }

        private bool VerificarAlterarCodigoUf(Municipio municipio)
            => new MunicipioEditarCodigoUfSpacification(this).IsSatisfiedBy(municipio);

        private bool VerificarAlterarCodigo(Municipio municipio)
            => new MunicipioEditarCodigoSpacification(this).IsSatisfiedBy(municipio);

        private bool VerificarAlterarNome(Municipio municipio)
            => new MunicipioEditarNomeSpacification(this).IsSatisfiedBy(municipio);

        private void AlterarCodigo(string codigo)
        {
            Codigo = codigo;
        }
        
        private void AlterarCodigoUf(string codigoUf)
        {
            Codigo = codigoUf;
        }

        private void AlterarNome(string nome)
        {
            Nome = nome;
        }

        private void AssociarEstado(Estado estado)
        {
            CodigoUf = estado.CodigoUf;
            Estado = estado;
        }

        public override bool EhValido()
        {
            ValidationResult = new MunicipioValidation().Validate(this);
            return base.EhValido();
        }
    }
}
