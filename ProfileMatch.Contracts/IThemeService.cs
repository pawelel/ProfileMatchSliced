using ProfileMatch.Models.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileMatch.Contracts
{
   public interface IThemeService
    {
        event Action<Preferences> OnChange;

        Task<Preferences> GetPreferences();

        Task ToggleDarkMode();
    }
}
