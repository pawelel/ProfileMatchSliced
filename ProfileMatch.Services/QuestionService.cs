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

        public async Task<List<Question>> FindAllAsync()
        {
            return await wrapper.Question.FindAllAsync();

        }

        public async Task<Question> Create(Question question)
        {
        
            var doesExist = await wrapper.Question.FindSingleByConditionAsync(q => q.Name.Contains(question.Name));
            if (doesExist!=null)
            {

                return await wrapper.Question.Create(question);
            }
            else
            {
                return null;
            }
        
        }

        public async Task<List<Question>> GetQuestionsWithCategories()
        {
            return await wrapper.Question.GetQuestionsWithCategories();
        }
    }
}
