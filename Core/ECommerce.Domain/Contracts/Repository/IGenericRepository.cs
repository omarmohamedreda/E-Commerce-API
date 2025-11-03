using ECommerce.Domain.Contracts.Specifications;
using ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Contracts.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(int id);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);

        Task<IEnumerable<TEntity>> GetAllWihSpecificationsAsync(ISpecifications<TEntity> specifications);

        Task<TEntity?> GetByIdWihSpecificationsAsync(ISpecifications<TEntity> specifications);

    }
}
