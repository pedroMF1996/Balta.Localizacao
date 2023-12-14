namespace Balta.Localizacao.Core.Spacification
{
    public interface ISpacification<T>
    {
        bool IsSatisfiedBy(T entity);
    }
}
