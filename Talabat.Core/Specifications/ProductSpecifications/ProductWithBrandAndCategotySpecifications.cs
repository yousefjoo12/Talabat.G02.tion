using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;

namespace Talabat.Core.Specifications.ProductSpecifications
{
    public class ProductWithBrandAndCategotySpecifications : BaseSpecifications<Product>
    {
        public ProductWithBrandAndCategotySpecifications(ProductsSpecParams spec) : base(P =>

        //BrandId   = null = true
        //CategoryId = null = true
        //Search="Mocha"
         
        (string.IsNullOrEmpty(spec.Search) || P.Name.ToLower().Contains(spec.Search.ToLower())) &&
        (!spec.BrandId.HasValue || P.BrandId == spec.BrandId.Value) &&
        (!spec.CategoryId.HasValue || P.CategoryId == spec.CategoryId.Value)
           )
        {


            Includes.Add(p => p.Brand);
            Includes.Add(p => p.Category);

            if (!string.IsNullOrEmpty(spec.sort))
            {
                switch (spec.sort)
                {
                    case "priceAsc":
                        // orderby(P=>p.price)
                        AddOrderBy(P => P.Price);
                        break;
                    case "priceDcse":
                        AddOrderByDecs(P => P.Price);
                        // orderbyDecs(P=>p.price)
                        break;
                    default:
                        AddOrderBy(P => P.Name);
                        break;
                }
            }
            else
            {
                AddOrderBy(P => P.Id);
            }

            //totalitems =18 ~ 20
            //PageSize = 20 / 4 = 5
            //PageIndex= (3-1) * 4 = 8
            //skip = 8  and take = 4

            ApplyPagination((spec.PageIndex - 1) * spec.PageSize, spec.PageSize);

        }
        public ProductWithBrandAndCategotySpecifications(int id) : base(P => P.Id == id)
        {
            Includes.Add(p => p.Brand);
            Includes.Add(p => p.Category);
        }
    }
}
