using Balta.Localizacao.Core.DomainObjects;
using Balta.Localizacao.Core.Spacification;
using Balta.Localizacao.Domain.Entities.Spacifications;
using Balta.Localizacao.Domain.Entities.Validations;

namespace Balta.Localizacao.Domain.Entities
{
    public class Estado : Entity, IAggregateRoot
    {
        public string CodigoUf { get; private set; }
        public string SiglaUf { get; private set; }
        public string NomeUf { get; private set; }
        public List<Municipio>? Municipios { get; private set; } = new();

        protected Estado()
        { }
        
        public Estado(string codigoUf, string siglaUf, string nomeUf)
        {
            CodigoUf = codigoUf;
            SiglaUf = siglaUf;
            NomeUf = nomeUf;
        }

        public void AlterarEstado(string codigoUf, string siglaUf, string nomeUf)
        {
            if (VerificarAlterarCodigoUf(codigoUf))
                AlterarCodigoUf(codigoUf);

            if (VerificarAlterarSiglaUf(siglaUf))
                AlterarSiglaUf(siglaUf);

            if (VerificarAlterarNomeUf(nomeUf))
                AlterarNomeUf(nomeUf);
        }

        public void AdicionarMunicipio(Municipio municipio)
        {
            if (VerificarMunicipiosRepetidos(municipio))
            {
                municipio.AssociarEstado(this);
                Municipios.Add(municipio);
            }
        }

        public override bool EhValido()
        {
            ValidationResult = new EstadoValidation().Validate(this);

            if (Municipios.Count > 0)
                ValidationResult.Errors.AddRange(Municipios
                    .SelectMany(i => new MunicipioValidation().Validate(i).Errors).ToList());

            return base.EhValido();
        }

        #region Metodos_Privados
        private bool VerificarAlterarCodigoUf(string codigoUf)
            => new NovoEstadoCodigoUfNuloOuVazioSpacification().Not()
                    .And(new NovoEstadoCodigoUfDiferenteDoAtualSpacification(this))
                        .IsSatisfiedBy(codigoUf);

        private bool VerificarAlterarSiglaUf(string siglaUf)
            => new NovoEstadoSiglaUfNuloOuVazioSpacification().Not()
                    .And(new NovoEstadoSiglaUfDiferenteDaAtualSpacification(this))
                        .IsSatisfiedBy(siglaUf);

        private bool VerificarAlterarNomeUf(string nomeUf)
            => new NovoEstadoNomeUfNuloOuVazioSpacification().Not()
                    .And(new NovoEstadoNomeUfDiferenteDaAtualSpacification(this))
                        .IsSatisfiedBy(nomeUf);

        private void AlterarCodigoUf(string codigoUf)
        {
            CodigoUf = codigoUf;
            Municipios.ForEach(municipio => municipio.AssociarEstado(this));
        }

        private void AlterarSiglaUf(string siglaUf)
        {
            SiglaUf = siglaUf;
        }

        private void AlterarNomeUf(string nomeUf)
        {
            NomeUf = nomeUf;
        }

        private bool VerificarMunicipiosRepetidos(Municipio municipio)
            => new ExisteMunipiosRepetidosSpacification(Municipios, CodigoUf)
                .IsSatisfiedBy(municipio);
        
        #endregion

        public static class EstadoFactory
        {
            public static Estado CriarEstadoVazio() => new Estado();
        }
    }
}
