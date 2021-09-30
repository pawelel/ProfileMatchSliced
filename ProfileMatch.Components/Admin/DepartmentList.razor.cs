using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;

using MudBlazor;

using ProfileMatch.Components.Dialogs;
using ProfileMatch.Contracts;
using ProfileMatch.Models.Models;

namespace ProfileMatch.Components.Admin
{
    public partial class DepartmentList : ComponentBase
    {
        [Inject]
        IDialogService DialogService { get; set; }
        [Inject]
        public IDepartmentService DepartmentService { get; set; }

        private async Task<IEnumerable<Department>> GetDepartmentsAsync()
        {
            return await DepartmentService.GetDepartmentsWithPeople();
        }

        protected override async Task OnInitializedAsync()
        {
            Departments = await GetDepartmentsAsync();
        }

        private bool dense = false;
        private bool hover = true;
        private bool striped = false;
        private bool bordered = false;
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
        async Task DepartmentDialog(Department department)
        {
            var parameters = new DialogParameters { ["Dep"] = department };
           var dialog = DialogService.Show<EditDepartmentDialog>("Edit Department", parameters);
            await dialog.Result;
        }
        async Task DepartmentDialog()
        {
            var dialog = DialogService.Show<EditDepartmentDialog>("Create Department");
            await dialog.Result;
            Departments = await GetDepartmentsAsync();
        }
    }
}