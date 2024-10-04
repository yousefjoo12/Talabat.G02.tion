using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;

namespace Talabat.Core.Specifications.ProductSpecifications
{
    public class ProductWithBrandAndCategotySpecifications:BaseSpecifications<Product>
    {
        public ProductWithBrandAndCategotySpecifications():base()
        {
            Includes.Add(p=>p.Brand);
            Includes.Add(p=>p.Category);
        }
        public ProductWithBrandAndCategotySpecifications(int id):base(P=>P.Id==id)
        {
            Includes.Add(p => p.Brand);
            Includes.Add(p => p.Category);
        }
    }
}
