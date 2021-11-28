using ProfileMatch.Models.Models;

using System.Threading.Tasks;

namespace ProfileMatch.Services
{
    public interface IRedirection
    {
        Task<ApplicationUser> GetUser();
    }
}