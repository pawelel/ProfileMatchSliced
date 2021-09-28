
using ProfileMatch.Contracts;
using ProfileMatch.Data;
using ProfileMatch.Models.Models;

namespace ProfileMatch.Repositories
{
    public class AnswerOptionRepository : RepositoryBase<AnswerOption>, IAnswerOptionRepository
    {
        public AnswerOptionRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {

        }
    }
}
