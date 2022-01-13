using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;

using MudBlazor;

using ProfileMatch.Components.Dialogs;
using ProfileMatch.Data;
using ProfileMatch.Models.Models;
using ProfileMatch.Repositories;
using ProfileMatch.Services;
using System.Collections.Generic;

namespace ProfileMatch.Components.User
{
    public partial class UserCertList : ComponentBase
    {
        [Inject] private IDialogService DialogService { get; set; }
        [Inject] DataManager<ApplicationUser, ApplicationDbContext> ApplicationUserRepository { get; set; }
        [Inject] DataManager<Certificate, ApplicationDbContext> CertificateRepository { get; set; }

        List<Certificate> certificates;
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
            DialogService.Show<UserCertDialog>("fill in the fields");
        }

        private void DelCert()
        {
            DialogService.Show<UserCertDialog>("Are you sure you want to delete the certificate?");
        }

        [Inject]        private IStringLocalizer<LanguageService> L { get; set; }
    }
}