using Balta.Localizacao.Core.Spacification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balta.Localizacao.Domain.Entities.Spacifications
{
    public class MunicipioEditarCodigoUfSpacification : ISpacification<Municipio>
    {
        private readonly Municipio _municipio;

        public MunicipioEditarCodigoUfSpacification(Municipio municipio)
        {
            _municipio = municipio;
        }

        public bool IsSatisfiedBy(Municipio entity)
        {
            return _municipio.CodigoUf != entity.CodigoUf;
        }
    }

    public class MunicipioEditarCodigoSpacification : ISpacification<Municipio>
    {
        private readonly Municipio _municipio;

        public MunicipioEditarCodigoSpacification(Municipio municipio)
        {
            _municipio = municipio;
        }

        public bool IsSatisfiedBy(Municipio entity)
        {
            return _municipio.Codigo != entity.Codigo;
        }
    }

    public class MunicipioEditarNomeSpacification : ISpacification<Municipio>
    {
        private readonly Municipio _municipio;

        public MunicipioEditarNomeSpacification(Municipio municipio)
        {
            _municipio = municipio;
        }

        public bool IsSatisfiedBy(Municipio entity)
        {
            return _municipio.Nome != entity.Nome;
        }
    }
}
