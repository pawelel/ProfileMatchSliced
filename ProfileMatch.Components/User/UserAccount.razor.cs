using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

using MudBlazor;

using ProfileMatch.Models.Models;
using ProfileMatch.Services;

using System;

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

        [CascadingParameter] public ApplicationUser CurrentUser { get; set; }

        

        private void SaveChanges(string message, Severity severity)
        {
            Snackbar.Add(message, severity, config =>
            {
                config.ShowCloseIcon = false;
            });
        }

        [Inject]
        private IStringLocalizer<LanguageService> L { get; set; }
    }
}