using System.Collections.Generic;

using Microsoft.AspNetCore.Components;

using ProfileMatch.Models.Models;

namespace ProfileMatch.Sites.User
{
    public partial class UserDashboard : ComponentBase
    {
        [Parameter] public IEnumerable<ApplicationUser> Users { get; set; }
    }
}