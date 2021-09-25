using Microsoft.AspNetCore.Components;

using ProfileMatch.Services;

using System;

namespace ProfileMatch.Components.Culture
{
    public partial class CultureSelector
    {
        [Inject]
        private NavigationManager Nav { get; set; }

        private void SelectCulture(string language)
        {
            string redirectUri = new Uri(Nav.Uri).GetComponents(
                components: UriComponents.PathAndQuery,
                format: UriFormat.Unescaped
            );

            UriQueryBuilder queryBuilder = new();
            queryBuilder.AppendParameter("culture", language);
            queryBuilder.AppendParameter("redirectUri", redirectUri);

            string query = queryBuilder.ToString();
            Nav.NavigateTo($"/culture/set{query}", forceLoad: true);
        }
    }
}
