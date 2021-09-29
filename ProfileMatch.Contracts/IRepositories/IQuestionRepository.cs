
using System.Collections.Generic;
using System.Threading.Tasks;

using ProfileMatch.Models.Models;
using ProfileMatch.Models.Responses;

namespace ProfileMatch.Contracts
{
    public interface IQuestionRepository : IRepositoryBase<Question>
    {
        Task<List<Question>> GetQuestionsWithCategories();
    }
}
