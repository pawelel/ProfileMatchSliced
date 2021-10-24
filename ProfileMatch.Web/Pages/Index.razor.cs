using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

using ProfileMatch.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileMatch.Web.Pages
{
    public partial class Index : ComponentBase
    {
        [Inject]
        IStringLocalizer<LanguageService> L { get; set; }
    }
}
