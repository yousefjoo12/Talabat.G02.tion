using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Specifications.ProductSpecifications
{
    public class ProductsSpecParams
    {
        public string? sort { get; set; }
        public int? brandId { get; set; }
        public int? categoryId { get; set; }

        private const int MAxPageSize = 10;
        private int pageSize;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value > MAxPageSize ? MAxPageSize : value; }
        }
        public int PageIndex { get; set; } = 1;

    }
}
