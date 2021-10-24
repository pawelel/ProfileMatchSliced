using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

using ProfileMatch.Services;

namespace ProfileMatch.Sites.Manager
{
    public partial class ManagerDashboard : ComponentBase
    {
        [Inject]
        IStringLocalizer<LanguageService> L { get; set; }
    }
}