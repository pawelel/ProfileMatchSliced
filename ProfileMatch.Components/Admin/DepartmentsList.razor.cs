using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;

using ProfileMatch.Contracts;
using ProfileMatch.Models.ViewModels;

namespace ProfileMatch.Components.Admin
{
    public partial class DepartmentsList : ComponentBase
    {
        [Inject]
        public IDepartmentService DepartmentService { get; set; }
        [Parameter] public int id { get; set; }

        private IEnumerable<DepartmentVM> Departments;
        protected override async Task OnInitializedAsync()
        {
            Departments = await DepartmentService.GetDepartmentsWithPeople();
        }



    }
}
