
using System.Collections.Generic;
using System.Threading.Tasks;

using ProfileMatch.Models.Models;
using ProfileMatch.Models.Responses;

namespace ProfileMatch.Contracts
{
    public interface IQuestionRepository 
    {
        Task<List<Question>> GetQuestionsWithCategories();
        Task<Question> Create(Question question);
        Task<Question> Delete(Question question);
        Task<Question> FindByName(string questionName);
        Task<Question> FindById(int questionId);
        Task<List<Question>> GetQuestionsForCategory(int categoryId);
        Task<List<Question>> GetAll();
        Task<Question> Update(Question question);
        Task<List<Question>> GetQuestionsWithCondition();
    }
}
