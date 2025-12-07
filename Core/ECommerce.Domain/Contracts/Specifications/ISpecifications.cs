using ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Contracts.Specifications
{
    public interface ISpecifications<TEntity> where TEntity : BaseEntity
    {
        Expression<Func<TEntity, bool>> Criteria { get; } // Where

        List<Expression<Func<TEntity, object>>> Includes { get; } // Includes

        Expression<Func<TEntity, object>> OrderBy { get; } // OrderBy

        Expression<Func<TEntity, object>> OrderByDescending { get; } // OrderByDescending 

        int Take { get; } // PageSize
        int Skip { get; } // PageIndex
        bool IsPaginated { get; } // IsPagingEnabled
    }
}
