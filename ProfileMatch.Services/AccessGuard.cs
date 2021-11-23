using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using ProfileMatch.Data;
using ProfileMatch.Models.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProfileMatch.Services
{
    public class AccessGuard
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccessGuard(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        public async Task<bool> IsOwnerOrHaveRightsAsync(UserManager<ApplicationUser> userManager, ApplicationUser claimant, ClaimsPrincipal User)
        {
            var loggedInUserId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            ApplicationUser fullUser = await _context.Users.AsNoTracking()
                                      .FirstOrDefaultAsync(au => au.Id == loggedInUserId);
            if (claimant.Id == fullUser.Id)
            {
                return true;
            }

            return false;
        }
    }
}
