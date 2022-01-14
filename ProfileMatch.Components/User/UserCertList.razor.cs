using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;

using MudBlazor;

using ProfileMatch.Components.Dialogs;
using ProfileMatch.Data;
using ProfileMatch.Models.Models;
using ProfileMatch.Repositories;
using ProfileMatch.Services;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProfileMatch.Components.User
{
    public partial class UserCertList : ComponentBase
    {
        [Inject] private IDialogService DialogService { get; set; }
        [Inject] IRedirection Redirection { get; set; }
        [Inject] DataManager<Certificate, ApplicationDbContext> CertificateRepository { get; set; }
        [CascadingParameter] public ApplicationUser CurrentUser    { get; set; }

        List<Certificate> certificates=new();

        protected override async Task OnInitializedAsync()
        {
            if (CurrentUser == null)
            {
                CurrentUser = await Redirection.GetUser();
            }
            certificates = await CertificateRepository.Get();
        }
        private void UpdateCert()
        {
            DialogService.Show<UserCertDialog>("fill in the fields");
        }

       

        [Inject] private IStringLocalizer<LanguageService> L { get; set; }
    }
}