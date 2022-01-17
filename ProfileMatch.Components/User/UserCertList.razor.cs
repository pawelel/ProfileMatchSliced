using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;

using MudBlazor;

using ProfileMatch.Components.User.Dialogs;
using ProfileMatch.Data;
using ProfileMatch.Models.Models;
using ProfileMatch.Models.ViewModels;
using ProfileMatch.Repositories;
using ProfileMatch.Services;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProfileMatch.Components.User
{
    public partial class UserCertList : ComponentBase
    {
        [Inject] private IDialogService DialogService { get; set; }
        [Inject] IRedirection Redirection { get; set; }
        [Inject] DataManager<Certificate, ApplicationDbContext> CertificateRepository { get; set; }
        [CascadingParameter] public ApplicationUser CurrentUser { get; set; }

        List<Certificate> certificates = new();
        string searchString;
        protected override async Task OnInitializedAsync()
        {
            if (CurrentUser == null)
            {
                CurrentUser = await Redirection.GetUser();
            }
            certificates = await CertificateRepository.Get();
        }
       
          void ShowPdf(string certificate)
        {

        }
        private async Task CertUpdate(Certificate certicicate = null)
        {
                var parameters = new DialogParameters { ["OpenCertificate"] = certicicate, ["CurrentUser"]= CurrentUser };

            if (certicicate == null)
            {
                var dialog = DialogService.Show<UserCertDialog>(L["Add Certificate"]);
                await dialog.Result;
            }
            else
            {
                var dialog = DialogService.Show<UserCertDialog>(L["Edit Certificate"], parameters);
                await dialog.Result;
            }

        }

        private Func<Certificate, bool> QuickFilter => cert =>
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (cert.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (cert.DescriptionPl.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (cert.Description.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            return false;
        };

    }
}