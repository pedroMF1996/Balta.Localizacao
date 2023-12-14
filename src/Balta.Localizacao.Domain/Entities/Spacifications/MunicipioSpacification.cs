using Balta.Localizacao.Core.DomainObjects;
using Balta.Localizacao.Core.Spacification;
using Microsoft.IdentityModel.Tokens;

namespace Balta.Localizacao.Domain.Entities.Spacifications
{
    public class NovoMunicipioCodigoUfDiferenteDoAtualSpacification : ISpacification<string>
    {
        private readonly Municipio _municipio;

        public NovoMunicipioCodigoUfDiferenteDoAtualSpacification(Municipio municipio)
        {
            _municipio = municipio;
        }

        public bool IsSatisfiedBy(string codigoUf)
        {
            return _municipio.CodigoUf != codigoUf;
        }
    }
    
    public class NovoMunicipioCodigoUfNuloOuVazioSpacification : ISpacification<string>
    {
        public bool IsSatisfiedBy(string codigoUf)
        {
            return codigoUf.IsNullOrEmpty();
        }
    }

    public class NovoMunicipioCodigoDiferenteDoAtualSpacification : ISpacification<string>
    {
        private readonly Municipio _municipio;

        public NovoMunicipioCodigoDiferenteDoAtualSpacification(Municipio municipio)
        {
            _municipio = municipio;
        }

        public bool IsSatisfiedBy(string codigo)
        {
            return _municipio.Codigo != codigo;
        }
    }

    public class NovoMunicipioCodigoNuloOuVazioSpacification : ISpacification<string>
    {
        public bool IsSatisfiedBy(string codigo)
        {
            return codigo.IsNullOrEmpty();
        }
    }

    public class MunicipioCodigoCompativelComCodigoUfSpacification : ISpacification<string>
    {
        private readonly string _codigoUf;

        public MunicipioCodigoCompativelComCodigoUfSpacification(string codigoUf)
        {
            _codigoUf = codigoUf;
        }

        public bool IsSatisfiedBy(string codigo)
        {
            var specficationResult = codigo.StartsWith(_codigoUf);
            return specficationResult ? specficationResult : throw new DomainException("Codigo municipio e codigo estado nao sao compativeis.");
        }
    }

    public class NovoMunicipioNomeDiferenteDoAtualSpacification : ISpacification<string>
    {
        private readonly Municipio _municipio;

        public NovoMunicipioNomeDiferenteDoAtualSpacification(Municipio municipio)
        {
            _municipio = municipio;
        }

        public bool IsSatisfiedBy(string nome)
        {
            return _municipio.Nome != nome;
        }
    }

    public class NovoMunicipioNomeNuloOuVazioSpacification : ISpacification<string>
    {
        public bool IsSatisfiedBy(string nome)
        {
            return nome.IsNullOrEmpty();
        }
    }
}
