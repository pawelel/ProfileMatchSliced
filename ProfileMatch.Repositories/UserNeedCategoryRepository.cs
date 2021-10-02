using System.Collections.Generic;
using System.Threading.Tasks;

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

        public Task<UserNeedCategory> Create(UserNeedCategory need)
        {
            throw new System.NotImplementedException();
        }

        public Task<UserNeedCategory> Delete(UserNeedCategory need)
        {
            throw new System.NotImplementedException();
        }

        public Task<UserNeedCategory> FindById(string userId, int categoryId)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<UserNeedCategory>> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Task<UserNeedCategory> Update(UserNeedCategory need)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<UserNeedCategory>> GetUserNeedCategoriesForUser(string userId)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<UserNeedCategory>> GetUserNeedCategoriesForCategory(int categoryId)
        {
            throw new System.NotImplementedException();
        }
    }
}