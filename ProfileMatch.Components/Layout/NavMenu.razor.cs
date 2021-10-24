using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

using ProfileMatch.Models.Models;
using ProfileMatch.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileMatch.Components.Layout
{
    public partial class NavMenu : ComponentBase
    {
        [CascadingParameter] ApplicationUser CurrentUser {get;set;}
        [Inject]
        IStringLocalizer<LanguageService> L { get; set; }
    }
}
