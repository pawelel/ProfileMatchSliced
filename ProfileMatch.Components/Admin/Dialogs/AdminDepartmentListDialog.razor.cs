using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;

using MudBlazor;

using ProfileMatch.Models.Entities;
using ProfileMatch.Models.ViewModels;
using ProfileMatch.Repositories;

namespace ProfileMatch.Components.Admin.Dialogs
{
    public partial class AdminDepartmentListDialog : ComponentBase
    {
        bool _loading;
        [Inject] IUnitOfWork UnitOfWork { get; set; }
        [Inject] private IDialogService DialogService { get; set; }
        private IEnumerable<Department> _departments = new List<Department>();
        private string _searchString;
        protected override async Task OnInitializedAsync()
        {
            _loading = true;
            _departments = await UnitOfWork.Departments.Get();
            _loading= false;
        }
        // quick filter - filter gobally across multiple columns with the same input
        private Func<Department, bool> QuickFilter => x =>
        {
            if (string.IsNullOrWhiteSpace(_searchString))
                return true;

            if (x.Name.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            if (x.NamePl.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            return false;
        };
        private async Task DepartmentUpdate(Department department=null)
        {
            if (department == null)
            {
                var dialog = DialogService.Show<AdminDepartmentDialog>(L["Create Department"]);
                await dialog.Result;
            }
            else
            {
                var parameters = new DialogParameters { ["DepartmentId"] = department.Id };
                var dialog = DialogService.Show<AdminDepartmentDialog>(L["Edit Department"], parameters);
                await dialog.Result;
            }

        }

    }
}
