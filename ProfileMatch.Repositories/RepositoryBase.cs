using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using ProfileMatch.Contracts;
using ProfileMatch.Data;
using ProfileMatch.Models.Responses;

namespace ProfileMatch.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected ApplicationDbContext RepositoryContext { get; set; }
        public RepositoryBase(ApplicationDbContext repositoryContext)
        {
            this.RepositoryContext = repositoryContext;
        }


        public async Task<T> Create(T entity)
        {
            var data = await RepositoryContext.AddAsync(entity);
            await RepositoryContext.SaveChangesAsync();
            return data.Entity;
        }
        public async Task<T> Update(T entity, int key)
        {

            T existing = await RepositoryContext.Set<T>().FindAsync(key);

            RepositoryContext.Entry(existing).CurrentValues.SetValues(entity);

            await RepositoryContext.SaveChangesAsync();

            return existing;
        }
        public async Task<T> Update(T entity, string key)
        {

            T existing = await RepositoryContext.Set<T>().FindAsync(key);

            RepositoryContext.Entry(existing).CurrentValues.SetValues(entity);

            await RepositoryContext.SaveChangesAsync();
            return existing;
        }
        public T Delete(T entity)
        {
        
            var data = this.RepositoryContext.Set<T>().Remove(entity).Entity;
            
            return data;
        }

        public async Task<List<T>> FindAllAsync()
        {
       
            return await this.RepositoryContext.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<List<T>> FindByConditionAsync(Expression<Func<T, bool>> expression)
        {
      
           return await this.RepositoryContext.Set<T>().Where(expression).AsNoTracking().ToListAsync();
        
        }
        public T FindSingleByCondition(Expression<Func<T, bool>> expression)
        {
           
            return this.RepositoryContext.Set<T>().Where(expression).AsNoTracking().FirstOrDefault();
        }
        public async Task<T> FindSingleByConditionAsync(Expression<Func<T, bool>> expression)
        {
            return await this.RepositoryContext.Set<T>().Where(expression).AsNoTracking().FirstOrDefaultAsync();
        }
        public async Task<bool> Exist(Expression<Func<T, bool>> expression)
        {
            var _ = await this.RepositoryContext.Set<T>().Where(expression).AsNoTracking().FirstOrDefaultAsync();
            if (_ != null)
            {
                return true;
            }
            return false;
        }

    }
}