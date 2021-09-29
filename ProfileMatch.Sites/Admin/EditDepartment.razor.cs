using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;

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
        public IDepartmentService DepartmentService { get; set; }
        private async Task<IEnumerable<Department>> GetDepartmentsAsync()
        {
            return await DepartmentService.GetDepartmentsWithPeople();
        }
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
            Departments = await GetDepartmentsAsync();
        }
        protected async Task HandleSave()
        {
            await Form.Validate();
            if (Form.IsValid)
            {
                if (await DepartmentService.Exist(Dep.Id))
                {

                    await DepartmentService.Update(Dep);
                    await DepartmentService.GetDepartmentsWithPeople();
                }
                else
                {
                    await DepartmentService.Create(Dep);
                    await DepartmentService.GetDepartmentsWithPeople();
                    await   InvokeAsync(() =>
                    {

                        StateHasChanged();
                    });
                }
                //NavigationManager.NavigateTo("/admin/dashboard");
            }
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
            return false;
        }
    }
}
