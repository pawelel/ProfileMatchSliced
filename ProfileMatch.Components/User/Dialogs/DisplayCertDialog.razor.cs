using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Components;

using MudBlazor;

using ProfileMatch.Models.Entities;

namespace ProfileMatch.Components.User.Dialogs
{
    public partial class DisplayCertDialog : ComponentBase
    {
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        void Cancel() => MudDialog.Cancel();
        [Parameter] public Certificate Cert { get; set; }
    }
}
