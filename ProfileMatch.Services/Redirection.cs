using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;

using ProfileMatch.Models.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNetCore.Identity;

namespace ProfileMatch.Services
{
    public class Redirection : IRedirection
    {
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly UserManager<ApplicationUser> userManager;
        readonly NavigationManager nav;
        public Redirection(AuthenticationStateProvider authenticationStateProvider, NavigationManager nav, UserManager<ApplicationUser> userManager)
        {
            _authenticationStateProvider = authenticationStateProvider;
            this.nav = nav;
            this.userManager = userManager;
        }
        public ApplicationUser AppUser { get; set; } = new();

        public async Task<ApplicationUser> GetUser()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (authState?.User?.Identity is null || !authState.User.Identity.IsAuthenticated)
            {
                var returnUrl = nav.ToBaseRelativePath(nav.Uri);

                if (string.IsNullOrWhiteSpace(returnUrl))
                    nav.NavigateTo("Identity/Account/Login", true);
                else
                    nav.NavigateTo($"auth/login?returnUrl={returnUrl}", true);
                return new();
            }
            else
            {

                return await userManager.GetUserAsync(user);
            }
        }
    }
}