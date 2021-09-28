
using AutoMapper;

using ProfileMatch.Contracts;

namespace ProfileMatch.Services
{
    public class AnswerOptionService : IAnswerOptionService
    {
        private readonly IRepositoryWrapper wrapper;
        private readonly IMapper mapper;

        public AnswerOptionService(IRepositoryWrapper wrapper, IMapper mapper)
        {
            this.wrapper = wrapper;
            this.mapper = mapper;
        }
    }
}
