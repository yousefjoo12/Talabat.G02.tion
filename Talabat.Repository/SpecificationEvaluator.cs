using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;
using Talabat.Core.Specifications;

namespace Talabat.Repository
{
    internal static class SpecificationEvaluator<TEntity>where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> InnerQuery,ISpecifications<TEntity> spec )
        {
            var query = InnerQuery; //= _dbcontext.Set<Product>()
            if (spec.Critria is not null)
            {
                query = query.Where(spec.Critria);// = _dbcontext.Set<Product>().Where(p=>p.Id == id)
            }
            query = spec.Includes.Aggregate(query, (CurrentQuery, IncludeExpr) => CurrentQuery.Include(IncludeExpr));
            // = _dbcontext.Set<Product>().Where(p=>p.Id == id).Include(p => p.Brand).Include(p => p.Category)
            return query;
        }
    }
}
