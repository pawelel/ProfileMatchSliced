


using ProfileMatch.Contracts;

namespace ProfileMatch.Services
{
    public class UserNeedCategoryService : IUserNeedCategoryService
    {
        private readonly IRepositoryWrapper wrapper;
        

        public UserNeedCategoryService(IRepositoryWrapper wrapper)
        {
            this.wrapper = wrapper;
            
        }
    }
}
