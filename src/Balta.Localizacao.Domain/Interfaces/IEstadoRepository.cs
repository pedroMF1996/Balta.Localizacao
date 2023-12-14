using Balta.Localizacao.Core.Data;
using Balta.Localizacao.Domain.Entities;

namespace Balta.Localizacao.Domain.Interfaces
{
    public interface IEstadoRepository : IRepository<Estado>
    {
        Municipio ObterMunicipioPorId(Guid id);
        void ExcluirMunicipio(Municipio municipio);
        void EditarMunicipios(Municipio municipio);
        Task AdicionarMunicipios(Municipio municipio);

        Estado ObterEstadoPorId(Guid id);
        Estado ObterEstadoPorCodigoUf(string codigoUf);
        void ExcluirEstado(Estado estado);
        void EditarEstado(Estado estado);
        Task AdicionarEstado(Estado estado);

    }
}
