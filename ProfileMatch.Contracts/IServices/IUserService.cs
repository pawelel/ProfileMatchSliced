using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using ProfileMatch.Models.Models;
using ProfileMatch.Models.Responses;
using ProfileMatch.Models.ViewModels;

namespace ProfileMatch.Contracts
{
   public interface IUserService
    {
        Task Create(ApplicationUserVM user);
        Task Delete(string id);
        Task Update(ApplicationUserVM user);
        Task<ServiceResponse<List<ApplicationUserVM>>> FindAllAsync();
        Task<ApplicationUserVM> FindSingleByIdAsync(string id);
        Task<ApplicationUser> FindSingleByEmailAsync(string email);
        Task<bool> Exist(ApplicationUserVM editUserVM);
        Task<bool> Exist(string id);
    }
}
