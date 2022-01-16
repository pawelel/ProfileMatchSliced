using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;

using MudBlazor;

using ProfileMatch.Components.Theme;
using ProfileMatch.Data;
using ProfileMatch.Models.Models;
using ProfileMatch.Repositories;
using ProfileMatch.Services;

using System;
using System.Data;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

using static Microsoft.AspNetCore.Razor.Language.TagHelperMetadata;

namespace ProfileMatch.Components.Layout
{
    public partial class MainLayout : ComponentBase
    {
        [Parameter] public RenderFragment Body { get; set; }
        [Inject] IRedirection Redirection { get; set; }
        ApplicationUser CurrentUser = new();
        [Inject] IJSRuntime JSRuntime { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }

        private bool _drawerOpen = true;
        bool isDarkTheme;
        
        private void DrawerToggle()
        {
            _drawerOpen = !_drawerOpen;
        }

        protected override async Task OnInitializedAsync()
        {
            if (!NavigationManager.Uri.Contains("emailconfirmation"))
            {
 CurrentUser = await Redirection.GetUser();
            }
            else
            {
                NavigationManager.NavigateTo("/account/login");
            }
        }
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
        void GoBack()
        {
            NavigationManager.NavigateTo("admin/dashboard");
        }
                
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
            if (theme=="dark")
            {
                isDarkTheme = true;
                currentTheme = darkTheme;
            }
            else
            {
                theme = "light";
                isDarkTheme= false;
                currentTheme = lightTheme;
            }
            StateHasChanged();
        }

    }
}
