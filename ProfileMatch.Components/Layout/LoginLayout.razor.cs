using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

using MudBlazor;

using ProfileMatch.Components.Theme;
using ProfileMatch.Contracts;
using ProfileMatch.Models.Models;
using ProfileMatch.Services;

namespace ProfileMatch.Components.Layout
{
    public partial class LoginLayout : ComponentBase, IDisposable
    {
        [Parameter]
        public RenderFragment Body { get; set; }

        [Inject]
        public IThemeService ThemeService { get; set; }

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
            GC.SuppressFinalize(this);
        }

        private readonly MudTheme defaultTheme = new GeneralTheme();
        private MudTheme currentTheme;
        private readonly MudTheme darkTheme = new DarkTheme();
        [Inject]
        IStringLocalizer<LanguageService> L { get; set; }
    }
}