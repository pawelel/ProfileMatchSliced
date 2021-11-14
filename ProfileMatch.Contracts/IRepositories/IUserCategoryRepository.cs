using ProfileMatch.Models.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProfileMatch.Contracts
{
    public interface IUserCategoryRepository
    {
        Task<UserCategory> Create(UserCategory need);

        Task<UserCategory> Delete(UserCategory need);

        Task<UserCategory> FindById(string userId, int categoryId);

        Task<List<UserCategory>> GetAll();

        Task<UserCategory> Update(UserCategory need);

        Task<List<UserCategory>> GetUserNeedCategoriesForUser(string userId);

        Task<List<UserCategory>> GetUserNeedCategoriesForCategory(int categoryId);
    }
}