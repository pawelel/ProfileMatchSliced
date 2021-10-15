using Microsoft.AspNetCore.Components;

using ProfileMatch.Models.Models;

namespace ProfileMatch.Components.User
{
    public partial class UserCard : ComponentBase
    {
        [Parameter] public string Class { get; set; }
        [Parameter] public string Style { get; set; }
        [Parameter] public ApplicationUser CurrentUser { get; set; } = new();

    }
}