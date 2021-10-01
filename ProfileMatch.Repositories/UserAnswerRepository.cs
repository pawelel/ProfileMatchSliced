
using Microsoft.EntityFrameworkCore;

using ProfileMatch.Contracts;
using ProfileMatch.Data;
using ProfileMatch.Models.Models;

namespace ProfileMatch.Repositories
{
    public class UserAnswerRepository : IUserAnswerRepository
    {
        private readonly IDbContextFactory<ApplicationDbContext> contextFactory;

        public UserAnswerRepository(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }
    }
}