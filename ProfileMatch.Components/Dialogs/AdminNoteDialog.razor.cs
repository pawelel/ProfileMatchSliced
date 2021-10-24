using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

using ProfileMatch.Services;

namespace ProfileMatch.Components.Dialogs
{
    public partial class AdminNoteDialog : ComponentBase
    {
        [Inject]
        IStringLocalizer<LanguageService> L { get; set; }
    }
}