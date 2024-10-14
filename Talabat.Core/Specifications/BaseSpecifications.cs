using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;

namespace Talabat.Core.Specifications
{
    public class BaseSpecifications<T> : ISpecifications<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Critria { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> OrderBy { get; set; }
        public Expression<Func<T, object>> OrderByDecs { get; set; }
        public int Take { get; set; }
        public int Skip { get; set; }
        public bool IsPaginationEnabled { get; set; } = false;

        public BaseSpecifications()
        {

        }
        public BaseSpecifications(Expression<Func<T, bool>> CritriaExpression)
        {
            Critria = CritriaExpression;
        }

        public void AddOrderBy(Expression<Func<T, object>> orderBy)
        {
            OrderBy = orderBy;
        }
        public void AddOrderByDecs(Expression<Func<T, object>> orderbydecs)
        {
            OrderByDecs = orderbydecs;
        }

        public void ApplyPagination(int skip , int take)
        {
           IsPaginationEnabled = true;
            Skip = skip;
            Take = take;
        }
    }
}
