
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
        private readonly IDbContextFactory<ApplicationDbContext> contextFactory;

        public QuestionRepository(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public async Task<List<Question>> GetQuestionsWithCategories()
        {
            using ApplicationDbContext repositoryContext = contextFactory.CreateDbContext();
            return await repositoryContext.Questions.Include(c => c.Category).AsNoTracking().ToListAsync();
        

        }
    }
}
