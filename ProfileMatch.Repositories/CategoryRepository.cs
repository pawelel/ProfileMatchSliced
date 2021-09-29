using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using ProfileMatch.Contracts;
using ProfileMatch.Data;
using ProfileMatch.Models.Models;
using ProfileMatch.Models.Responses;

namespace ProfileMatch.Repositories
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<List<Category>> GetCategoriesWithQuestions()
        {
           
           return await this.RepositoryContext.Set<Category>().Include(q => q.Questions).AsNoTracking().ToListAsync();
        
        }
    }
}
