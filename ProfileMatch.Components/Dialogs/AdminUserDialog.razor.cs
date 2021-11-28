using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

using MudBlazor;

using ProfileMatch.Data;
using ProfileMatch.Models.Models;
using ProfileMatch.Models.ViewModels;
using ProfileMatch.Repositories;
using ProfileMatch.Services;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileMatch.Components.Dialogs
{
    public partial class AdminUserDialog : ComponentBase
    {
        [Inject] private NavigationManager NavigationManager { get; set; }
        [Inject] DataManager<ApplicationUser, ApplicationDbContext> ApplicationUserManager { get; set; }
        [Inject] DataManager<IdentityRole, ApplicationDbContext> IdentityRoleManager { get; set; }
        [Inject] DataManager<IdentityUserRole<string>, ApplicationDbContext> IdentityUserRoleManager { get; set; }
        [Inject] ISnackbar Snackbar { get; set; } //todo add notifications to dialog

        List<IdentityRole> Roles;
        readonly List<UserRoleVM> UserRoles = new();
        [Inject] DataManager<Department, ApplicationDbContext> DepartmentRepository { get; set; }
        List<IdentityUserRole<string>> UserIdentityRoles;
        [Parameter] public string Id { get; set; }
        protected MudForm Form { get; set; } // TODO add validations
        private DateTime? _dob;
        [Parameter] public ApplicationUser EditedUser { get; set; } = new();
        private List<Department> Departments = new();
        protected override async Task OnInitializedAsync()
        {
            await LoadData();
        }
        bool isShow;
        InputType PasswordInput = InputType.Password;
        string EditedUserPassword;
        readonly PasswordHasher<ApplicationUser> hasher = new();
        string PasswordInputIcon = Icons.Material.Filled.VisibilityOff;
        private async Task LoadData()
        {
            Roles = await IdentityRoleManager.Get();
            UserIdentityRoles = await IdentityUserRoleManager.Get(u => u.UserId == EditedUser.Id);
            foreach (var role in Roles)
            {
                var userRoleVM = new UserRoleVM
                {
                    RoleId = role.Id,
                    RoleName = role.Name,
                    UserId = EditedUser.Id
                };
                if (UserIdentityRoles.Any(r => r.RoleId == role.Id))
                {
                    userRoleVM.IsSelected = true;
                }
                else
                {
                    userRoleVM.IsSelected = false;
                }
                UserRoles.Add(userRoleVM);
            }
            Departments = await DepartmentRepository.Get();
            if (string.IsNullOrEmpty(EditedUser.PhotoPath))
            {
                EditedUser.PhotoPath = "files/blank-profile.png";
            }
            if (EditedUser.DateOfBirth == null)
            {
                _dob = DateTime.Now;
            }
            else
            {
                _dob = EditedUser.DateOfBirth;
            }
        }
        void ButtonTestclick()
        {
            if (isShow)
            {
                isShow = false;
                PasswordInputIcon = Icons.Material.Filled.VisibilityOff;
                PasswordInput = InputType.Password;
            }
            else
            {
                isShow = true;
                PasswordInputIcon = Icons.Material.Filled.Visibility;
                PasswordInput = InputType.Text;
            }
        }
        protected async Task HandleSave()
        {
            await Form.Validate();
            if (Form.IsValid)
            {
                EditedUser.DateOfBirth = (DateTime)_dob;
                EditedUser.UserName = EditedUser.Email;
                EditedUser.NormalizedEmail = EditedUser.Email.ToUpper();
                var exists = await ApplicationUserManager.ExistById(EditedUser.Id);
                if (exists)
                {
                    // Update the user
                    await ApplicationUserManager.Update(EditedUser);
                }
                else
                {
                    EditedUser.PasswordHash = hasher.HashPassword(EditedUser, EditedUserPassword);
                    await ApplicationUserManager.Insert(EditedUser);
                }
                foreach (var role in UserRoles)
                {
                    if (role.IsSelected && !await IdentityUserRoleManager.ExistById(EditedUser.Id, role.RoleId))
                    {
                        IdentityUserRole<string> roleToInsert = new() { RoleId = role.RoleId, UserId = EditedUser.Id };
                        await IdentityUserRoleManager.Insert(roleToInsert);
                    }
                    if (!role.IsSelected && await IdentityUserRoleManager.ExistById(EditedUser.Id, role.RoleId))
                    {
                        IdentityUserRole<string> roleToRemove = new() { UserId = EditedUser.Id, RoleId = role.RoleId };
                        await IdentityUserRoleManager.Delete(roleToRemove);
                    }
                }
                StateHasChanged();
                NavigationManager.NavigateTo("/admin/dashboard");
            }
        }

        async Task OnChange(InputFileChangeEventArgs e)
        {
            var file = e.File;
            string imageType = file.ContentType;
            if (imageType != "image/jpeg")
            {
                Snackbar.Clear();
                Snackbar.Add("Wrong file format. Allowed file formats are: .jpg, .jpeg, .png.", Severity.Error);
                return;
            }
            if (file.Size> 5200000)
            {
                Snackbar.Clear();
                Snackbar.Add("Max allowed size is 5MB", Severity.Error);
                return;
            }
            if (imageType == "image/jpeg")
            {
                var resizedImage = await file.RequestImageFileAsync(imageType, 400, 400);
                var buffer = new byte[resizedImage.Size];
                await resizedImage.OpenReadStream().ReadAsync(buffer);
                
                EditedUser.PhotoPath = $"data:{imageType};base64,{Convert.ToBase64String(buffer)}";
                StateHasChanged();
            }
        }


        [Inject]
        private IStringLocalizer<LanguageService> L { get; set; }
    }
}