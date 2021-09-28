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

        public async Task<ServiceResponse<List<Category>>> GetCategoriesWithQuestions()
        {
            ServiceResponse<List<Category>> response = new();
            response.Data = await this.RepositoryContext.Set<Category>().Include(q => q.Questions).AsNoTracking().ToListAsync();
            if (response.Data == null)
            {
                response.Message = "Query returned no data.";
                response.Success = false;
            }
            else
            {
                response.Message = "Data loaded with success.";
                response.Success = true;
            }
            return response;
        }
    }
}
