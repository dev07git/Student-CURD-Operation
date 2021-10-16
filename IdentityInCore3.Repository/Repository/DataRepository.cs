using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System.Data;
using IdentityInCore3.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace IdentityInCore3.Repository
{
    public class DataRepository<TEntity> : IDataRepository<TEntity>
          where TEntity : DomainEntity
    {
        public Core3DBContext Context { get; set; }
        readonly IConfiguration configuration;

        public DataRepository(Core3DBContext context, IConfiguration _configuration)
        {
            Context = context;
            this.configuration = _configuration;

        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            var result = await Task.Run(() => Context.Set<TEntity>().AsNoTracking());
            return result.ToList();
        }

        public async Task<TEntity> FindById(long id)
        {
            return await Task.Run(() =>
            {
                return Context.Set<TEntity>().AsNoTracking().SingleOrDefault(e => e.Id.Equals(id));
            });
           
        }


        public async Task<TEntity> Add(TEntity entity)
        {

            if (entity == null)
                throw new ArgumentException("entity is null");

            return await Task.Run(() =>
            {
                try
                {
                    Context.Set<TEntity>().Add(entity);
                    Context.SaveChanges();
                }
                catch (Exception e)
                {
                    throw;
                }
                return entity;
            });

        }
        public async Task<IEnumerable<TEntity>> BulkAdd(IEnumerable<TEntity> entity)
        {

            if (entity == null)
                throw new ArgumentException("entity is null");

            return await Task.Run(() =>
            {

                Context.Set<TEntity>().AddRange(entity);
                Context.SaveChanges();

                return entity;
            });

        }

        public async Task<IEnumerable<TEntity>> BulkUpdate(IEnumerable<TEntity> entity)
        {
            if (entity == null)
                throw new ArgumentException("entity is null");

            return await Task.Run(() =>
            {

                Context.Set<TEntity>().UpdateRange(entity);
                Context.SaveChanges();

                return entity;
            });
        }
 

        public async Task<TEntity> Update(TEntity entity)
        {
            return await Task.Run(() =>
            {

                if (entity == null)
                    throw new ArgumentException("entity is null");
                else
                {
                    var actualEntity = Context.Set<TEntity>().SingleOrDefault(e => e.Id.Equals(entity.Id));
                    Context.Entry(actualEntity).CurrentValues.SetValues(entity);
                    Context.SaveChanges();
                }

                return entity;
            });
        }

        public async Task<int> Remove(long id)
        {
            return await Task.Run(() =>
            {
                var entity = Context.Set<TEntity>().SingleOrDefault(e => e.Id.Equals(id));
                if (entity != null) Context.Set<TEntity>().Remove(entity);
                return Context.SaveChanges();
            });

        }

       
        public async Task<int> Remove(IEnumerable<TEntity> entity)
        {
            int result = 1;
            try
            {
                return await Task.Run(() =>
                {
                    if (entity != null)
                        Context.Set<TEntity>().RemoveRange(entity);
                    return Context.SaveChanges();
                });
            }
            catch (Exception)
            {
                result = 0;
            }
            return result;
        }

       
        public async Task<int> ExecuteNonQueryAsync(string query)
        {
            string conString = configuration["DBConnectionString"];
            int result = 0;
            using (SqlConnection con = new SqlConnection(conString))
            {




                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandTimeout = 900;
                    await con.OpenAsync();

                    SqlTransaction transaction;
                    // Start a local transaction.
                    transaction = con.BeginTransaction("SampleTransaction");
                    cmd.Transaction = transaction;

                    try
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter())
                        {
                            adapter.UpdateCommand = cmd;
                            adapter.UpdateBatchSize = 0;
                            adapter.UpdateCommand.CommandText = query;
                            result = adapter.UpdateCommand.ExecuteNonQuery();
                            // Attempt to commit the transaction.
                            transaction.CommitAsync();
                        }
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        result = -1;
                    }
                }
            }
            return result;
        }

      
    }
}
