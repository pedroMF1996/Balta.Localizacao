using Balta.Localizacao.Core.Spacification;
using Microsoft.IdentityModel.Tokens;

namespace Balta.Localizacao.Domain.Entities.Spacifications
{
    public class NovoEstadoCodigoUfDiferenteDoAtualSpacification : ISpacification<Estado>
    {
        private readonly Estado _estado;

        public NovoEstadoCodigoUfDiferenteDoAtualSpacification(Estado estado)
        {
            _estado = estado;
        }

        public bool IsSatisfiedBy(Estado novoEstado)
        {
            return _estado.CodigoUf != novoEstado.CodigoUf;
        }
    }
    
    public class NovoEstadoCodigoUfNuloOuVazioSpacification : ISpacification<Estado>
    {
        public bool IsSatisfiedBy(Estado novoEstado)
        {
            return novoEstado.CodigoUf.IsNullOrEmpty();
        }
    }

    public class NovoEstadoSiglaUfDiferenteDaAtualSpacification : ISpacification<Estado>
    {
        private readonly Estado _estado;

        public NovoEstadoSiglaUfDiferenteDaAtualSpacification(Estado estado)
        {
            _estado = estado;
        }

        public bool IsSatisfiedBy(Estado novoEstado)
        {
            return _estado.SiglaUf != novoEstado.SiglaUf;
        }
    }
    
    public class NovoEstadoSiglaUfNuloOuVazioSpacification : ISpacification<Estado>
    {
        public bool IsSatisfiedBy(Estado novoEstado)
        {
            return novoEstado.SiglaUf.IsNullOrEmpty();
        }
    }

    public class NovoEstadoNomeUfDiferenteDaAtualSpacification : ISpacification<Estado>
    {
        private readonly Estado _estado;

        public NovoEstadoNomeUfDiferenteDaAtualSpacification(Estado estado)
        {
            _estado = estado;
        }

        public bool IsSatisfiedBy(Estado novoEstado)
        {
            return _estado.NomeUf != novoEstado.NomeUf;
        }
    }
    
    public class NovoEstadoNomeUfNuloOuVazioSpacification : ISpacification<Estado>
    {
        public bool IsSatisfiedBy(Estado novoEstado)
        {
            return novoEstado.NomeUf.IsNullOrEmpty();
        }
    }
}
