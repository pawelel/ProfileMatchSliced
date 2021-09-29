
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;

using ProfileMatch.Contracts;
using ProfileMatch.Models.Models;

namespace ProfileMatch.Components.Admin
{
    public partial class EditDepartment : ComponentBase
    {
        [Inject]
        public IDepartmentService departmentService { get; set; }

        Department department = new();
        List<Department> Departments = new();

        protected override async Task OnInitializedAsync()
        {
            Departments = await departmentService.FindAllAsync();
        }


    }
}
