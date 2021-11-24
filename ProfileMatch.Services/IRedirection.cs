using ProfileMatch.Models.Models;

using System.Threading.Tasks;

namespace ProfileMatch.Services
{
    public interface IRedirection
    {
        ApplicationUser AppUser { get; set; }

        Task<ApplicationUser> GetUser();
    }
}