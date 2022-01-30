using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;

using ProfileMatch.Models.Entities;

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
        private readonly UserManager<ApplicationUser> _userManager;
        readonly NavigationManager _nav;
        public Redirection(AuthenticationStateProvider authenticationStateProvider, NavigationManager nav, UserManager<ApplicationUser> userManager)
        {
            _authenticationStateProvider = authenticationStateProvider;
            this._nav = nav;
            this._userManager = userManager;
        }
        public ApplicationUser AppUser { get; set; } = new();

        public async Task<ApplicationUser> GetUser()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (authState?.User?.Identity is null || !authState.User.Identity.IsAuthenticated)
            {
                var returnUrl = _nav.ToBaseRelativePath(_nav.Uri);
                if (string.IsNullOrWhiteSpace(returnUrl))
                    _nav.NavigateTo("Identity/Account/Login", true);
                
                return new();
            }
            else
            {

                return await _userManager.GetUserAsync(user);
            }
        }
    }
}