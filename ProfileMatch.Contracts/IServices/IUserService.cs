using System.Collections.Generic;
using System.Threading.Tasks;

using ProfileMatch.Models.Responses;
using ProfileMatch.Models.ViewModels;

namespace ProfileMatch.Contracts
{
    public interface IUserService
    {
        Task<ServiceResponse<List<ApplicationUserVM>>> FindAllAsync();
        Task<ServiceResponse<ApplicationUserVM>> Create(ApplicationUserVM user);
        Task<ServiceResponse<ApplicationUserVM>> Delete(string id);
       Task< ServiceResponse<ApplicationUserVM>> Update(ApplicationUserVM userVM);
        Task<ServiceResponse<ApplicationUserVM>> FindSingleByIdAsync(string id);
        Task<ServiceResponse<ApplicationUserVM>> FindSingleByEmailAsync(string email);
        Task<bool> Exist(ApplicationUserVM editUserVM);
        Task<bool> Exist(string id);
    }
}
