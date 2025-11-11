using ECommerce.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Shared
{
    public class ProductQueryParameters
    {
        private const int DefaultSize = 5;
        private const int MaxSize = 10;


        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public ProductSortingOptions? SortingOption { get; set; }
        public string? SearchValue { get; set; }

        public int PageIndex { get; set; } = 1;

        private int _pageSize = DefaultSize;
        public int PageSize { get { return _pageSize; } set { _pageSize = value > MaxSize ? MaxSize : value; } }
    }
}
