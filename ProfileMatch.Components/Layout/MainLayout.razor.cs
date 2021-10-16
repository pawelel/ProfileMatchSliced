using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;

using MudBlazor;

using ProfileMatch.Components.Theme;
using ProfileMatch.Contracts;
using ProfileMatch.Models.Models;

namespace ProfileMatch.Components.Layout
{
    public partial class MainLayout : ComponentBase, IDisposable
    {
        [Parameter]
        public RenderFragment Body { get; set; }

        [Inject]
        public IThemeService ThemeService { get; set; }

        private bool _drawerOpen = true;

        private void DrawerToggle()
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

        protected override async Task OnInitializedAsync()
        {
            await GetUserDetails();
        }

        public void Dispose()
        {
            ThemeService.OnChange -= ThemeServiceOnChange;
            GC.SuppressFinalize(this);
        }

        public ApplicationUser CurrentUser { get; set; }
        private readonly MudTheme defaultTheme = new GeneralTheme();
        private MudTheme currentTheme;
        private readonly MudTheme darkTheme = new DarkTheme();

        [CascadingParameter]
        private Task<AuthenticationState> AuthSP { get; set; }

        [Inject]
        private UserManager<ApplicationUser> UserManager { get; set; }

        private async Task GetUserDetails()
        {
            var user = (await AuthSP).User;

            if (user.Identity.IsAuthenticated)
            {
                CurrentUser = await UserManager.GetUserAsync(user);
            }
        }
    }
}