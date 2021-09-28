using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;

using ProfileMatch.Contracts;


namespace ProfileMatch.Components.Admin
{
    public partial class DepartmentsList : ComponentBase
    {
        [Inject]
        public IDepartmentService DepartmentService { get; set; }
        [Parameter] public int id { get; set; }

        private IEnumerable<Department> Departments;
        protected override async Task OnInitializedAsync()
        {
            Departments = await DepartmentService.GetDepartmentsWithPeople();
        }



    }
}
