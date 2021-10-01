
using Microsoft.EntityFrameworkCore;

using ProfileMatch.Contracts;
using ProfileMatch.Data;
using ProfileMatch.Models.Models;

namespace ProfileMatch.Repositories
{

    public class UserNoteRepository : IUserNoteRepository
    {
        private readonly IDbContextFactory<ApplicationDbContext> contextFactory;

        public UserNoteRepository(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }
    }
}
