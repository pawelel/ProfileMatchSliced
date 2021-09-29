using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;

using MudBlazor;

using ProfileMatch.Contracts;
using ProfileMatch.Models.Models;
using ProfileMatch.Services;

namespace ProfileMatch.Sites.Admin
{
    public partial class EditDepartment : ComponentBase
    {
        MudForm Form;
        bool _success;
        [Parameter] public string Id { get; set; }
        Department Dep = new();
        [Inject]
        NavigationManager NavigationManager { get; set; }
        [Inject]
        public IDepartmentService DepartmentService { get; set; }

        private async Task<Department> GetDepartment()
        {
           if( int.TryParse(Id, out int id))
            {
                Dep = await DepartmentService.GetDepartment(id);
            }
            else
            {
                Dep = new();
            }
            return Dep;
        }

        protected override async Task OnInitializedAsync()
        {
            Dep = await GetDepartment();
        }
        protected async Task HandleSave()
        {
            await Form.Validate();
            if (Form.IsValid)
            {
                if (await DepartmentService.Exist(Dep.Id))
                {

                    await DepartmentService.Update(Dep);
                }
                else
                {
                    await DepartmentService.Create(Dep);
                }

                NavigationManager.NavigateTo("/admin/dashboard");
            }
        }
    }
}
