
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

        public async Task<List<Question>>> GetQuestionsWithCategories()
        {
            var qlist = await RepositoryContext.Set<Question>().Include(c => c.Category).AsNoTracking().ToListAsync();
            if (qlist != null)
            {
                return new()
                {
                    Message = "Questions with categories",
                    Success = true,
                    Data = qlist
                };
            }
            else
            {
                return new()
                {
                    Message = "There are no questions.",
                    Success = false,
                    Data = new List<Question>()
                };
            }

        }
    }
}
