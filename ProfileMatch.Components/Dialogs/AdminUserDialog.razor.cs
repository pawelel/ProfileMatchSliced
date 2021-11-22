using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;

using MudBlazor;

using ProfileMatch.Contracts;
using ProfileMatch.Data;
using ProfileMatch.Models.Models;
using ProfileMatch.Repositories;
using ProfileMatch.Services;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProfileMatch.Components.Dialogs
{
    public partial class AdminUserDialog : ComponentBase
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject] DataManager<ApplicationUser, ApplicationDbContext> UserRepository { get; set; }

        [Inject] DataManager<Department, ApplicationDbContext> DepartmentRepository { get; set; }

        [Parameter] public string Id { get; set; }
        protected MudForm Form { get; set; } // TODO add validations
        private DateTime? _dob;
        [Parameter] public ApplicationUser EditedUser { get; set; } = new();
        private List<Department> Departments = new();

        protected override async Task OnInitializedAsync()
        {
            await LoadData();
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
                if (await UserRepository.Get(u=>u.Email.ToUpper()==EditedUser.Email) != null)
                {
                    await UserRepository.Update(EditedUser);
                }
                else
                {
                    await UserRepository.Insert(EditedUser);
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

        private readonly long maxFileSize = 1024 * 1024 * 15;

        [Inject]
        private IStringLocalizer<LanguageService> L { get; set; }
    }
}