using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;

using MudBlazor;
using MudBlazor.Examples.Data.Models;

using ProfileMatch.Components.Dialogs;
using ProfileMatch.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileMatch.Components.User
{
   public partial class UserCertList :ComponentBase
    {
        [Inject]
        IDialogService DialogService { get; set; }
        private bool hover = true;
        private HashSet<Element> selectedItems1 = new();
        private HashSet<Element> selectedItems2 = new();

        private IEnumerable<Element> Elements = new List<Element>();


        IList<IBrowserFile> files = new List<IBrowserFile>();
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
        IStringLocalizer<LanguageService> L { get; set; }
    }
}
