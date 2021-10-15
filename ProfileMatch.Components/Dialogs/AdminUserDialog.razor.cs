using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;

using MudBlazor;

using ProfileMatch.Contracts;

using ProfileMatch.Models.Models;

namespace ProfileMatch.Components.Dialogs
{
    public partial class AdminUserDialog : ComponentBase
    {
        [Inject]
        private AuthenticationStateProvider AuthSP { get; set; }

        [Inject]
        private UserManager<ApplicationUser> UserManager { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        public IUserRepository UserRepository { get; set; }

        [Inject]
        public IDepartmentRepository DepartmentRepository { get; set; }

        [Parameter] public string Id { get; set; }
        private ApplicationUser currentUser = new();
        protected MudForm Form { get; set; } // TODO add validations
        private DateTime? _dob;
        private string currentUserName;
        [Parameter] public ApplicationUser EditedUser { get; set; } = new();
        private List<Department> Departments = new();

        private async Task GetUserDetails()
        {
            var authState = await AuthSP.GetAuthenticationStateAsync();
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

            await GetUserDetails();
        }

        private async Task LoadData()
        {
            Departments = await DepartmentRepository.GetAll();
            if (EditedUser.DateOfBirth == null)
            {
                _dob = DateTime.Now;
            }
            else
            {
                _dob = EditedUser.DateOfBirth;
            }
            StateHasChanged();
        }

        protected async Task HandleSave()
        {
            await Form.Validate();
            if (Form.IsValid)
            {
                EditedUser.DateOfBirth = (DateTime)_dob;
                EditedUser.UserName = EditedUser.Email;
                EditedUser.NormalizedEmail = EditedUser.Email.ToUpper();
                if (await UserRepository.FindByEmail(EditedUser.Email) != null)
                {
                    await UserRepository.Update(EditedUser);
                }
                else
                {
                    await UserRepository.Create(EditedUser);
                }

                NavigationManager.NavigateTo("/admin/dashboard");
            }
        }
        private IBrowserFile file;
        private async void UploadFile(InputFileChangeEventArgs e)
        {
            file = e.File;
            var buffers = new byte[file.Size];
            await file.OpenReadStream(maxFileSize).ReadAsync(buffers);
            EditedUser.PhotoPath = $"data:{file.ContentType};base64,{Convert.ToBase64String(buffers)}";
            StateHasChanged();
        }
        private long maxFileSize = 1024*1024 * 15;
    }
}