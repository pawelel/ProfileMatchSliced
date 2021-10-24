using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

using ProfileMatch.Models.Models;
using ProfileMatch.Services;

namespace ProfileMatch.Sites.User
{
    public partial class UserDashboard : ComponentBase
    {
        
        [Inject]
        IStringLocalizer<LanguageService> L { get; set; }
    }
}