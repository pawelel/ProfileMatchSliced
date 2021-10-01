
using Microsoft.EntityFrameworkCore;

using ProfileMatch.Contracts;
using ProfileMatch.Data;
using ProfileMatch.Models.Models;

namespace ProfileMatch.Repositories
{
    public class AnswerOptionRepository :  IAnswerOptionRepository
    {
        private readonly IDbContextFactory<ApplicationDbContext> contextFactory;

        public AnswerOptionRepository(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }
    }
}
