using ProfileMatch.Models.Entities;

using System.Threading.Tasks;

namespace ProfileMatch.Services
{
    public interface IRedirection
    {
        Task<ApplicationUser> GetUser();
    }
}