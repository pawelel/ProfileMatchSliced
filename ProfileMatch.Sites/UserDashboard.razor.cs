using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

using ProfileMatch.Models.Entities;
using ProfileMatch.Services;

namespace ProfileMatch.Sites
{
    public partial class UserDashboard : ComponentBase
    {
        [Parameter] public int ActiveIndex { get; set; }
    }
}