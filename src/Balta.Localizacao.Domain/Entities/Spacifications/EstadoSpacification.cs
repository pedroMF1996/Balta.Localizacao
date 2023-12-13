using Balta.Localizacao.Core.Spacification;

namespace Balta.Localizacao.Domain.Entities.Spacifications
{
    public class EstadoEditarCodigoUfSpacification : ISpacification<Estado>
    {
        private readonly Estado _estado;

        public EstadoEditarCodigoUfSpacification(Estado estado)
        {
            _estado = estado;
        }

        public bool IsSatisfiedBy(Estado entity)
        {
            return _estado.CodigoUf != entity.CodigoUf;
        }
    }

    public class EstadoEditarSiglaUfSpacification : ISpacification<Estado>
    {
        private readonly Estado _estado;

        public EstadoEditarSiglaUfSpacification(Estado estado)
        {
            _estado = estado;
        }

        public bool IsSatisfiedBy(Estado entity)
        {
            return _estado.SiglaUf != entity.SiglaUf;
        }
    }

    public class EstadoEditarNomeUfSpacification : ISpacification<Estado>
    {
        private readonly Estado _estado;

        public EstadoEditarNomeUfSpacification(Estado estado)
        {
            _estado = estado;
        }

        public bool IsSatisfiedBy(Estado entity)
        {
            return _estado.NomeUf != entity.NomeUf;
        }
    }
}
