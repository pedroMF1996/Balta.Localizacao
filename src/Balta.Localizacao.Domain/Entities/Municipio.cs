using Balta.Localizacao.Core.DomainObjects;
using Balta.Localizacao.Core.Spacification;
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

        public void AlterarMunicipio(Municipio novoMunicipio)
        {
            if(VerificarAlterarCodigoUf(novoMunicipio)) 
                AlterarCodigoUf(novoMunicipio.CodigoUf);

            if(VerificarAlterarCodigo(novoMunicipio)) 
                AlterarCodigo(novoMunicipio.Codigo);

            if(VerificarAlterarNome(novoMunicipio)) 
                AlterarNome(novoMunicipio.Nome);
        }

        public void AssociarEstado(Estado estado)
        {
            CodigoUf = estado.CodigoUf;
            Estado = estado;
        }

        public override bool EhValido()
        {
            ValidationResult = new MunicipioValidation().Validate(this);
            return base.EhValido();
        }

        #region Metodos_Privados

        private bool VerificarAlterarCodigoUf(Municipio novoMunicipio)
            => new NovoMunicipioCodigoUfNuloOuVazioSpacification().Not()
                    .And(new NovoMunicipioCodigoUfDiferenteDoAtualSpacification(this))
                    .IsSatisfiedBy(novoMunicipio);

        private bool VerificarAlterarCodigo(Municipio novoMunicipio)
            => new NovoMunicipioCodigoNuloOuVazioSpacification().Not()
                .And(new NovoMunicipioCodigoDiferenteDoAtualSpacification(this))
                .And(new MunicipioCodigoCompativelComCodigoUfSpacification(CodigoUf))
                    .IsSatisfiedBy(novoMunicipio);

        private bool VerificarAlterarNome(Municipio novoMunicipio)
            => new NovoMunicipioNomeNuloOuVazioSpacification().Not()
                .And(new NovoMunicipioNomeDiferenteDoAtualSpacification(this))
                    .IsSatisfiedBy(novoMunicipio);

        private void AlterarCodigo(string codigo)
        {
            Codigo = codigo;
        }

        private void AlterarCodigoUf(string codigoUf)
        {
            CodigoUf = codigoUf;
        }

        private void AlterarNome(string nome)
        {
            Nome = nome;
        } 

        public static class MunicipioFactory
        {
            public static Municipio CriarMunicipioVazio() => new Municipio();
        }

        #endregion
    }
}
