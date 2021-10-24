using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

using ProfileMatch.Services;

namespace ProfileMatch.Components.Layout
{
    public partial class NoContent : ComponentBase
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; }

        public void GoHome()
        {
            NavigationManager.NavigateTo("");
        }

        [Inject]
        private IStringLocalizer<LanguageService> L { get; set; }
    }
}