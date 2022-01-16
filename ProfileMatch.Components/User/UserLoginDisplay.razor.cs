using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

using ProfileMatch.Models.Models;
using ProfileMatch.Services;

namespace ProfileMatch.Components.User
{
    public partial class UserLoginDisplay : ComponentBase
    {
        [CascadingParameter] public ApplicationUser CurrentUser { get; set; }
    }
}