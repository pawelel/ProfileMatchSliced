
using AutoMapper;

using ProfileMatch.Contracts;

namespace ProfileMatch.Services
{
    public class UserNeedCategoryService : IUserNeedCategoryService
    {
        private readonly IRepositoryWrapper wrapper;
        private readonly IMapper mapper;

        public UserNeedCategoryService(IRepositoryWrapper wrapper, IMapper mapper)
        {
            this.wrapper = wrapper;
            this.mapper = mapper;
        }
    }
}
