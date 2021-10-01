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
        private readonly ApplicationDbContext repositoryContext;

        public CategoryRepository(ApplicationDbContext repositoryContext)
        {
            this.repositoryContext = repositoryContext;
        }

        public async Task<List<Category>> GetCategoriesWithQuestions()
        {
           
           return await repositoryContext.Set<Category>().Include(q => q.Questions).AsNoTracking().ToListAsync();
        
        }
    }
}
