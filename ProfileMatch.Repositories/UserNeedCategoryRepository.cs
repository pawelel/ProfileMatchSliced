
using ProfileMatch.Contracts;
using ProfileMatch.Data;
using ProfileMatch.Models.Models;

namespace ProfileMatch.Repositories
{
    public class UserNeedCategoryRepository : IUserNeedCategoryRepository
    {
        private readonly ApplicationDbContext repositoryContext;

        public UserNeedCategoryRepository(ApplicationDbContext repositoryContext)  
        {
            this.repositoryContext = repositoryContext;
        }
    }
}
