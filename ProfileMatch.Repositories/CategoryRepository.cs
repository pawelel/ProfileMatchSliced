using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using ProfileMatch.Contracts;
using ProfileMatch.Data;
using ProfileMatch.Models.Models;
using ProfileMatch.Models.Responses;

namespace ProfileMatch.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IDbContextFactory<ApplicationDbContext> contextFactory;

        public CategoryRepository(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public async Task<List<Category>> GetCategoriesWithQuestions()
        {
            using ApplicationDbContext repositoryContext = contextFactory.CreateDbContext();
            return await repositoryContext.Categories.Include(q => q.Questions).AsNoTracking().ToListAsync();
        }
        public async Task<List<Category>> GetCategories()
        {
            using ApplicationDbContext repositoryContext = contextFactory.CreateDbContext();
            return await repositoryContext.Categories.AsNoTracking().ToListAsync();
        }

        public Task<Category> Create(Category category)
        {
            throw new System.NotImplementedException();
        }

        public Task<Category> Delete(Category category)
        {
            throw new System.NotImplementedException();
        }

        public Task<Category> FindByName(string categoryName)
        {
            throw new System.NotImplementedException();
        }

        public Task<Category> FindById(int categoryId)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Category>> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Task<Category> Update(Category category)
        {
            throw new System.NotImplementedException();
        }
    }
}
