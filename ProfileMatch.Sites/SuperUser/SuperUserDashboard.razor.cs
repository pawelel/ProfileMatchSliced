using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;

using ProfileMatch.Models.Models;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileMatch.Sites.SuperUser
{
    public partial class SuperUserDashboard : ComponentBase
    {
        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationStateTask { get; set; }

        [Inject]
        public RoleManager<IdentityRole> RoleManager { get; set; }

        [Inject]
        public UserManager<ApplicationUser> UserManager { get; set; }

        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        readonly string ADMINISTRATION_ROLE = "Admin";
        private System.Security.Claims.ClaimsPrincipal CurrentUser;

        // Tracks the selected role for the currently selected user
        private string CurrentUserRole { get; set; } = "User";

        protected override async Task OnInitializedAsync()
        {
            // ensure there is a ADMINISTRATION_ROLE
            var RoleResult = await RoleManager.FindByNameAsync(ADMINISTRATION_ROLE);
            if (RoleResult == null)
            {
                // Create ADMINISTRATION_ROLE Role
                await RoleManager.CreateAsync(new IdentityRole(ADMINISTRATION_ROLE));
            }

            // Ensure a user named admin@admin.com is an Administrator
            var user = await UserManager.FindByNameAsync("admin@admin.com");
            if (user != null)
            {
                // Is admin@admin.com in administrator role?
                var UserResult = await UserManager.IsInRoleAsync(user, ADMINISTRATION_ROLE);
                if (!UserResult)
                {
                    // Put admin in Administrator role
                    await UserManager.AddToRoleAsync(user, ADMINISTRATION_ROLE);
                }
            }

            // Get the current logged in user
            CurrentUser = (await AuthenticationStateTask).User;

            // Get the users
            GetUsers();
        }

        public void GetUsers()
        {
            // Collection to hold users
            ColUsers = new();

            // get users from _UserManager
            var user = UserManager.Users.Select(x => new ApplicationUser
            {
                Id = x.Id,
                UserName = x.UserName,
                Email = x.Email,
                PasswordHash = x.PasswordHash
            });

            foreach (var item in user)
            {
                ColUsers.Add(item);
            }
        }

        // Collection to display the existing users
        private List<ApplicationUser> ColUsers = new();
    }
}
