using System.Collections.Generic;

using Microsoft.AspNetCore.Components;

using ProfileMatch.Models.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Components.Authorization;
using System.Threading.Tasks;

namespace ProfileMatch.Sites.User
{
    public partial class UserDashboard : ComponentBase
    {
        [Parameter] public IEnumerable<ApplicationUser> Users { get; set; }
        [Inject]
        public AuthenticationStateProvider AuthStateProv { get; set; }
        [Inject]
        UserManager<ApplicationUser> UserManager { get; set; }
        private async Task GetUserDetails()
        {
            var authState = await AuthStateProv.GetAuthenticationStateAsync();
            var user = authState.User;
            if (user.Identity.IsAuthenticated)
            {
                var currentUser = await UserManager.GetUserAsync(user);
                var currentUserId = currentUser.Id;
            }
        }
    }
}