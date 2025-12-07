using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Abstraction
{
    public interface ICacheServices
    {
        Task<string?> GetAsync(string Chachekey);
        Task SetAsync(string key, object cacheValue, TimeSpan TimeToLive);

    }
}
