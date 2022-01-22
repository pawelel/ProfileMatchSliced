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
        ApplicationUser _currentUser = new();
        [Inject] IJSRuntime JSRuntime { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }

        private bool _drawerOpen = true;
        bool _isDarkTheme;

        private void DrawerToggle()
        {
            _drawerOpen = !_drawerOpen;
        }

        protected override async Task OnInitializedAsync()
        {
            if (!NavigationManager.Uri.Contains("emailconfirmation"))
            {
                _currentUser = await Redirection.GetUser();
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

        string _theme = "";
        private readonly MudTheme _lightTheme = new LightTheme();
        private MudTheme _currentTheme = new DarkTheme();
        private readonly MudTheme _darkTheme = new DarkTheme();
        void GoBack()
        {
            NavigationManager.NavigateTo("admin/dashboard");
        }

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
