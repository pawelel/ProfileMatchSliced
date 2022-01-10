using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

using MudBlazor;

using ProfileMatch.Components.Dialogs;
using ProfileMatch.Contracts;
using ProfileMatch.Data;
using ProfileMatch.Models.Models;
using ProfileMatch.Models.ViewModels;
using ProfileMatch.Repositories;
using ProfileMatch.Services;

using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProfileMatch.Components.Admin
{
    public partial class AdminPeopleList : ComponentBase
    {
        [Inject]
        private IDialogService DialogService { get; set; }

#pragma warning disable IDE0052 // Remove unread private members
        private ClaimsPrincipal user; //todo alternative blockade of self-edit
#pragma warning restore IDE0052 // Remove unread private members

        [Inject] DataManager<ApplicationUser, ApplicationDbContext> UserRepository { get; set; }

        string searchString;


        [CascadingParameter] private Task<AuthenticationState> AuthSP { get; set; }
       

        public List<ApplicationUser> Users { get; set; }

        protected override async Task OnInitializedAsync()
        {
            user = (await AuthSP).User;
            Users = await UserRepository.Get(include: src => src.Include(u => u.Department));
        }

        [Inject] private IStringLocalizer<LanguageService> L { get; set; }
        private async Task EditProfile(ApplicationUser applicationUser)
        {
            DialogOptions maxWidth = new() { MaxWidth = MaxWidth.Large, FullWidth = true };
            var parameters = new DialogParameters { ["EditedUser"] = applicationUser };
            var dialog = DialogService.Show<AdminUserDialog>(L.GetString("Account") + $": {applicationUser.FirstName} {applicationUser.LastName}", parameters, maxWidth);
            await dialog.Result;
            Users = await UserRepository.Get();
        }

        [Inject] NavigationManager NavigationManager { get; set; }
        void ShowProfile(ApplicationUser applicationUser)
        {
            NavigationManager.NavigateTo($"user/{applicationUser.Id}");
        }

        private Func<ApplicationUser, bool> QuickFilter => person =>
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
          
            if (person.FirstName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (person.LastName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (ShareResource.IsEn())
            {
                if (person.Department.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            else
            {
                if (person.Department.NamePl.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        };




    }
}