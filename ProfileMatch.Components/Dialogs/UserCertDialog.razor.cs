using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;

using MudBlazor;

using ProfileMatch.Services;

namespace ProfileMatch.Components.Dialogs
{
    public partial class UserCertDialog
    {

        [Inject] IStringLocalizer<LanguageService> L { get; set; }
        [Inject] private IDialogService DialogService { get; set; }


        private readonly IList<IBrowserFile> files = new List<IBrowserFile>();

        private void UploadFiles(InputFileChangeEventArgs e)
        {
            foreach (var file in e.GetMultipleFiles())
            {
                files.Add(file);
            }
            //TODO upload the files to the server
        }
    }
}
