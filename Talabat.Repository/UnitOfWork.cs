using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entites;
using Talabat.Core.Order_Aggregrate;
using Talabat.Core.Reposeitories.Contract;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _dbcontext;
        //private Dictionary<string, GenericRepository<BaseEntity>> _Repositories;
        private Hashtable _Repositories;
        public UnitOfWork(StoreContext dbcontext)
        {
            _dbcontext = dbcontext;
            _Repositories = new Hashtable();
        }

        public async Task<int> CompleteAsync()
        {
            return await _dbcontext.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _dbcontext.DisposeAsync();
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            var key = typeof(TEntity).Name;// اسم الجدول
            if (!_Repositories.ContainsKey(key))
            {
                var repository = new GenericRepository<TEntity>(_dbcontext);
                _Repositories.Add(key, repository);
            }
            return _Repositories[key] as IGenericRepository<TEntity>;
        }
    }
}
