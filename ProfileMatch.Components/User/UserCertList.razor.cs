using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;

using MudBlazor;

using ProfileMatch.Components.Dialogs;
using ProfileMatch.Services;

using System.Collections.Generic;

namespace ProfileMatch.Components.User
{
    public partial class UserCertList : ComponentBase
    {
        [Inject]
        private IDialogService DialogService { get; set; }

        
        private readonly IList<IBrowserFile> files = new List<IBrowserFile>();

        private void UploadFiles(InputFileChangeEventArgs e)
        {
            foreach (var file in e.GetMultipleFiles())
            {
                files.Add(file);
            }
            //TODO upload the files to the server
        }

        private void AddCert()
        {
            DialogService.Show<UserAddCertDialog>("fill in the fields");
        }

        private void DelCert()
        {
            DialogService.Show<UserDelCertDialog>("Are you sure you want to delete the certificate?");
        }

        [Inject]
        private IStringLocalizer<LanguageService> L { get; set; }
    }
}