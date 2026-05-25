using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> CreateQuery<TEntity, TKey>(
            IQueryable<TEntity> InputQuery,
            ISpecifications<TEntity, TKey> specifications)
            where TEntity : BaseEntity<TKey>
        {
            var Query = InputQuery;

            if (specifications.Criteria is not null)
            {
                Query = Query.Where(specifications.Criteria);
            }


            if(specifications.OrderBy is not null)
            {
                Query = Query.OrderBy(specifications.OrderBy);
            }
            if(specifications.OrderByDesc is not null)
            {
                Query = Query.OrderByDescending(specifications.OrderByDesc);
            }


            if (specifications.IncludeExpressions is not null &&
                specifications.IncludeExpressions.Count > 0)
            {
                Query = specifications.IncludeExpressions.Aggregate(
                    Query,
                    (CurrentQuery, IncludeExp) =>
                        CurrentQuery.Include(IncludeExp));
            }

            if (specifications.IsPaginated)
            {
                Query = Query.Skip(specifications.Skip).Take(specifications.Take);
            }
            return Query;
        }
    }
}
