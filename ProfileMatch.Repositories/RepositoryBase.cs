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


        public async Task<ServiceResponse<T>> Create(T entity)
        {
            ServiceResponse<T> response = new();
           
            if (entity == null)
            {
                response.Success = false;
                response.Message = "There was an error with provided data.";
            }
            else
            {

                response.Success = true;
                response.Message = "Created";
                var data = await RepositoryContext.AddAsync(entity);
                await RepositoryContext.SaveChangesAsync();
                response.Data = data.Entity;
            }
            return response;
        }
        public async Task<ServiceResponse<T>> Update(T entity, int key)
        {
            ServiceResponse<T> response = new();
            T existing = await RepositoryContext.Set<T>().FindAsync(key);
           

            
            if (existing==null)
            {
                response.Success = false;
                response.Message = "There was an error with provided data.";
                response.Data = null;
            }
            else
            {
                RepositoryContext.Entry(existing).CurrentValues.SetValues(entity);
                response.Success = true;
                response.Message = "Updated";
                await RepositoryContext.SaveChangesAsync();
                response.Data = existing;
            }
            return response;
        }
        public async Task<ServiceResponse<T>> Update(T entity, string key)
        {
            ServiceResponse<T> response = new();
            T existing = await RepositoryContext.Set<T>().FindAsync(key);



            if (existing == null)
            {
                response.Success = false;
                response.Message = "There was an error with provided data.";
                response.Data = null;
            }
            else
            {
                RepositoryContext.Entry(existing).CurrentValues.SetValues(entity);
                response.Success = true;
                response.Message = "Updated";
                await RepositoryContext.SaveChangesAsync();
                response.Data = existing;
            }
            return response;
        }
        public ServiceResponse<T> Delete(T entity)
        {
            ServiceResponse<T> response = new();
            var data = this.RepositoryContext.Set<T>().Remove(entity).Entity;
            if (data == null)
            {
                response.Success = false;
                response.Message = "There was an error with provided data.";
            }
            else
            {

                response.Success = true;
                response.Message = "Deleted";
                response.Data = data;
            }
            return response;
        }

        public async Task<ServiceResponse<List<T>>> FindAllAsync()
        {
            ServiceResponse<List<T>> response = new();
            var data = await this.RepositoryContext.Set<T>().AsNoTracking().ToListAsync();
            if (data == null)
            {
                response.Message = "The query returned no data";
                response.Success = false;
                response.Data = new();
            }
            else
            {
                response.Message = "Data received with success.";
                response.Success = true;
                response.Data = data;
            }
            return response;
        }

        public async Task<ServiceResponse<List<T>>> FindByConditionAsync(Expression<Func<T, bool>> expression)
        {
            ServiceResponse<List<T>> response = new();
            var data = await this.RepositoryContext.Set<T>().Where(expression).AsNoTracking().ToListAsync();
            if (data == null)
            {
                response.Data = new();
                response.Success = false;
                response.Message = "The query returned no data";
            }
            else
            {
                response.Message = "Data received with success.";
                response.Success = true;
                response.Data = data;
            }
            return response;
        }
        public ServiceResponse<T> FindSingleByCondition(Expression<Func<T, bool>> expression)
        {
            ServiceResponse<T> response = new();
            var data =  this.RepositoryContext.Set<T>().Where(expression).AsNoTracking().FirstOrDefault();
            if (data == null)
            {
                response.Message = "The query returned no data";
                response.Success = false;
            }
            else
            {
                response.Success = true;
                response.Message = "Data received with success.";
                response.Data = data;
            }
            return response;
        }
        public async Task<ServiceResponse<T>> FindSingleByConditionAsync(Expression<Func<T, bool>> expression)
        {
            ServiceResponse<T> response = new();
            var data = await this.RepositoryContext.Set<T>().Where(expression).AsNoTracking().FirstOrDefaultAsync();
            if (data == null)
            {
                response.Message = "The query returned no data";
                response.Success = false;
            }
            else
            {
                response.Success = true;
                response.Message = "Data received with success.";
                response.Data = data;
            }
            return response;
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