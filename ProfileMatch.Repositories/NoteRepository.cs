
using Microsoft.EntityFrameworkCore;

using ProfileMatch.Contracts;
using ProfileMatch.Data;
using ProfileMatch.Models.Models;

namespace ProfileMatch.Repositories
{
    public class NoteRepository :  INoteRepository
    {
        private readonly IDbContextFactory<ApplicationDbContext> contextFactory;

        public NoteRepository(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }
    }
}
