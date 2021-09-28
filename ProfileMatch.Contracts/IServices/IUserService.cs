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
        Task<ServiceResponse<List<ApplicationUserVM>>> FindAllAsync();
        Task<ServiceResponse<ApplicationUserVM>> Create(ApplicationUserVM user);
        Task<ServiceResponse<ApplicationUserVM>> Delete(string id);
        ServiceResponse<ApplicationUserVM> Update(ApplicationUserVM userVM);
        Task<ServiceResponse<ApplicationUserVM>> FindSingleByIdAsync(string id);
        Task<ServiceResponse<ApplicationUserVM>> FindSingleByEmailAsync(string email);
        Task<bool> Exist(ApplicationUserVM editUserVM);
        Task<bool> Exist(string id);
    }
}
