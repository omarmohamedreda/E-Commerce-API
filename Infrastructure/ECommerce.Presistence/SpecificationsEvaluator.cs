using ECommerce.Domain.Contracts.Specifications;
using ECommerce.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Presistence
{
    public class SpecificationsEvaluator
    {
        public static IQueryable<TEntity> GreateQuery<TEntity>(IQueryable<TEntity> BaseQuery, ISpecifications<TEntity> specifications) where TEntity : BaseEntity
        {
            var Query = BaseQuery;

            // Apply Criteria
            if (specifications.Criteria != null)
            {
                Query = Query.Where(specifications.Criteria);
            }

            // Apply OrderBy
            if (specifications.OrderBy != null)
            {
                Query = Query.OrderBy(specifications.OrderBy);
            }


            // Apply OrderByDescending
            else if (specifications.OrderByDescending != null)
            {
                Query = Query.OrderByDescending(specifications.OrderByDescending);
            }

            // Apply Pagination
            if (specifications.IsPaginated)
            {
                Query = Query.Skip(specifications.Skip).Take(specifications.Take);
            }


            // Apply Includes
            if (specifications.Includes != null && specifications.Includes.Any())
            {
                Query = specifications.Includes.Aggregate(Query,(CurrentQuery, Expression) => CurrentQuery.Include(Expression));
            }

            return Query;

        }
    }
}
