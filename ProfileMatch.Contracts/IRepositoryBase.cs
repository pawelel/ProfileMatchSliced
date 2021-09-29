using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

using ProfileMatch.Models.Responses;

namespace ProfileMatch.Contracts
{
    public interface IRepositoryBase<T>
    {
        Task<T> Create(T entity);
        T Delete(T entity);
        Task<bool> Exist(Expression<Func<T, bool>> expression);
        Task<List<T>> FindAllAsync();
        Task<List<T>> FindByConditionAsync(Expression<Func<T, bool>> expression);
        T FindSingleByCondition(Expression<Func<T, bool>> expression);
        Task<T> FindSingleByConditionAsync(Expression<Func<T, bool>> expression);
        Task<T> Update(T entity, string key);
        Task<T> Update(T entity, int key);
    }
}
