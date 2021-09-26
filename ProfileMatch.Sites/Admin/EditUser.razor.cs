using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProfileMatch.Models.Enumerations;
using AutoMapper;
using FluentValidation;
using ProfileMatch.Validations;
using Microsoft.AspNetCore.Components;

using MudBlazor;

using ProfileMatch.Contracts;

using ProfileMatch.Models.Models;
using ProfileMatch.Models.ViewModels;

namespace ProfileMatch.Sites.Admin
{
    public partial class EditUser : ComponentBase
    {
        [Parameter] public EventCallback OnSaveCallBack { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        public IMapper Mapper { get; set; }

        [Inject]
        public IUserService UserService { get; set; }

        [Inject]
        public IDepartmentService DepartmentService { get; set; }

        [Parameter] public string Id { get; set; }
        protected MudForm Form { get; set; } // TODO add validations
        private bool _success;

        private DateTime? _dob;
        private IEnumerable<Department> Departments = new List<Department>();

        protected override async Task OnInitializedAsync()
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            if (await UserService.Exist(Id))
            {
                EditUserVM = await UserService.FindSingleByIdAsync(Id);
            }
            else
            {
                EditUserVM = new()
                {
                    DepartmentId = 1,
                    DateOfBirth = DateTime.Now,
                    PhotoPath = "images/nophoto.jpg"
                };
            }

            Departments = (await DepartmentService.FindAllAsync()).ToList();

            _dob = EditUserVM.DateOfBirth;
            StateHasChanged();
        }

        public EditUserVM EditUserVM { get; set; } = new();

        protected async Task HandleSave()
        {
            await Form.Validate();
            if (Form.IsValid)
            {
                var exist = await UserService.Exist(Id);
                if (!exist)
                {
                    await UserService.Update(EditUserVM);
                }
                NavigationManager.NavigateTo("/admin/dashboard");
                await Refresh();
            }
        }
        private async Task Refresh()
        {
            await LoadData();
        }
    }
}
