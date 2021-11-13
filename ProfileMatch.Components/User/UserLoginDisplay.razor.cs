using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

using ProfileMatch.Models.Models;
using ProfileMatch.Services;

namespace ProfileMatch.Components.User
{
    public partial class UserUserLoginDisplay : ComponentBase
    {
        [CascadingParameter] public ApplicationUser CurrentUser { get; set; }

        [Inject]
#pragma warning disable IDE0051 // Remove unused private members
        private IStringLocalizer<LanguageService> L { get; set; }
#pragma warning restore IDE0051 // Remove unused private members
    }
}