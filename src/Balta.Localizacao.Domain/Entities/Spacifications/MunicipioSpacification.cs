using Balta.Localizacao.Core.DomainObjects;
using Balta.Localizacao.Core.Spacification;
using Microsoft.IdentityModel.Tokens;

namespace Balta.Localizacao.Domain.Entities.Spacifications
{
    public class NovoMunicipioCodigoUfDiferenteDoAtualSpacification : ISpacification<Municipio>
    {
        private readonly Municipio _municipio;

        public NovoMunicipioCodigoUfDiferenteDoAtualSpacification(Municipio municipio)
        {
            _municipio = municipio;
        }

        public bool IsSatisfiedBy(Municipio novoMunicipio)
        {
            return _municipio.CodigoUf != novoMunicipio.CodigoUf;
        }
    }
    
    public class NovoMunicipioCodigoUfNuloOuVazioSpacification : ISpacification<Municipio>
    {
        public bool IsSatisfiedBy(Municipio novoMunicipio)
        {
            return novoMunicipio.CodigoUf.IsNullOrEmpty();
        }
    }

    public class NovoMunicipioCodigoDiferenteDoAtualSpacification : ISpacification<Municipio>
    {
        private readonly Municipio _municipio;

        public NovoMunicipioCodigoDiferenteDoAtualSpacification(Municipio municipio)
        {
            _municipio = municipio;
        }

        public bool IsSatisfiedBy(Municipio novoMunicipio)
        {
            return _municipio.Codigo != novoMunicipio.Codigo;
        }
    }

    public class NovoMunicipioCodigoNuloOuVazioSpacification : ISpacification<Municipio>
    {
        public bool IsSatisfiedBy(Municipio novoMunicipio)
        {
            return novoMunicipio.Codigo.IsNullOrEmpty();
        }
    }

    public class MunicipioCodigoCompativelComCodigoUfSpacification : ISpacification<Municipio>
    {
        private readonly string _codigoUf;

        public MunicipioCodigoCompativelComCodigoUfSpacification(string codigoUf)
        {
            _codigoUf = codigoUf;
        }

        public bool IsSatisfiedBy(Municipio novoMunicipio)
        {
            var specficationResult = novoMunicipio.Codigo.StartsWith(_codigoUf);
            return specficationResult ? specficationResult : throw new DomainException("Codigo municipio e codigo estado nao sao compativeis.");
        }
    }

    public class NovoMunicipioNomeDiferenteDoAtualSpacification : ISpacification<Municipio>
    {
        private readonly Municipio _municipio;

        public NovoMunicipioNomeDiferenteDoAtualSpacification(Municipio municipio)
        {
            _municipio = municipio;
        }

        public bool IsSatisfiedBy(Municipio novoMunicipio)
        {
            return _municipio.Nome != novoMunicipio.Nome;
        }
    }

    public class NovoMunicipioNomeNuloOuVazioSpacification : ISpacification<Municipio>
    {
        public bool IsSatisfiedBy(Municipio entity)
        {
            return entity.Nome.IsNullOrEmpty();
        }
    }
}
