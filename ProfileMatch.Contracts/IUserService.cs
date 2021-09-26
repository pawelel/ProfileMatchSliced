using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProfileMatch.Models.Models;
using ProfileMatch.Models.ViewModels;

namespace ProfileMatch.Contracts
{
   public interface IUserService
    {
        Task<List<ApplicationUser>> GetAllUsers();
        Task CreateUser(EditUserVM user);
        Task DeleteUser(string id);
    }
}
