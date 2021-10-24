using Blazored.LocalStorage;

using Microsoft.JSInterop;

using ProfileMatch.Contracts;
using ProfileMatch.Models.Models;

using System;
using System.Threading.Tasks;

namespace ProfileMatch.Services
{
    public class ThemeService : IThemeService
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly IJSRuntime _jsRuntime;

        public event Action<Preferences> OnChange;

        public ThemeService(ILocalStorageService localStorageService, IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
            _localStorageService = localStorageService;
        }

        public async Task ToggleDarkMode()
        {
            Preferences preferences = await GetPreferences().ConfigureAwait(true);
            var newPreferences = preferences
                with
            { DarkMode = !preferences.DarkMode };

            await _localStorageService.SetItemAsync("preferences", newPreferences).ConfigureAwait(true);
            OnChange?.Invoke(newPreferences);
        }

        public async Task<Preferences> GetPreferences()
        {
            //preset theme settings
            if (await _localStorageService.ContainKeyAsync("preferences").ConfigureAwait(true))
                return await _localStorageService.GetItemAsync<Preferences>("preferences").ConfigureAwait(true);

            // default OS settings
            bool prefersDarkMode = await _jsRuntime.InvokeAsync<bool>("prefersDarkMode").ConfigureAwait(true);

            return new Preferences
            {
                DarkMode = prefersDarkMode
            };
        }
    }
}