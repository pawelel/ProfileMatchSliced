using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

using ProfileMatch.Contracts;
using ProfileMatch.Models.Models;
using ProfileMatch.Models.ViewModels;

namespace ProfileMatch.Services
{
    class QuestionService
    {
        private readonly IRepositoryWrapper wrapper;
        private readonly IMapper mapper;

        public QuestionService(IRepositoryWrapper wrapper, IMapper mapper)
        {
            this.wrapper = wrapper;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<QuestionVM>> FindAllAsync()
        {
            var request = await wrapper.Question.FindAllAsync();
            return mapper.Map<IEnumerable<QuestionVM>>(request);

        }

        public async Task Create(QuestionVM questionVM)
        {
           var request = mapper.Map<Question>(questionVM);
          await  wrapper.Question.Create(request);
        }

    }
}
