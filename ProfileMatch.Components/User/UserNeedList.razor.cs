﻿using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

using ProfileMatch.Services;

namespace ProfileMatch.Components.User
{
    public partial class UserNeedList : ComponentBase
    {
        [Inject]
        IStringLocalizer<LanguageService> L { get; set; }
    }
}