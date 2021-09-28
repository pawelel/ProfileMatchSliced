using System.Collections.Generic;
using System.Threading.Tasks;

using ProfileMatch.Models.Models;
using ProfileMatch.Models.Responses;


namespace ProfileMatch.Contracts
{
    public interface IUserService
    {
        Task<ServiceResponse<List<ApplicationUser>>> FindAllAsync();
        Task<ServiceResponse<ApplicationUser>> Create(ApplicationUser user);
        Task<ServiceResponse<ApplicationUser>> Delete(string id);
       Task< ServiceResponse<ApplicationUser>> Update(ApplicationUser user);
        Task<ServiceResponse<ApplicationUser>> FindSingleByIdAsync(string id);
        Task<ServiceResponse<ApplicationUser>> FindSingleByEmailAsync(string email);
        Task<bool> Exist(ApplicationUser editUser);
        Task<bool> Exist(string email);
    }
}
