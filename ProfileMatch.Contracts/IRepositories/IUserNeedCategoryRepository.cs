using ProfileMatch.Models.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProfileMatch.Contracts
{
    public interface IUserNeedCategoryRepository
    {
        Task<UserNeedCategory> Create(UserNeedCategory need);

        Task<UserNeedCategory> Delete(UserNeedCategory need);

        Task<UserNeedCategory> FindById(string userId, int categoryId);

        Task<List<UserNeedCategory>> GetAll();

        Task<UserNeedCategory> Update(UserNeedCategory need);

        Task<List<UserNeedCategory>> GetUserNeedCategoriesForUser(string userId);

        Task<List<UserNeedCategory>> GetUserNeedCategoriesForCategory(int categoryId);
    }
}