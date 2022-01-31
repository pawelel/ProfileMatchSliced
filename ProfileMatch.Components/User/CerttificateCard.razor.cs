using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;

using MudBlazor;

using ProfileMatch.Components.User.Dialogs;
using ProfileMatch.Models.Entities;

namespace ProfileMatch.Components.User
{
    public partial class CerttificateCard : ComponentBase
    {
        [Inject] IDialogService Dialog { get; set; }
        [Parameter] public  Certificate Cert { get; set; } = new Certificate();

        private async Task CertUpdate()
        {
            var parameters = new DialogParameters { ["OpenCertificate"] = Cert };
                var dialog = Dialog.Show<UserCertDialog>(L["Edit Certificate"], parameters);
                await dialog.Result;
        }
  
       void DisplayCert()
        {
            var parameters = new DialogParameters {["Cert"]=Cert };
            DialogOptions options = new() { MaxWidth = MaxWidth.Medium, CloseOnEscapeKey=true, DisableBackdropClick = false, Position = DialogPosition.Center};
            Dialog.Show<DisplayCertDialog>(Cert.Name, parameters, options);
        }
    }
}
