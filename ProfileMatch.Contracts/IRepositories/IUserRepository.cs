using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using ProfileMatch.Models.Models;
using ProfileMatch.Models.ViewModels;

namespace ProfileMatch.Contracts
{
    public interface IUserRepository
    {
        Task<ApplicationUser> Create(ApplicationUser user);
        Task<ApplicationUser> Delete(ApplicationUser user);
        Task<ApplicationUser> FindByEmail(string email);
        Task<ApplicationUser> FindById(string id);
        Task<List<ApplicationUser>> GetAll();
        Task<ApplicationUser> Update(ApplicationUser user);
        Task<List<QuestionUserLevelVM>> GetUsersWithQuestionAnswerLevel(int questionId, int level);
    }
}
