using ECommerce.Domain.Contracts.Specifications;
using ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.BaseSpecifications
{
    public class BaseSpecifications<TEntity> : ISpecifications<TEntity> where TEntity : BaseEntity
    {

        #region Where
        public Expression<Func<TEntity, bool>> Criteria { get; }

        public List<Expression<Func<TEntity, object>>> Includes { get; } = new();



        protected BaseSpecifications(Expression<Func<TEntity, bool>> _Criteria)
        {
            Criteria = _Criteria;
        }
        #endregion

        #region Include
        protected void AddInclude(Expression<Func<TEntity, object>> IncludeExpression)
        {
            Includes.Add(IncludeExpression);
        } 
        #endregion

        #region Ordering
        public Expression<Func<TEntity, object>> OrderBy { get; private set; }

        public Expression<Func<TEntity, object>> OrderByDescending { get; private set; }

        protected void AddOrderBy(Expression<Func<TEntity, object>> _OrderBy)
        {
            OrderBy = _OrderBy;
        }

        protected void AddOrderByDescending(Expression<Func<TEntity, object>> _OrderByDesc)
        {
            OrderByDescending = _OrderByDesc;
        } 
        #endregion

    }
}
