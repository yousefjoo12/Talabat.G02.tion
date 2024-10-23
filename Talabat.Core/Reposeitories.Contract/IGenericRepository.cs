using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;
using Talabat.Core.Specifications;

namespace Talabat.Core.Reposeitories.Contract
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T?> GetAsync(int id);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> spec);

        Task<T?> GetWithspecAsync(ISpecifications<T> spec);

        Task<int> GetCountAsync(ISpecifications<T> spec);

        Task AddAsync(T entity);
        void UpdateAsync(T entity);
        void DeleteAsync(T entity);

    }
}
