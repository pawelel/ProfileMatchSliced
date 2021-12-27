using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

using MudBlazor;

using ProfileMatch.Components.Dialogs;
using ProfileMatch.Contracts;
using ProfileMatch.Data;
using ProfileMatch.Models.Models;
using ProfileMatch.Repositories;
using ProfileMatch.Services;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProfileMatch.Components.Admin
{
    public partial class AdminDepartmentList : ComponentBase
    {
        [Inject]
        public ISnackbar Snackbar { get; set; }

        [Inject]
        private IDialogService DialogService { get; set; }

        [Inject] DataManager<Department, ApplicationDbContext> DepartmentRepository { get; set; }
      

        private async Task<IEnumerable<Department>> GetDepartmentsAsync()
        {
            return await DepartmentRepository.Get(include:src=>src.Include(d=>d.ApplicationUsers));
        }
        protected override async Task OnInitializedAsync()
        {
            Departments = await GetDepartmentsAsync();
        }

        
        
        
        
        private string searchString1 = "";
        private Department selectedItem1 = null;
        private IEnumerable<Department> Departments = new List<Department>();

        private bool FilterFunc1(Department department) => FilterFunc(department, searchString1);

        private static bool FilterFunc(Department department, string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (department.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (department.Description != null && department.Description.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            return false;
        }

        private async Task DepartmentUpdate(Department department)
        {
            var parameters = new DialogParameters { ["Dep"] = department };
            var dialog = DialogService.Show<AdminDepartmentDialog>("Update Department", parameters);
            await dialog.Result;
        }

        private async Task DepartmentCreate()
        {
            var dialog = DialogService.Show<AdminDepartmentDialog>("Create Department");
            await dialog.Result;
            Departments = await GetDepartmentsAsync();
        }

        [Inject]
        private IStringLocalizer<LanguageService> L { get; set; }
    }
}