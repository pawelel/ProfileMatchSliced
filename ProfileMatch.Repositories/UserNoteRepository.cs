
using ProfileMatch.Contracts;
using ProfileMatch.Data;
using ProfileMatch.Models.Models;

namespace ProfileMatch.Repositories
{

    public class UserNoteRepository : IUserNoteRepository
    {
        private readonly ApplicationDbContext repositoryContext;

        public UserNoteRepository(ApplicationDbContext repositoryContext)
        {
            this.repositoryContext = repositoryContext;
        }
    }
}
