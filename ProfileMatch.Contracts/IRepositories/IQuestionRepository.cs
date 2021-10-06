using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

using ProfileMatch.Models.Models;

namespace ProfileMatch.Contracts
{
    public interface IQuestionRepository
    {
        Task<Question> Create(Question question);

        Task<Question> Delete(Question question);

        Task<Question> FindByName(string questionName);

        Task<Question> FindById(int questionId);

        Task<List<Question>> GetQuestionsForCategory(int categoryId);

        Task<List<Question>> GetAll();

        Task Update(Question question);
        Task<List<Question>> GetQuestionsWithCategoriesAndOptions();
        Task<List<Question>> GetQuestionsWithCategories();
        Task<List<Question>> GetActiveQuestionsWithCategoriesAndOptions();
    }
}