namespace Balta.Localizacao.Core.Spacification
{
    public static class SpecificationExtensions
    {

        public static ISpacification<T> And<T> (this ISpacification<T> left, ISpacification<T> right)
        {
            return new AndSpacification<T>(left, right);
        }

        public static ISpacification<T> Or<T> (this ISpacification<T> left, ISpacification<T> right)
        {
            return new OrSpacification<T>(left, right);
        }

        public static ISpacification<T> Not<T>(this ISpacification<T> spacification)
        {
            return new NotSpacification<T>(spacification);
        }

    }
}
