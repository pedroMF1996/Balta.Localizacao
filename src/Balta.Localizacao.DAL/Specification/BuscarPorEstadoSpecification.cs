using Balta.Localizacao.DAL.SpecificationBase;
using Balta.Localizacao.Domain.Entities;


namespace Balta.Localizacao.DAL.Specification
{
    public class BuscarPorEstadoSpecification: BaseSpecification<Municipio>
    {
        public BuscarPorEstadoSpecification(Guid estadoid) : base(x=>x.Estado.Id.Equals(estadoid))
        {
            AddInclude(x=>x.Estado);
        }
    }
}
