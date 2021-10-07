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
        [Inject]
        public AuthenticationStateProvider AuthStateProv { get; set; }
        [Inject]
        UserManager<ApplicationUser> UserManager { get; set; }
      [CascadingParameter]  public string CurrentUserId { get; set; }
        public ApplicationUser CurrentUser { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await GetUserDetails();
        }
        private async Task GetUserDetails()
        {
            var authState = await AuthStateProv.GetAuthenticationStateAsync();
            var user = authState.User;
            if (user.Identity.IsAuthenticated)
            {
                CurrentUser = await UserManager.GetUserAsync(user);
                CurrentUserId = CurrentUser.Id;
            }
        }
    }
}