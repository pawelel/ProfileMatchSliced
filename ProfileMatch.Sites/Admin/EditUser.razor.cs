using System;
using System.Collections.Generic;
using System.Threading.Tasks;



using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;

using MudBlazor;

using ProfileMatch.Contracts;
using ProfileMatch.Models.Models;
using ProfileMatch.Models.Responses;


namespace ProfileMatch.Sites.Admin
{
    public partial class EditUser : ComponentBase
    {
        [Inject]
        AuthenticationStateProvider authSP { get; set; }

        [Inject]
        UserManager<ApplicationUser> UserManager { get; set; }
        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        public IMapper Mapper { get; set; }

        [Inject]
        public IUserService UserService { get; set; }

        [Inject]
        public IDepartmentService DepartmentService { get; set; }
        [Parameter] public string Id { get; set; }
        ApplicationUser currentUser = new();
        protected MudForm Form { get; set; } // TODO add validations
        private bool _success;
        DateTime? _dob;
        string currentUserName;
        ServiceResponse<ApplicationUser> loadedUserResponse = new();
        ApplicationUser EditUser { get; set; } = new();
        private IEnumerable<Department> Departments = new List<Department>();
        private async Task GetUserDetails()
        {
var authState = await authSP.GetAuthenticationStateAsync();
        var user = authState.User;
            if (user.Identity.IsAuthenticated)
            {
        currentUser = await UserManager.GetUserAsync(user);
        currentUserName = currentUser.FirstName + " " + currentUser.LastName;
            }
            else
            {
                currentUserName = "Please log in.";
            }

        }
        
        protected override async Task OnInitializedAsync()
        {
            await LoadData();
            EditUser = loadedUserResponse.Data;
            await GetUserDetails();
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
                if (await UserService.Exist(EditUser.Email))
                {
                await UserService.Update(EditUser);
                }
                else
                {
                   await UserService.Create(EditUser);
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
