using Balta.Localizacao.Core.DomainObjects;

namespace Balta.Localizacao.Core.Data
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        IUnitOfWork unitOfWork { get; }
    }
}
