using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProfileMatch.Models.Models;
using ProfileMatch.Models.Responses;

namespace ProfileMatch.Contracts
{
    public interface ICategoryRepository : IRepositoryBase<Category>
    {
        Task<ServiceResponse<List<Category>>> GetCategoriesWithQuestions();
    }
}
