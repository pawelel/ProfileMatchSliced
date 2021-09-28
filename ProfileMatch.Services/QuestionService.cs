using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

using ProfileMatch.Contracts;
using ProfileMatch.Models.Models;
using ProfileMatch.Models.Responses;
using ProfileMatch.Models.ViewModels;

namespace ProfileMatch.Services
{
    class QuestionService : IQuestionService
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

        public async Task<ServiceResponse<QuestionVM>> Create(QuestionVM questionVM)
        {
            var doesExist = await wrapper.Question.FindSingleByConditionAsync(q => q.Name.Contains(questionVM.Name));
            if (!doesExist.Success)
            {
           var request = mapper.Map<Question>(questionVM);
           var response = wrapper.Question.Create(request);
                return mapper.Map<ServiceResponse<QuestionVM>>(response);
            }
            return new ServiceResponse<QuestionVM>()
            {
                Message = "Question with that name already exists.",
                Success = false
            };
        }
    }
}
