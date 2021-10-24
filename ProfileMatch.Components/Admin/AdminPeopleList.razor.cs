using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

using MudBlazor;

using ProfileMatch.Components.Dialogs;
using ProfileMatch.Contracts;
using ProfileMatch.Models.Models;
using ProfileMatch.Services;

namespace ProfileMatch.Components.Admin
{
    public partial class AdminPeopleList : ComponentBase
    {
        [Inject]
        private IDialogService DialogService { get; set; }

        [Inject]
        public IUserRepository UserRepo { get; set; }

        public string SearchString { get; set; }

        private bool _dense = false;

        private bool FilterFunc1(ApplicationUser person) => FilterFunc(person, SearchString);

        private static bool FilterFunc(ApplicationUser person, string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (person.Department.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (person.FirstName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (person.LastName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if ($"{person.DateOfBirth} {person.IsActive}".Contains(searchString))
                return true;
            return false;
        }

        public List<ApplicationUser> Users { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Users = await UserRepo.GetAll();
        }

        private async Task EditProfile(ApplicationUser applicationUser)
        {
            DialogOptions maxWidth = new() { MaxWidth = MaxWidth.Large, FullWidth = true };
            var parameters = new DialogParameters { ["EditedUser"] = applicationUser };
            var dialog = DialogService.Show<AdminUserDialog>($"Edit User {applicationUser.FirstName}, {applicationUser.LastName} data", parameters, maxWidth);
            await dialog.Result;
        }
        [Inject]
        IStringLocalizer<LanguageService> L { get; set; }
    }
}