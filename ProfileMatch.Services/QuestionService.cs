using System;
using System.Collections.Generic;
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

        public async Task<List<QuestionVM>> FindAllAsync()
        {
            var request = await wrapper.Question.FindAllAsync();
            return mapper.Map<List<Question>, List<QuestionVM>>(request.Data);

        }

        public async Task<ServiceResponse<QuestionVM>> Create(QuestionVM questionVM)
        {
                ServiceResponse<QuestionVM> result = new();
            var doesExist = await wrapper.Question.FindSingleByConditionAsync(q => q.Name.Contains(questionVM.Name));
            if (!doesExist.Success)
            {
                var request = mapper.Map<QuestionVM, Question>(questionVM);
                var response = await wrapper.Question.Create(request);
                result.Data = mapper.Map<Question, QuestionVM>(response.Data);
                result.Message = response.Message;
                result.Success = response.Success;
            }
                return result;
        }

        public Task<ServiceResponse<List<QuestionVM>>> GetQuestionsWithCategories()
        {
            throw new NotImplementedException();
        }
    }
}
