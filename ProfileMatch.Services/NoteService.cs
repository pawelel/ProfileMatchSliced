
using AutoMapper;

using ProfileMatch.Contracts;

namespace ProfileMatch.Services
{
    public class NoteService : INoteService
    {
        private readonly IRepositoryWrapper wrapper;
        private readonly IMapper mapper;

        public NoteService(IRepositoryWrapper wrapper, IMapper mapper)
        {
            this.wrapper = wrapper;
            this.mapper = mapper;
        }
    }
}
