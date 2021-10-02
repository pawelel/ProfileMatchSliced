
using System.Collections.Generic;
using System.Threading.Tasks;

using ProfileMatch.Models.Models;

namespace ProfileMatch.Contracts
{
    public interface IUserAnswerRepository
    {
        Task<UserAnswer> Create(UserAnswer answer);
        Task<UserAnswer> Delete(UserAnswer answer);
        Task<UserAnswer> FindById(string userId, int answerOptionId);
        Task<List<UserAnswer>> GetAll();
        Task<UserAnswer> Update(UserAnswer answer);
        Task<List<UserAnswer>> GetUserAnswersForQuestion(int questionId);
        Task<List<AnswerOption>> GetAnswerOptionsForOptionAndQuestion(int answerOptionId, int questionId);
        Task<List<UserAnswer>> GetUserAnswersForUser(string userId);
    }
}
