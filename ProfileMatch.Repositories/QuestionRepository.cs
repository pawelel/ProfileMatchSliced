
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using ProfileMatch.Contracts;
using ProfileMatch.Data;
using ProfileMatch.Models.Models;
using ProfileMatch.Models.Responses;

namespace ProfileMatch.Repositories
{
    public class QuestionRepository : RepositoryBase<Question>, IQuestionRepository
    {
        public QuestionRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<List<Question>> GetQuestionsWithCategories()
        {
           return await RepositoryContext.Set<Question>().Include(c => c.Category).AsNoTracking().ToListAsync();
        

        }
    }
}
