using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;

using MudBlazor;

using ProfileMatch.Components.User.Dialogs;
using ProfileMatch.Data;
using ProfileMatch.Models.Entities;
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
        [Inject] IUnitOfWork UnitOfWork { get; set; }
        [CascadingParameter] public ApplicationUser CurrentUser { get; set; }

        List<Certificate> _certificates = new();
        string _searchString;
        protected override async Task OnInitializedAsync()
        {
            if (CurrentUser == null)
            {
                CurrentUser = await Redirection.GetUser();
            }
            _certificates = await UnitOfWork.Certificates.Get();
        }
       
        
        private async Task CertUpdate()
        {
                var parameters = new DialogParameters { ["UserId"]= CurrentUser.Id };
                var dialog = DialogService.Show<UserCertDialog>(L["Add Certificate", parameters]);
                await dialog.Result;
            _certificates = await UnitOfWork.Certificates.Get();
        }
        
        private bool IsVisible(Certificate cert)
        {
            if (string.IsNullOrWhiteSpace(_searchString))
                return true;
            if (cert.Name.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (cert.DescriptionPl.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (cert.Description.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            return false;
        }

    }
}