using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Abstraction
{
    public interface IServiceManager
    {
        public IProductServices ProductServices { get; }
        public IBasketServices BasketServices { get; }

    }
}
 