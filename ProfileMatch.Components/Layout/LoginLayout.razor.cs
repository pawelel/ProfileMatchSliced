using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;
using MudBlazor;

using ProfileMatch.Components.Theme;

using ProfileMatch.Models.Entities;

using ProfileMatch.Services;

namespace ProfileMatch.Components.Layout
{
    public partial class LoginLayout : ComponentBase
    {
        [Parameter] public RenderFragment Body { get; set; }

        [Inject] IJSRuntime JSRuntime { get; set; }
       
        bool _isDarkTheme;
     
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await GetTheme();
            }
            base.OnAfterRender(firstRender);
        }

        string _theme = "";
        private readonly MudTheme _lightTheme = new LightTheme();
        private MudTheme _currentTheme = new DarkTheme();
        private readonly MudTheme _darkTheme = new DarkTheme();
        
 

        async Task ChangeTheme()
        {
            if (_theme == "light")
            {
                _isDarkTheme = true;
                _theme = "dark";
                _currentTheme = _darkTheme;
            }
            else
            {
                _isDarkTheme = false;
                _theme = "light";
                _currentTheme = _lightTheme;
            }
            await JSRuntime.InvokeVoidAsync("setCookie", "theme", _theme);

            StateHasChanged();
        }

        async Task GetTheme()
        {
            _theme = await JSRuntime.InvokeAsync<string>("getCookie", "theme");
            if (_theme == "dark")
            {
                _isDarkTheme = true;
                _currentTheme = _darkTheme;
            }
            else
            {
                _theme = "light";
                _isDarkTheme = false;
                _currentTheme = _lightTheme;
            }
            StateHasChanged();
        }
    }
}
