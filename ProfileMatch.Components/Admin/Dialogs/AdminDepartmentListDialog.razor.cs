using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;

using ProfileMatch.Models.Entities;

namespace ProfileMatch.Components.Admin.Dialogs
{
    public partial class AdminDepartmentListDialog : ComponentBase
    {
        bool _loading;
        private IEnumerable<Department> Departments = new List<Department>();
        private string _searchString;
        protected override async Task OnInitializedAsync()
        {

        }
        // quick filter - filter gobally across multiple columns with the same input
        private Func<Department, bool> _quickFilter => x =>
        {
            if (string.IsNullOrWhiteSpace(_searchString))
                return true;

            if (x.Name.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            if (x.NamePl.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (x.Description.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (x.DescriptionPl.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            return false;
        };


    }
}
