
using ProfileMatch.Contracts;
using ProfileMatch.Data;
using ProfileMatch.Models.Models;

namespace ProfileMatch.Repositories
{
    public class NoteRepository :  INoteRepository
    {
        private readonly ApplicationDbContext repositoryContext;

        public NoteRepository(ApplicationDbContext repositoryContext)
        {
            this.repositoryContext = repositoryContext;
        }
    }
}
