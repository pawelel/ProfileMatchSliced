using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

using MudBlazor;
using MudBlazor.Extensions;

using ProfileMatch.Components.Admin.Dialogs;
using ProfileMatch.Data;
using ProfileMatch.Models.Entities;
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
        [Inject] IUnitOfWork UnitOfWork { get; set; }
        string _searchString;
        List<IdentityRole> _roles;
        List<Job> _jobs=new();
        List<IdentityUserRole<string>> _userIdentityRoles;
        List<DepartmentUserVM> _users;
        protected override async Task OnInitializedAsync()
        {
            _jobs = await UnitOfWork.Jobs.Get();
            _userIdentityRoles = await UnitOfWork.IdentityUserRoles.Get();
            _roles = await UnitOfWork.IdentityRoles.Get();
            _users = await GetDepartmentsAsync();
        }

        private async Task EditProfile(DepartmentUserVM applicationUser = null)
        {
            if (applicationUser == null) applicationUser = new()
            {
                FirstName = L["New User"],
                JobId = 1
            };

            DialogOptions maxWidth = new() { MaxWidth = MaxWidth.Large, FullWidth = true };
            var parameters = new DialogParameters { ["UserId"] = applicationUser.UserId };

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
            if (string.IsNullOrWhiteSpace(_searchString))
                return true;

            if (person.FirstName.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (person.LastName.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (ShareResource.IsEn())
            {
                if (person.DepartmentName.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            else
            {
                if (person.DepartmentNamePl.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
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
        private async Task JobUpdate(DepartmentUserVM job = null)
        {
            if (job == null)
            {
                var dialog = DialogService.Show<AdminJobDialog>(L["Create Job Title"]);
                await dialog.Result;
            }
            else
            {
                var parameters = new DialogParameters { ["Job"] = job };
                var dialog = DialogService.Show<AdminJobDialog>(L["Edit Job Title"], parameters);
                await dialog.Result;
            }

        }

        private async Task<List<DepartmentUserVM>> GetDepartmentsAsync()
        {
            var appUsers = await UnitOfWork.ApplicationUsers.Get();
            var depts = await UnitOfWork.Departments.Get();
            var appDepts = (from dept in depts
                            join appUser in appUsers on dept.Id equals appUser.DepartmentId join jt in _jobs on appUser.JobId equals jt.Id
                            select new DepartmentUserVM()
                            {
                                DepartmentId = dept.Id,
                                DepartmentName = dept.Name,
                                DepartmentNamePl = dept.NamePl,
                                FirstName = appUser.FirstName,
                                LastName = appUser.LastName,
                                UserId = appUser.Id,
                                JobId = appUser.JobId,
                                PhotoPath = appUser.PhotoPath,
                                JobNamePl = jt.NamePl,
                                JobName = jt.Name,
                                IsActive = appUser.IsActive,
                                UserRolesVM = (from userRole in _userIdentityRoles where userRole.UserId==appUser.Id join role in _roles on userRole.RoleId equals role.Id select new UserRoleVM()
                                {
                                    RoleId = role.Id,
                                    RoleName = role.Name,
                                    UserId = userRole.UserId
                                }).ToList(),
                            }).ToList();
            return appDepts;
        }

        void DepartmentList()
        {
            DialogService.Show<AdminDepartmentListDialog>();
        }

        void JobList()
        {
            DialogService.Show<AdminJobListDialog>();
        }
    }
}