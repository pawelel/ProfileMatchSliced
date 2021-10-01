using System.Collections.Generic;
using System.Threading.Tasks;

using ProfileMatch.Models.Models;
using ProfileMatch.Models.Responses;

namespace ProfileMatch.Contracts
{
    public interface ICategoryRepository 
    {
        Task<List<Category>> GetCategories();
        Task<List<Category>> GetCategoriesWithQuestions();
    }
}
