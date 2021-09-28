
using AutoMapper;

using ProfileMatch.Contracts;

namespace ProfileMatch.Services
{
    public class UserNoteService : IUserNoteService
    {
        private readonly IRepositoryWrapper wrapper;
        private readonly IMapper mapper;

        public UserNoteService(IRepositoryWrapper wrapper, IMapper mapper)
        {
            this.wrapper = wrapper;
            this.mapper = mapper;
        }
    }
}
