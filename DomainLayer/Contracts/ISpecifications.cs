using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Contracts
{
    public interface ISpecifications<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        // property for each dynamic query that we want to build

        // where clause
        public Expression<Func<TEntity, bool>> Criteria { get; }

        // include clause
        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; }
        public Expression<Func<TEntity, object>> OrderBy { get; }
        public Expression<Func<TEntity, object>> OrderByDesc { get; }
        public int Skip { get; }
        public int Take { get; }
        public bool IsPaginated { get; set; }

    }
}
