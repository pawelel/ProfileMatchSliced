
using ProfileMatch.Contracts;
using ProfileMatch.Data;
using ProfileMatch.Models.Models;

namespace ProfileMatch.Repositories
{
    public class AnswerOptionRepository :  IAnswerOptionRepository
    {
        private readonly ApplicationDbContext repositoryContext;

        public AnswerOptionRepository(ApplicationDbContext repositoryContext)
        {
            this.repositoryContext = repositoryContext;
        }
    }
}
