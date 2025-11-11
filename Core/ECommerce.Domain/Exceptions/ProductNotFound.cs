using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Exceptions
{
    public sealed class ProductNotFound(int Id) : NotFoundException($"Product with {Id} is Not Found")
    {
        
    }
}
