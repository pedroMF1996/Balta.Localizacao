namespace Balta.Localizacao.Core.Spacification
{
    internal class NotSpacification<T> : ISpacification<T>
    {
        private readonly ISpacification<T> _spacification;

        public NotSpacification(ISpacification<T> spacification)
        {
            _spacification = spacification;
        }

        public bool IsSatisfiedBy(T entity)
        {
            return !_spacification.IsSatisfiedBy(entity);
        }
    }
}