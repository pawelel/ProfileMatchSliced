
using AutoMapper;

using ProfileMatch.Contracts;

namespace ProfileMatch.Services
{
    public class UserAnswerService : IUserAnswerService
    {
        private readonly IRepositoryWrapper wrapper;
        private readonly IMapper mapper;

        public UserAnswerService(IRepositoryWrapper wrapper, IMapper mapper)
        {
            this.wrapper = wrapper;
            this.mapper = mapper;
        }
    }
}
