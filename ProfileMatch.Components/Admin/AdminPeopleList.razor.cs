using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

using MudBlazor;
using MudBlazor.Extensions;

using ProfileMatch.Components.Admin.Dialogs;
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
        [Inject] DataManager<IdentityRole, ApplicationDbContext> IdentityRoleRepository { get; set; }
        [Inject] DataManager<IdentityUserRole<string>, ApplicationDbContext> IdentityUserRoleRepository { get; set; }
        [Inject] DataManager<Department, ApplicationDbContext> DepartmentRepository { get; set; }
        [Inject] DataManager<JobTitle, ApplicationDbContext> JobTitleRepository { get; set; }
        string searchString;
        List<IdentityRole> roles;
        List<JobTitle> jobTitles=new();
        List<IdentityUserRole<string>> userIdentityRoles;
        List<DepartmentUserVM> users;
        protected override async Task OnInitializedAsync()
        {
            jobTitles = await JobTitleRepository.Get();
            userIdentityRoles = await IdentityUserRoleRepository.Get();
            roles = await IdentityRoleRepository.Get();
            users = await GetDepartmentsAsync();
        }

        private async Task EditProfile(DepartmentUserVM applicationUser = null)
        {
            if (applicationUser == null) applicationUser = new()
            {
                FirstName = L["New User"],
                JobTitleId = 1
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

        
        private async Task DepartmentUpdate(DepartmentUserVM department = null)
        {
            if (department == null)
            {
                var dialog = DialogService.Show<AdminDepartmentDialog>(L["Create Department"]);
                await dialog.Result;
            }
            else
            {
                var parameters = new DialogParameters { ["Dep"] = department };
                var dialog = DialogService.Show<AdminDepartmentDialog>(L["Edit Department"], parameters);
                await dialog.Result;
            }

        }
        private async Task JobTitleUpdate(DepartmentUserVM jobTitle = null)
        {
            if (jobTitle == null)
            {
                var dialog = DialogService.Show<AdminJobTitleDialog>(L["Create Job Title"]);
                await dialog.Result;
            }
            else
            {
                var parameters = new DialogParameters { ["JobTitle"] = jobTitle };
                var dialog = DialogService.Show<AdminJobTitleDialog>(L["Edit Job Title"], parameters);
                await dialog.Result;
            }

        }

        private async Task<List<DepartmentUserVM>> GetDepartmentsAsync()
        {
            var appUsers = await ApplicationUserRepository.Get();
            var depts = await DepartmentRepository.Get();
            var appDepts = (from dept in depts
                            join appUser in appUsers on dept.Id equals appUser.DepartmentId join jt in jobTitles on appUser.JobTitleId equals jt.Id join ur in userIdentityRoles on appUser.Id equals ur.UserId join r in roles on ur.RoleId equals r.Id
                            select new DepartmentUserVM()
                            {
                                DepartmentId = dept.Id,
                                DepartmentName = dept.Name,
                                DepartmentNamePl = dept.NamePl,
                                FirstName = appUser.FirstName,
                                LastName = appUser.LastName,
                                UserId = appUser.Id,
                                JobTitleId = appUser.JobTitleId,
                                PhotoPath = appUser.PhotoPath,
                                JobTitleNamePl = jt.NamePl,
                                JobTitleName = jt.Name,
                                IsActive = appUser.IsActive,
                                UserRolesVM = new List<UserRoleVM>() { new()
                                {
                                    RoleId = ur.RoleId,
                                    UserId = appUser.Id,
                                    RoleName = r.Name,
                                    IsSelected = true
                                } }
                            }).ToList();
            return appDepts;
        }
    }
}