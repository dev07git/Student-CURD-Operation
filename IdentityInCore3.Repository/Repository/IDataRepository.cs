using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using IdentityInCore3.DAL.Models;

namespace IdentityInCore3.Repository
{
    public interface IDataRepository<TEntity> : IRepository<TEntity>
         where TEntity : DomainEntity
    {
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity> FindById(long id);

        Task<TEntity> Add(TEntity entity);
        Task<IEnumerable<TEntity>> BulkAdd(IEnumerable<TEntity> entity);
        Task<int> Remove(long id);
        Task<TEntity> Update(TEntity entity);
        Task<int> Remove(IEnumerable<TEntity> entity);
        
        Task<IEnumerable<TEntity>> BulkUpdate(IEnumerable<TEntity> entity);
        Task<int> ExecuteNonQueryAsync(string query);
    }
}