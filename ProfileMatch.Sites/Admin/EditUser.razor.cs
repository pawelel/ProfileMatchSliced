using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using Microsoft.AspNetCore.Components;

using MudBlazor;

using ProfileMatch.Models;
using ProfileMatch.Models.Models;
using ProfileMatch.Models.ViewModels;
using ProfileMatch.Services;


namespace ProfileMatch.Pages.Admin.Pages
{
    public partial class AdminEditUserPage : ComponentBase
    {
        ApplicationUser result = null;
        [Parameter] public EventCallback OnSaveCallBack { get; set; }

        [Inject]
        NavigationManager NavigationManager { get; set; }

        [Inject]
        public IMapper Mapper { get; set; }


        [Parameter] public string Id { get; set; }
        protected MudForm Form { get; set; } // TODO add validations
        private bool _success;
        private ApplicationUser User { get; set; } = new();
        private DateTime? _dob;
        private IEnumerable<Department> Departments = new List<Department>();

        protected override async Task OnInitializedAsync()
        {
            await LoadData();

        }

        private async Task LoadData()
        {

            if (int.TryParse(Id, out int userId) && userId != 0)
            {
                User = await UserService.GetUser(int.Parse(Id));
            }
            else
            {
                User = new ApplicationUser
                {
                    DepartmentId = 1,
                    DateOfBirth = DateTime.Now,
                    PhotoPath = "images/nophoto.jpg"
                };
            }

            Departments = (await DepartmentService.GetOnlyDepartments()).ToList();
            Mapper.Map(User, EditUserModel);
            _dob = EditUserModel.DateOfBirth;
            StateHasChanged();
        }

        public EditUserModel EditUserModel { get; set; } = new();

        protected async Task HandleSave()
        {
                Mapper.Map(EditUserModel, User);
          await  Form.Validate();
            if (Form.IsValid)
            {
            if (string.IsNullOrWhiteSpace(User.Id))
            {
                result = await UserService.UpdateUser(User);
            }
            else
            {
                result = await UserService.CreateUser(User);
            }

           
                if (result != null)
                {
                    //await OnSaveCallBack.InvokeAsync();
                    NavigationManager.NavigateTo("/admin/dashboard");
                    await Refresh();
                }
            }
        }

        private async Task Refresh()
        {
            await LoadData();
        }
    }
}