using ECommerce.Domain.Contracts.Repository;
using ECommerce.Domain.Models;
using ECommerce.Presistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Presistence.Repository
{
    public class UnitOfWork(StoreDbContext _context) : IUnitOfWork
    {
        private readonly Dictionary<string, object> _repositories = [];
        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity
        {

            // Get Type Name
            var TypeName = typeof(TEntity).Name; // ==> Ex : Product
            if (_repositories.TryGetValue(TypeName, out object? value))
                return (IGenericRepository<TEntity>)value;
            else
            {
                // Create repository
                var repository = new GenericRepository<TEntity>(_context);
                // Add to dictionary
                _repositories["TypeName"] = repository;
                // Return repository
                return repository;
            }




        }

        public async Task<int> SaveChangesAsync() 

            => await _context.SaveChangesAsync();
       
    }
}
