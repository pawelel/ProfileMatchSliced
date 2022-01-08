using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

using MudBlazor;

using ProfileMatch.Data;
using ProfileMatch.Models.Models;
using ProfileMatch.Models.ViewModels;
using ProfileMatch.Repositories;
using ProfileMatch.Services;

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
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await LoadData();
        }
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
                    Snackbar.Add(@L["Account"] + $" {EditedUser.FirstName} " + $" {EditedUser.LastName} " + @L["has been updated[O]"], Severity.Success);
                }
                else
                {
                    await ApplicationUserManager.Insert(EditedUser);
                    Snackbar.Add(@L["Account"] + $" {EditedUser.FirstName} " + $" {EditedUser.LastName} " + @L["has been created[O]"], Severity.Success);
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
            string[] imageTypes = { "image/jpeg", "image/jpeg", "image/png" };
            if (!imageTypes.Any(i=>i.Contains(imageType)))
            {
                Snackbar.Clear();
                Snackbar.Add(@L["Wrong file format. Allowed file formats are: .jpg, .jpeg, .png."], Severity.Error);
                return;
            }
            if (file.Size> 5200000)
            {
                Snackbar.Clear();
                Snackbar.Add(@L["Max allowed size is 5MB"], Severity.Error);
                return;
            }

            if (imageTypes.Any(i => i.Contains(imageType)))
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
        private void Cancel()
        {
            MudDialog.Cancel();
            Snackbar.Add(L["Operation cancelled"], Severity.Warning);
        }
       
    }
}