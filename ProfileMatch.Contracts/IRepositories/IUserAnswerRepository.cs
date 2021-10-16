using System.Collections.Generic;
using System.Threading.Tasks;

using ProfileMatch.Models.Models;

namespace ProfileMatch.Contracts
{
    public interface IUserAnswerRepository
    {
        Task<UserAnswer> Create(UserAnswer answer);

        Task<UserAnswer> Delete(UserAnswer answer);

        Task<List<UserAnswer>> GetAll();

        Task<UserAnswer> Update(UserAnswer answer);

        Task<List<UserAnswer>> GetUserAnswersForQuestion(int questionId);

        Task<List<UserAnswer>> GetUserAnswersForUser(string userId);

        Task<List<UserAnswer>> GetUserAnswersForLevelAndQuestion(int level, int questionId);

        Task<int> GetUserAnswerLevel(string userId, int optionId);

        Task<UserAnswer> FindById(UserAnswer userAnswer);

        Task<UserAnswer> FindByIdAsync(string userId, int optionId);

        UserAnswer FindById(string userId, int optionId);

        Task<UserAnswer> GetUserAnswer(string userId, int questionId);
    }
}