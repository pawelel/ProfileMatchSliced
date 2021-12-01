using Microsoft.EntityFrameworkCore.Query;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProfileMatch.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<bool> Delete(TEntity entityToDelete);

        Task<bool> Delete(object id);
        Task<bool> ExistById(params object[] ids);
        Task<List<TEntity>> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);

        Task<List<TEntity>> GetAll();

        Task<TEntity> GetById(params object[] ids);
        Task<TEntity> GetOne(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);
        Task<TEntity> Insert(TEntity entity);

        Task<TEntity> Update(TEntity entityToUpdate);
    }
}