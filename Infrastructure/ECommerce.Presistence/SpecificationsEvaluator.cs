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

            if (specifications.Includes != null && specifications.Includes.Any())
            {
                Query = specifications.Includes.Aggregate(Query,(CurrentQuery, Expression) => CurrentQuery.Include(Expression));
            }

            return Query;




        }
    }
}
