using Balta.Localizacao.Core.DomainObjects;
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

        public void AlterarCodigoUf(string codigoUf)
        {
            CodigoUf = codigoUf;
            Municipios.ForEach(municipio => municipio.AssociarEstado(this));
        }

        public void AlterarSiglaUf(string siglaUf)
        {
            SiglaUf = siglaUf;
        }

        public void AlterarNomeUf(string nomeUf)
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
