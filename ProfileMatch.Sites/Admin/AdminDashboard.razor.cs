using System.Collections.Generic;

using Microsoft.AspNetCore.Components;

using ProfileMatch.Models.Models;

namespace ProfileMatch.Pages.Admin.Pages
{
    public partial class AdminDashboard : ComponentBase
    {
        [Parameter] public IEnumerable<ApplicationUser> Users { get; set; }
    }
}