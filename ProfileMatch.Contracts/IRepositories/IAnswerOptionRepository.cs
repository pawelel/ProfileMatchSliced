
using System.Collections.Generic;
using System.Threading.Tasks;

using ProfileMatch.Models.Models;

namespace ProfileMatch.Contracts
{
    public interface IAnswerOptionRepository
    {
        Task<List<AnswerOption>> GetAnswerOptionsWithQuestions();
        Task<AnswerOption> Create(AnswerOption answerOption);
        Task<AnswerOption> Delete(AnswerOption answerOption);
        Task<AnswerOption> FindByName(string answerOptionName);
        Task<AnswerOption> FindById(int answerOptionId);
        Task<List<AnswerOption>> GetAnswerOptionsForQuestion(int questionId);
        Task<List<AnswerOption>> GetAll();
        Task<AnswerOption> Update(AnswerOption answerOption);
    }
}
