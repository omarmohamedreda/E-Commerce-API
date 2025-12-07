using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Contracts.Repository
{
    public interface ICacheRepository
    {
        // Gets a value from the cache by key.

        Task<string?> GetAsync(string Cachekey);


        // Sets a value in the cache.
        Task SetAsync(string Cachekey, string CacheVvalue, TimeSpan? TimeToLive);


    }
}
