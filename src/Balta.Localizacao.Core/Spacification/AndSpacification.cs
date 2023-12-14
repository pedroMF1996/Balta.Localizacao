namespace Balta.Localizacao.Core.Spacification
{
    public class AndSpacification<T> : ISpacification<T>
    {
        private readonly ISpacification<T> _left;
        private readonly ISpacification<T> _right;

        public AndSpacification(ISpacification<T> left, ISpacification<T> right)
        {
            _left = left;
            _right = right;
        }

        public bool IsSatisfiedBy(T entity)
        {
            return _left.IsSatisfiedBy(entity) && _right.IsSatisfiedBy(entity);
        }
    }
}