using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

using MudBlazor;

using ProfileMatch.Components.Theme;
using ProfileMatch.Contracts;
using ProfileMatch.Data;
using ProfileMatch.Models.Models;
using ProfileMatch.Repositories;
using ProfileMatch.Services;

using System;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProfileMatch.Components.Layout
{
    public partial class MainLayout : ComponentBase, IDisposable
    {
        [Parameter] public RenderFragment Body { get; set; }
        [CascadingParameter] private Task<AuthenticationState> AuthSP { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }
        [Inject] IRedirection Redirection { get; set; }
        [Inject] DataManager<ApplicationUser, ApplicationDbContext> ApplicationUserManager { get; set; }
        string UserId;
        ApplicationUser CurrentUser = new();
        [Inject] public IThemeService ThemeService { get; set; }

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
            CurrentUser = await Redirection.GetUser();
        }

        public void Dispose()
        {
            ThemeService.OnChange -= ThemeServiceOnChange;
            GC.SuppressFinalize(this);
        }

        private readonly MudTheme defaultTheme = new GeneralTheme();
        private MudTheme currentTheme = new DarkTheme();
        private readonly MudTheme darkTheme = new DarkTheme();

        //private async Task RedirectToLogin()
        //{
        //    var user = (await AuthSP).User;
        //    if (!user.Identity.IsAuthenticated)
        //    {
        //        NavigationManager.NavigateTo("Identity/Account/Login", true);
        //    }
        //    else
        //    {
        //        UserId = user.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).First().Value;
        //        CurrentUser = await ApplicationUserManager.GetById(UserId);
        //    }
        //}

        [Inject]
        private IStringLocalizer<LanguageService> L { get; set; }
    }
}