using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using ProfileMatch.Contracts;
using ProfileMatch.Data;
using ProfileMatch.Models.Models;

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

        public async Task<Category> Create(Category category)
        {
            using ApplicationDbContext repositoryContext = contextFactory.CreateDbContext();
            var data = await repositoryContext.Categories.AddAsync(category);
            await repositoryContext.SaveChangesAsync();
            return data.Entity;
        }

        public async Task<Category> Delete(Category category)
        {
            using ApplicationDbContext repositoryContext = contextFactory.CreateDbContext();
            var data = repositoryContext.Categories.Remove(category).Entity;
            await repositoryContext.SaveChangesAsync();
            return data;
        }

        public async Task<Category> FindByName(string categoryName)
        {
            using ApplicationDbContext repositoryContext = contextFactory.CreateDbContext();
            return await repositoryContext.Categories.SingleOrDefaultAsync(c => c.Name == categoryName);
        }

        public async Task<Category> FindById(int categoryId)
        {
            using ApplicationDbContext repositoryContext = contextFactory.CreateDbContext();
            return await repositoryContext.Categories.SingleOrDefaultAsync(c => c.Id == categoryId);
        }

        public async Task<List<Category>> GetAll()
        {
            using ApplicationDbContext repositoryContext = contextFactory.CreateDbContext();
            return await repositoryContext.Categories.ToListAsync();
        }

        public async Task<Category> Update(Category category)
        {
            using ApplicationDbContext repositoryContext = contextFactory.CreateDbContext();
            var existing = await repositoryContext.Categories.FindAsync(category.Id);
            if (existing != null)
            {
                repositoryContext.Entry(existing).CurrentValues.SetValues(category);
                await repositoryContext.SaveChangesAsync();
                return existing;
            }
            else
            {
                return category;
            }
        }
    }
}