using System;
using System.Collections.Generic;
using System.Threading.Tasks;



using ProfileMatch.Contracts;
using ProfileMatch.Models.Models;
using ProfileMatch.Models.Responses;


namespace ProfileMatch.Services
{
    class QuestionService : IQuestionService
    {
        private readonly IRepositoryWrapper wrapper;
        

        public QuestionService(IRepositoryWrapper wrapper)
        {
            this.wrapper = wrapper;
            
        }

        public async Task<ServiceResponse<List<Question>>> FindAllAsync()
        {
            return await wrapper.Question.FindAllAsync();

        }

        public async Task<ServiceResponse<Question>> Create(Question question)
        {
        
            var doesExist = await wrapper.Question.FindSingleByConditionAsync(q => q.Name.Contains(question.Name));
            if (!doesExist.Success)
            {

                return await wrapper.Question.Create(question);
            }
            else
            {
                return new()
                {
                    Success = false,
                    Message = "User already exists."
                };
            }
        
        }

        public async Task<ServiceResponse<List<Question>>> GetQuestionsWithCategories()
        {
            return await wrapper.Question.GetQuestionsWithCategories();
        }
    }
}
