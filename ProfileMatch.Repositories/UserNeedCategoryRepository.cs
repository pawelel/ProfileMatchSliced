
using ProfileMatch.Contracts;
using ProfileMatch.Data;
using ProfileMatch.Models.Models;

namespace ProfileMatch.Repositories
{
    public class UserNeedCategoryRepository : RepositoryBase<UserNeedCategory>, IUserNeedCategoryRepository
    {
        public UserNeedCategoryRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {

        }
    }
}
