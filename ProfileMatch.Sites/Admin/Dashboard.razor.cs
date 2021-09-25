using Microsoft.AspNetCore.Components;

using ProfileMatch.Models.Models;

using System.Collections.Generic;

namespace ProfileMatch.Pages.Admin.Pages
{
    public partial class AdminDashboardPage : ComponentBase
    {
        [Parameter] public IEnumerable<ApplicationUser> Users { get; set; }
    }
}