
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using ProfileMatch.Contracts;
using ProfileMatch.Data;
using ProfileMatch.Models.Models;
using ProfileMatch.Models.Responses;

namespace ProfileMatch.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly ApplicationDbContext repositoryContext;

        public QuestionRepository(ApplicationDbContext repositoryContext)
        {
            this.repositoryContext = repositoryContext;
        }

        public async Task<List<Question>> GetQuestionsWithCategories()
        {
           return await repositoryContext.Questions.Include(c => c.Category).AsNoTracking().ToListAsync();
        

        }
    }
}
