using Balta.Localizacao.Core.DomainObjects;
using Balta.Localizacao.Core.Spacification;
using Microsoft.IdentityModel.Tokens;

namespace Balta.Localizacao.Domain.Entities.Spacifications
{
    public class NovoEstadoCodigoUfDiferenteDoAtualSpacification : ISpacification<string>
    {
        private readonly Estado _estado;

        public NovoEstadoCodigoUfDiferenteDoAtualSpacification(Estado estado)
        {
            _estado = estado;
        }

        public bool IsSatisfiedBy(string codigoUf)
        {
            return _estado.CodigoUf != codigoUf;
        }
    }
    
    public class NovoEstadoCodigoUfNuloOuVazioSpacification : ISpacification<string>
    {
        public bool IsSatisfiedBy(string codigoUf)
        {
            return codigoUf.IsNullOrEmpty();
        }
    }

    public class NovoEstadoSiglaUfDiferenteDaAtualSpacification : ISpacification<string>
    {
        private readonly Estado _estado;

        public NovoEstadoSiglaUfDiferenteDaAtualSpacification(Estado estado)
        {
            _estado = estado;
        }

        public bool IsSatisfiedBy(string siglaUf)
        {
            return _estado.SiglaUf != siglaUf;
        }
    }
    
    public class NovoEstadoSiglaUfNuloOuVazioSpacification : ISpacification<string>
    {
        public bool IsSatisfiedBy(string siglaUf)
        {
            return siglaUf.IsNullOrEmpty();
        }
    }

    public class NovoEstadoNomeUfDiferenteDaAtualSpacification : ISpacification<string>
    {
        private readonly Estado _estado;

        public NovoEstadoNomeUfDiferenteDaAtualSpacification(Estado estado)
        {
            _estado = estado;
        }

        public bool IsSatisfiedBy(string nomeUf)
        {
            return _estado.NomeUf != nomeUf;
        }
    }
    
    public class NovoEstadoNomeUfNuloOuVazioSpacification : ISpacification<string>
    {
        public bool IsSatisfiedBy(string nomeUf)
        {
            return nomeUf.IsNullOrEmpty();
        }
    }

    public class ExisteMunipiosRepetidosSpacification : ISpacification<Municipio>
    {
        private readonly IReadOnlyCollection<Municipio> _municipios;
        private readonly string _codigoUf;

        public ExisteMunipiosRepetidosSpacification(IReadOnlyCollection<Municipio> municipios, string codigoUf)
        {
            _municipios = municipios;
            _codigoUf = codigoUf;
        }

        public bool IsSatisfiedBy(Municipio novoMunicipio)
        {
            return _municipios.Where(m => m.CodigoUf == _codigoUf
                                        && m.Codigo == novoMunicipio.Codigo
                                        && m.Nome == novoMunicipio.Nome).Any();
        }
    }
}
