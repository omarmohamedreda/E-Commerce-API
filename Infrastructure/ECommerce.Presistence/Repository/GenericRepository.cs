using ECommerce.Domain.Contracts.Repository;
using ECommerce.Domain.Contracts.Specifications;
using ECommerce.Domain.Models;
using ECommerce.Presistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Presistence.Repository
{
    public class GenericRepository<TEntity>(StoreDbContext _context) : IGenericRepository<TEntity> where TEntity : BaseEntity
    {

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }

        public void Delete(TEntity entity)
        {
             _context.Set<TEntity>().Remove(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllWihSpecificationsAsync(ISpecifications<TEntity> specifications)
        {
            return await SpecificationsEvaluator.GreateQuery(_context.Set<TEntity>(), specifications).ToListAsync();
            
        }

        public Task<TEntity?> GetByIdWihSpecificationsAsync(ISpecifications<TEntity> specifications)
        {
            throw new NotImplementedException();
        }
    }
}
