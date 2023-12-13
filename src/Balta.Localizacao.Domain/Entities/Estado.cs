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

        public void AlterarEstado(Estado novoEstado)
        {
            if (VerificarAlterarCodigoUf(novoEstado))
                AlterarCodigoUf(novoEstado.CodigoUf);

            if (VerificarAlterarSiglaUf(novoEstado))
                AlterarSiglaUf(novoEstado.SiglaUf);

            if (VerificarAlterarNomeUf(novoEstado))
                AlterarNomeUf(novoEstado.NomeUf);
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
        private bool VerificarAlterarCodigoUf(Estado novoEstado)
            => new NovoEstadoCodigoUfNuloOuVazioSpacification().Not()
                    .And(new NovoEstadoCodigoUfDiferenteDoAtualSpacification(this))
                        .IsSatisfiedBy(novoEstado);

        private bool VerificarAlterarSiglaUf(Estado novoEstado)
            => new NovoEstadoSiglaUfNuloOuVazioSpacification().Not()
                    .And(new NovoEstadoSiglaUfDiferenteDaAtualSpacification(this))
                        .IsSatisfiedBy(novoEstado);

        private bool VerificarAlterarNomeUf(Estado estado)
            => new NovoEstadoNomeUfNuloOuVazioSpacification().Not()
                    .And(new NovoEstadoNomeUfDiferenteDaAtualSpacification(this))
                        .IsSatisfiedBy(estado);

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
