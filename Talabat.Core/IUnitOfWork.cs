using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;
using Talabat.Core.Order_Aggregrate;
using Talabat.Core.Reposeitories.Contract;

namespace Talabat.Core
{
    public interface IUnitOfWork :IAsyncDisposable
    {
        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
       
        Task<int> CompleteAsync();
    }
}
