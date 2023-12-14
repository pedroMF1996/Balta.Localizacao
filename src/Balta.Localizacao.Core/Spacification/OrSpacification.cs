namespace Balta.Localizacao.Core.Spacification
{
    public class OrSpacification<T> : ISpacification<T>
    {
        private readonly ISpacification<T> left;
        private readonly ISpacification<T> right;

        public OrSpacification(ISpacification<T> left, ISpacification<T> right)
        {
            this.left = left;
            this.right = right;
        }

        public bool IsSatisfiedBy(T entity)
        {
            return left.IsSatisfiedBy(entity) || right.IsSatisfiedBy(entity);
        }
    }
}