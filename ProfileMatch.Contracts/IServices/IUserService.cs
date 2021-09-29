using System.Collections.Generic;
using System.Threading.Tasks;

using ProfileMatch.Models.Models;
using ProfileMatch.Models.Responses;


namespace ProfileMatch.Contracts
{
    public interface IUserService
    {
        Task<List<ApplicationUser>> FindAllAsync();
        Task<ApplicationUser> Create(ApplicationUser user);
        Task<ApplicationUser> Delete(string id);
       Task<ApplicationUser> Update(ApplicationUser user);
        Task<ApplicationUser> FindSingleByIdAsync(string id);
        Task<ApplicationUser> FindSingleByEmailAsync(string email);
        Task<bool> Exist(ApplicationUser editUser);
        Task<bool> Exist(string email);
    }
}
