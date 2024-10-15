using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;

namespace Talabat.Core.Specifications.ProductSpecifications
{
    public class ProductWithFilterationCountSpec : BaseSpecifications<Product>
    {
        public ProductWithFilterationCountSpec(ProductsSpecParams spec) : base(P =>
        
            (string.IsNullOrEmpty(spec.Search) || P.Name.ToLower().Contains(spec.Search.ToLower())) && 
            (!spec.BrandId.HasValue || P.BrandId == spec.BrandId.Value) &&
            (!spec.CategoryId.HasValue || P.CategoryId == spec.CategoryId.Value)
        )
        {

        }
    }
}
