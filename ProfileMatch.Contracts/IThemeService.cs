using System;
using System.Threading.Tasks;

using ProfileMatch.Models.Models;

namespace ProfileMatch.Contracts
{
    public interface IThemeService
    {
        event Action<Preferences> OnChange;

        Task<Preferences> GetPreferences();

        Task ToggleDarkMode();
    }
}
