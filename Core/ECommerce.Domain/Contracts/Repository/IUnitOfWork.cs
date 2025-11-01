using ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Contracts.Repository
{
    public interface IUnitOfWork
    {
        IGenericRepository<TEntity> GetRepository<TEntity> () where TEntity : BaseEntity;
        Task<int> SaveChangesAsync();
    }
}
