using System.Collections.Generic;
using System.Threading.Tasks;

using ProfileMatch.Models.Responses;
using ProfileMatch.Models.ViewModels;

namespace ProfileMatch.Contracts
{
    public interface IQuestionService
    {
        Task<ServiceResponse<List<QuestionVM>>> GetQuestionsWithCategories();
    }
}
