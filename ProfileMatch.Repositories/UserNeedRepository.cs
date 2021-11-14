using Microsoft.EntityFrameworkCore;

using ProfileMatch.Contracts;
using ProfileMatch.Data;
using ProfileMatch.Models.Models;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileMatch.Repositories
{
    public class UserNeedRepository : IUserCategoryRepository
    {
        private readonly IDbContextFactory<ApplicationDbContext> contextFactory;

        public UserNeedRepository(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public async Task<UserCategory> Create(UserCategory need)
        {
            using ApplicationDbContext repositoryContext = contextFactory.CreateDbContext();
            var data = await repositoryContext.UserNeedCategories.AddAsync(need);
            await repositoryContext.SaveChangesAsync();
            return data.Entity;
        }

        public async Task<UserCategory> Delete(UserCategory need)
        {
            using ApplicationDbContext repositoryContext = contextFactory.CreateDbContext();
            var data = repositoryContext.UserNeedCategories.Remove(need).Entity;
            await repositoryContext.SaveChangesAsync();
            return data;
        }

        public async Task<UserCategory> FindById(string userId, int categoryId)
        {
            using ApplicationDbContext repositoryContext = contextFactory.CreateDbContext();
            return await repositoryContext.UserNeedCategories.FindAsync(userId, categoryId);
        }

        public async Task<UserCategory> Update(UserCategory need)
        {
            using ApplicationDbContext repositoryContext = contextFactory.CreateDbContext();
            var existing = await repositoryContext.UserNeedCategories.FindAsync(need.ApplicationUserId, need.CategoryId);
            if (existing != null)
            {
                repositoryContext.Entry(existing).CurrentValues.SetValues(need);
                await repositoryContext.SaveChangesAsync();
                return existing;
            }
            else
            {
                return need;
            }
        }

        public async Task<List<UserCategory>> GetUserNeedCategoriesForUser(string userId)
        {
            using ApplicationDbContext repositoryContext = contextFactory.CreateDbContext();
            return await repositoryContext.UserNeedCategories.Where(u => u.ApplicationUserId == userId).ToListAsync();
        }

        public async Task<List<UserCategory>> GetUserNeedCategoriesForCategory(int categoryId)
        {
            using ApplicationDbContext repositoryContext = contextFactory.CreateDbContext();
            return await repositoryContext.UserNeedCategories.Where(u => u.CategoryId==categoryId).ToListAsync();
        }

        public async Task<List<UserCategory>> GetAll()
        {
            using ApplicationDbContext repositoryContext = contextFactory.CreateDbContext();
            return await repositoryContext.UserNeedCategories.ToListAsync();
        }
    }
}