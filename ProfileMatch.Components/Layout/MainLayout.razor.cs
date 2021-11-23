using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

using MudBlazor;

using ProfileMatch.Components.Theme;
using ProfileMatch.Contracts;
using ProfileMatch.Models.Models;
using ProfileMatch.Services;

using System;
using System.Data;
using System.Threading.Tasks;

namespace ProfileMatch.Components.Layout
{
    public partial class MainLayout : ComponentBase, IDisposable
    {
        [Parameter]public RenderFragment Body { get; set; }

        [Inject]private NavigationManager NavigationManager { get; set; }

        [Inject]public IThemeService ThemeService { get; set; }

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

        public ApplicationUser CurrentUser { get; set; } = new();
        private readonly MudTheme defaultTheme = new GeneralTheme();
        private MudTheme currentTheme = new DarkTheme();
        private readonly MudTheme darkTheme = new DarkTheme();

        [CascadingParameter] private Task<AuthenticationState> AuthSP { get; set; }

        [Inject]private UserManager<ApplicationUser> UserManager { get; set; }

        private async Task GetUserDetails()
        {
            var user = (await AuthSP).User;
            
            if (user.Identity.IsAuthenticated)
            {
                CurrentUser = await UserManager.GetUserAsync(user);
            }
            else
            {
                NavigationManager.NavigateTo("Identity/Account/Login", true);
            }
            if (CurrentUser!=null && CurrentUser.Email == "admin@admin.com")
            {
                if (!await UserManager.IsInRoleAsync(CurrentUser, "Admin"))
                    await UserManager.AddToRoleAsync(CurrentUser, "Admin");
            }
        }

        [Inject]
        private IStringLocalizer<LanguageService> L { get; set; }
    }
}