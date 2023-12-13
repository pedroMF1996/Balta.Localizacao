using Balta.Localizacao.Core.DomainObjects;
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
        {

        }
        
        public Estado(string codigoUf, string siglaUf, string nomeUf)
        {
            CodigoUf = codigoUf;
            SiglaUf = siglaUf;
            NomeUf = nomeUf;
        }

        public void AlterarEstado(Estado estado)
        {
            if (VerificarAlterarCodigoUf(estado))
                AlterarCodigoUf(estado.CodigoUf);

            if (VerificarAlterarSiglaUf(estado))
                AlterarSiglaUf(estado.SiglaUf);

            if (VerificarAlterarNomeUf(estado))
                AlterarNomeUf(estado.NomeUf);
        }

        private bool VerificarAlterarCodigoUf(Estado estado)
            => new EstadoEditarCodigoUfSpacification(this).IsSatisfiedBy(estado);

        private bool VerificarAlterarSiglaUf(Estado estado)
            => new EstadoEditarSiglaUfSpacification(this).IsSatisfiedBy(estado);

        private bool VerificarAlterarNomeUf(Estado estado)
            => new EstadoEditarNomeUfSpacification(this).IsSatisfiedBy(estado);

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

        public void AdicionarMunicipio(Municipio municipio)
        {
            municipio.AssociarEstado(this);
            Municipios.Add(municipio);
        }

        public override bool EhValido()
        {
            ValidationResult = new EstadoValidation().Validate(this);

            if (Municipios.Count > 0)
                ValidationResult.Errors.AddRange(Municipios
                    .SelectMany(i => new MunicipioValidation().Validate(i).Errors).ToList());

            return base.EhValido();
        }
    }
}
