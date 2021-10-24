using ProfileMatch.Models.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProfileMatch.Contracts
{
    public interface IQuestionRepository
    {
        Task<Question> Create(Question question);

        Task<Question> Delete(Question question);

        Task<Question> FindById(int questionId);

        Task<List<Question>> GetQuestionsForCategory(int categoryId);

        Task<List<Question>> GetAll();

        Task Update(Question question);

        Task<List<Question>> GetQuestionsWithCategoriesAndOptions();

        Task<List<Question>> GetQuestionsWithCategories();

        Task<List<Question>> GetActiveQuestionsWithCategoriesAndOptions();

        Task<List<Question>> GetActiveQuestionsWithCategoriesAndOptionsForUser(string userId);

        Task<bool> IsDuplicated(Question question);
    }
}