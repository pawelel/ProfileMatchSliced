using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;

using MudBlazor;

using ProfileMatch.Data;
using ProfileMatch.Models.Models;
using ProfileMatch.Repositories;
using ProfileMatch.Services;

using System;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProfileMatch.Components.User
{
    public partial class UserAccount : ComponentBase
    {
        [Inject]
        private ISnackbar Snackbar { get; set; }
        public string AvatarImageLink { get; set; }
        public string AvatarIcon { get; set; }
        public string FirstName { get; set; } = "Karol";
        public string LastName { get; set; } = "Pluciński";
        public string JobTitle { get; set; } = "IT Consultant";
        public string Email { get; set; } = "karol@test.com";
        [Parameter] public string UserId { get; set; }
        [Inject] public AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        ApplicationUser CurrentUser;
        [Inject] DataManager<ApplicationUser, ApplicationDbContext> AppUserManager { get; set; }
        protected override async Task OnInitializedAsync()
        {
            if (!string.IsNullOrEmpty(UserId))
            {
                CurrentUser = await AppUserManager.GetById(UserId);
            }
            else
            {
                var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
                var principal = authState.User;
                if (principal != null)
                    UserId = principal.FindFirst("UserId").Value;
                CurrentUser = await AppUserManager.GetById(UserId);
            }
        }


        [Inject] private IStringLocalizer<LanguageService> L { get; set; }
    }
}