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
using ProfileMatch.Models.Responses;

namespace ProfileMatch.Sites.Admin
{
    public partial class EditUser : ComponentBase
    {

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
        ServiceResponse<ApplicationUserVM> loadedUserResponse = new();
        ApplicationUserVM EditUserVM { get; set; } = new();
        private IEnumerable<DepartmentVM> Departments = new List<DepartmentVM>();

        protected override async Task OnInitializedAsync()
        {
            await LoadData();
            EditUserVM = loadedUserResponse.Data;
        }

        private async Task LoadData()
        {

            loadedUserResponse = await UserService.FindSingleByIdAsync(Id);
            if (loadedUserResponse.Data == null)
            {
                loadedUserResponse.Data = new()
                {
                    DepartmentId = 1,
                    DateOfBirth = DateTime.Now,
                    PhotoPath = "images/nophoto.jpg"
                };
            }


        }

        protected async Task HandleSave()
        {
            await Form.Validate();
            if (Form.IsValid)
            {
                await LoadData();


                UserService.Update(EditUserVM);

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
