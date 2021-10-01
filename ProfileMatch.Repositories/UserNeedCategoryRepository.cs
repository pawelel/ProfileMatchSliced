
using Microsoft.EntityFrameworkCore;

using ProfileMatch.Contracts;
using ProfileMatch.Data;
using ProfileMatch.Models.Models;

namespace ProfileMatch.Repositories
{
    public class UserNeedCategoryRepository : IUserNeedCategoryRepository
    {
        private readonly IDbContextFactory<ApplicationDbContext> contextFactory;

        public UserNeedCategoryRepository(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }
    }
}
