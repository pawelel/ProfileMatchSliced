using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using ProfileMatch.Models.Responses;

namespace ProfileMatch.Contracts
{
    public interface IRepositoryBase<T>
    {
        ServiceResponse<T> Create(T entity);
        ServiceResponse<T> Delete(T entity);
        Task<bool> Exist(Expression<Func<T, bool>> expression);
        Task<ServiceResponse<List<T>>> FindAllAsync();
        Task<ServiceResponse<List<T>>> FindByConditionAsync(Expression<Func<T, bool>> expression);
        Task<ServiceResponse<T>> FindSingleByConditionAsync(Expression<Func<T, bool>> expression);
        ServiceResponse<T> Update(T entity);
    }
}
