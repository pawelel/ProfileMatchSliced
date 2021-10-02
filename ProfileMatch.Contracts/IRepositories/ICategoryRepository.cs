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
        Task<Category> Create(Category category);
        Task<Category> Delete(Category category);
        Task<Category> FindByName(string categoryName);
        Task<Category> FindById(int categoryId);
        Task<List<Category>> GetAll();
        Task<Category> Update(Category category);
    }
}
