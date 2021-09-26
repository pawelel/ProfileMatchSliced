using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using ProfileMatch.Models.Models;
using ProfileMatch.Models.ViewModels;

namespace ProfileMatch.Contracts
{
   public interface IUserService
    {
        Task Create(EditUserVM user);
        Task Delete(string id);
        Task Update(EditUserVM user);
        Task<IEnumerable<EditUserVM>> FindAllAsync();
        Task<EditUserVM> FindSingleByIdAsync(string id);
        Task<ApplicationUser> FindSingleByEmailAsync(string email);
        Task<bool> Exist(EditUserVM editUserVM);
        Task<bool> Exist(string id);
    }
}
