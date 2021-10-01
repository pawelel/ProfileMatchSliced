
using ProfileMatch.Contracts;
using ProfileMatch.Data;
using ProfileMatch.Models.Models;

namespace ProfileMatch.Repositories
{
    public class UserAnswerRepository : IUserAnswerRepository
    {
        private readonly ApplicationDbContext repositoryContext;

        public UserAnswerRepository(ApplicationDbContext repositoryContext)
        {
            this.repositoryContext = repositoryContext;
        }
    }
}