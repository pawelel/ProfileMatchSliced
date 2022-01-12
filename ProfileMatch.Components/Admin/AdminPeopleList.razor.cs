﻿using Microsoft.AspNetCore.Components;
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
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProfileMatch.Components.Admin
{
    public partial class AdminPeopleList : ComponentBase
    {
        [Inject] private IDialogService DialogService { get; set; }
        [Inject] DataManager<ApplicationUser, ApplicationDbContext> ApplicationUserRepository { get; set; }
        [Inject] DataManager<Department, ApplicationDbContext> DepartmentRepository { get; set; }
        string searchString;
        public List<DepartmentUserVM> Users { get; set; }
        protected override async Task OnInitializedAsync()
        {

            Users = await GetDepartmentsAsync();
        }

        [Inject] private IStringLocalizer<LanguageService> L { get; set; }
        private async Task EditProfile(DepartmentUserVM applicationUser = null)
        {
            if (applicationUser == null) applicationUser = new()
            {
                FirstName = L["New User"]
            };

            DialogOptions maxWidth = new() { MaxWidth = MaxWidth.Large, FullWidth = true };
            var parameters = new DialogParameters { ["OpenedUser"] = applicationUser };
            var dialog = DialogService.Show<AdminUserDialog>(L.GetString("Account") + $": {applicationUser.FirstName} {applicationUser.LastName}", parameters, maxWidth);
            await dialog.Result;
        }

        [Inject] NavigationManager NavigationManager { get; set; }
        void ShowProfile(DepartmentUserVM applicationUser)
        {
            NavigationManager.NavigateTo($"user/{applicationUser.UserId}");
        }

        private Func<DepartmentUserVM, bool> QuickFilter => person =>
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;

            if (person.FirstName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (person.LastName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (ShareResource.IsEn())
            {
                if (person.DepartmentName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            else
            {
                if (person.DepartmentNamePl.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        };


        private async Task DepartmentUpdate(DepartmentUserVM department)
        {
            var parameters = new DialogParameters { ["Dep"] = department };
            var dialog = DialogService.Show<AdminDepartmentDialog>(L["Edit Department"], parameters);
            await dialog.Result;
        }

        private async Task DepartmentCreate()
        {
            var dialog = DialogService.Show<AdminDepartmentDialog>(L["Create Department"]);
            await dialog.Result;
        }
        private async Task<List<DepartmentUserVM>> GetDepartmentsAsync()
        {
            var appUsers = await ApplicationUserRepository.Get();
            var depts = await DepartmentRepository.Get();
            return (from dept in depts
                    join appUser in appUsers on dept.Id equals appUser.DepartmentId
                    select new DepartmentUserVM()
                    {
                        DepartmentId = dept.Id,
                        DepartmentName = dept.Name,
                        DepartmentNamePl = dept.NamePl,
                        FirstName = appUser.FirstName,
                        JobTitle = appUser.JobTitle,
                        JobTitlePl = appUser.JobTitlePl,
                        LastName = appUser.LastName,
                        UserId = appUser.Id,
                        PhotoPath = appUser.PhotoPath,
                        IsActive = appUser.IsActive
                    }).ToList();
        }
    }
}