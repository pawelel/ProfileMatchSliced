using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Components;

namespace ProfileMatch.Components.Layout
{
    public partial class NoContent : ComponentBase
    {
        [Inject]
        NavigationManager NavigationManager { get; set; }
        public void GoHome()
        {
            NavigationManager.NavigateTo("");
        }
    }
}
