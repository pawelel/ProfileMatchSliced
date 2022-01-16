using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;
using MudBlazor;

using ProfileMatch.Components.Theme;

using ProfileMatch.Models.Models;

using ProfileMatch.Services;

namespace ProfileMatch.Components.Layout
{
    public partial class LoginLayout : ComponentBase
    {
        [Parameter] public RenderFragment Body { get; set; }

        [Inject] IJSRuntime JSRuntime { get; set; }
       
        bool isDarkTheme;
     
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await GetTheme();
            }
            base.OnAfterRender(firstRender);
        }

        string theme = "";
        private readonly MudTheme lightTheme = new LightTheme();
        private MudTheme currentTheme = new DarkTheme();
        private readonly MudTheme darkTheme = new DarkTheme();
        
 

        async Task ChangeTheme()
        {
            if (theme == "light")
            {
                isDarkTheme = true;
                theme = "dark";
                currentTheme = darkTheme;
            }
            else
            {
                isDarkTheme = false;
                theme = "light";
                currentTheme = lightTheme;
            }
            await JSRuntime.InvokeVoidAsync("setCookie", "theme", theme);

            StateHasChanged();
        }

        async Task GetTheme()
        {
            theme = await JSRuntime.InvokeAsync<string>("getCookie", "theme");
            if (theme == "dark")
            {
                isDarkTheme = true;
                currentTheme = darkTheme;
            }
            else
            {
                theme = "light";
                isDarkTheme = false;
                currentTheme = lightTheme;
            }
            StateHasChanged();
        }
    }
}
