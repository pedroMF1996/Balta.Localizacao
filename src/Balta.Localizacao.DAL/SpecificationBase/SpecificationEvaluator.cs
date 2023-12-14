using Balta.Localizacao.Core.DomainObjects;
using Balta.Localizacao.Domain.Interfaces.Specification;
using Microsoft.EntityFrameworkCore;
namespace Balta.Localizacao.DAL.SpecificationBase
{

    public class SpecificationEvaluator<TEntity> where TEntity : Entity
    {
        public static  IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> specification)
        {
            var query = inputQuery;

            query = Criterio(specification, query);

            query = specification.Includes.Aggregate(query,
                                    (current, include) => current.Include(include));

            query = specification.IncludeStrings.Aggregate(query,
                                    (current, include) => current.Include(include));

            query = OrderBy(specification, query);

            query = OrderByDesc(specification, query);

            query = Pagination(specification, query);

            return query;
        }

        private static IQueryable<TEntity> Pagination(ISpecification<TEntity> specification, IQueryable<TEntity> query)
        {
            if (specification.IsPagingEnabled)
            {
                query = query.Skip(specification.Skip)
                             .Take(specification.Take);
            }

            return query;
        }

        private static IQueryable<TEntity> OrderByDesc(ISpecification<TEntity> specification, IQueryable<TEntity> query)
        {
            if (specification.GroupBy != null)
            {
                query = query.GroupBy(specification.GroupBy).SelectMany(x => x);
            }

            return query;
        }

        private static IQueryable<TEntity> OrderBy(ISpecification<TEntity> specification, IQueryable<TEntity> query)
        {
            if (specification.OrderBy != null)
            {
                query = query.OrderBy(specification.OrderBy);
            }
            return query;
        }

        private static IQueryable<TEntity> Criterio(ISpecification<TEntity> specification, IQueryable<TEntity> query)
        {
            if (specification.Criteria != null)
            {
                query = query.Where(specification.Criteria);
            }

            return query;
        }
    }
}
