using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;

using MudBlazor;


using ProfileMatch.Components.Theme;
using ProfileMatch.Contracts;
using ProfileMatch.Models.Models;

namespace ProfileMatch.Components.Layout
{
    public partial class LoginLayout : ComponentBase, IDisposable
    {
        [Parameter]
        public RenderFragment Body { get; set; }
        [Inject]
        public IThemeService ThemeService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        bool _drawerOpen = true;

        void DrawerToggle()
        {
            _drawerOpen = !_drawerOpen;
        }

        private Preferences _preferences = new();

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            ThemeService.OnChange += ThemeServiceOnChange;
            if (firstRender)
            {
                _preferences = await ThemeService.GetPreferences();
                currentTheme = _preferences.DarkMode ? darkTheme : defaultTheme;
                StateHasChanged();
            }

        }
        private void ThemeServiceOnChange(Preferences newPreferences)
        {
            _preferences = newPreferences;
            currentTheme = _preferences.DarkMode ? darkTheme : defaultTheme;
            StateHasChanged();
        }

        public void Dispose()
        {
            ThemeService.OnChange -= ThemeServiceOnChange;
        }

        MudTheme defaultTheme = new GeneralTheme();
        MudTheme currentTheme;
        MudTheme darkTheme = new DarkTheme();

    }
}