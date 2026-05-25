using DomainLayer.Contracts;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications
{
    abstract public class BaseSpecifications<TEntity, Tkey> : ISpecifications<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        protected BaseSpecifications(Expression<Func<TEntity, bool>> CriteriaExpression)
        {
            Criteria = CriteriaExpression;
        }
        public Expression<Func<TEntity, bool>> Criteria { get; private set; }



        #region Include
        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = new List<Expression<Func<TEntity, object>>>();

        protected void AddInclude(Expression<Func<TEntity, object>> IncludeExpression)
        {
            IncludeExpressions.Add(IncludeExpression);
        }

        #endregion



        #region Sorting
        public Expression<Func<TEntity, object>> OrderBy { get; private set; }
        public Expression<Func<TEntity, object>> OrderByDesc { get; private set; }
        public void AddOrderBy(Expression<Func<TEntity, object>> OrderByExp) => OrderBy = OrderByExp;
        public void AddOrderByDesc(Expression<Func<TEntity, object>> OrderByExpDesc) => OrderByDesc = OrderByExpDesc;

        #endregion

        public int Skip { get; set; }

        public int Take { get; set; }

        public bool IsPaginated { get ; set; }

        protected void ApplyPagination(int pageSize, int PageIndex)
        {
            IsPaginated = true;
            Take = pageSize;
            Skip = (PageIndex-1)*pageSize;
        }

    }
}
