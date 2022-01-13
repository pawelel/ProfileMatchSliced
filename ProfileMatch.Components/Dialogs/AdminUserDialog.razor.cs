using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
using Microsoft.AspNetCore.Components.Authorization;

namespace ProfileMatch.Components.Dialogs
{
    public partial class AdminUserDialog : ComponentBase
    {
        [Inject] NavigationManager NavigationManager { get; set; }
        [Inject] IWebHostEnvironment Environment { get; set; }
        [Inject] DataManager<ApplicationUser, ApplicationDbContext> ApplicationUserRepository { get; set; }
        [Inject] DataManager<IdentityRole, ApplicationDbContext> IdentityRoleRepository { get; set; }
        [Inject] DataManager<IdentityUserRole<string>, ApplicationDbContext> IdentityUserRoleRepository { get; set; }
        [Inject] ISnackbar Snackbar { get; set; }
        [Inject] DataManager<Department, ApplicationDbContext> DepartmentRepository { get; set; }
        List<IdentityRole> Roles;
        string UserId;
        List<UserRoleVM> UserRolesVM;
        List<IdentityUserRole<string>> UserIdentityRoles;
        [Inject] public AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        protected MudForm Form { get; set; } // TODO add validations
        [Parameter] public DepartmentUserVM OpenedUser { get; set; }
        ApplicationUser CurrentUser;
        ApplicationUser EditedUser;
        private List<Department> Departments = new();
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
        bool created;
        bool canChangeRoles;
        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var principal = authState.User;
            if (principal != null)
            UserId = principal.FindFirst("UserId").Value;
            CurrentUser = await ApplicationUserRepository.GetById(UserId);
            await LoadData();
           await CanChangeRolesCheck();
        }
        private async Task LoadData()
        {
            Departments = await DepartmentRepository.Get();
            Roles = await IdentityRoleRepository.Get();
            
            if (!string.IsNullOrEmpty(OpenedUser.UserId) && OpenedUser != null)
            {
                try
                {
                    EditedUser = await ApplicationUserRepository.GetById(OpenedUser.UserId);
                    created = true;
                    await TryToAddUserRoles();
                }
                catch (Exception ex)
                {
                    Snackbar.Add(ex.Message, Severity.Error);
                }
            }
            else
            {
                created = false;
                EditedUser = new()
                {
                    PhotoPath = "blank-profile.png",
                    DateOfBirth = DateTime.Now
                };
            }
        }
        protected async Task HandleSave()
        {
            await Form.Validate();
            if (Form.IsValid)
            {
                EditedUser.UserName = EditedUser.Email;
                EditedUser.NormalizedEmail = EditedUser.Email.ToUpper();
                if (created)
                {
                    // Update the user
                    await ApplicationUserRepository.Update(EditedUser);
                    Snackbar.Add(@L["Account"] + $" {EditedUser.FirstName} " + $" {EditedUser.LastName} " + @L["has been updated[O]"], Severity.Success);
                }
                else
                {
                    
                    EditedUser = await ApplicationUserRepository.Insert(EditedUser);
                    Snackbar.Add(@L["Account"] + $" {EditedUser.FirstName} " + $" {EditedUser.LastName} " + @L["has been created[O]"], Severity.Success);

                    created = true;
                }
                foreach (var role in UserRolesVM)
                {
                    if (role.IsSelected && !await IdentityUserRoleRepository.ExistById(EditedUser.Id, role.RoleId))
                    {
                        IdentityUserRole<string> roleToInsert = new() { RoleId = role.RoleId, UserId = EditedUser.Id };
                        await IdentityUserRoleRepository.Insert(roleToInsert);
                    }
                    if (!role.IsSelected && await IdentityUserRoleRepository.ExistById(EditedUser.Id, role.RoleId))
                    {
                        IdentityUserRole<string> roleToRemove = new() { UserId = EditedUser.Id, RoleId = role.RoleId };
                        await IdentityUserRoleRepository.Delete(roleToRemove);
                    }
                }
                MudDialog.Close(DialogResult.Ok(true));
                await Task.Delay(2000);
                NavigationManager.NavigateTo("admin/dashboard", true);
            }
        }
        [Inject] private IStringLocalizer<LanguageService> L { get; set; }
        private void Cancel()
        {
            MudDialog.Cancel();
            Snackbar.Add(L["Operation cancelled"], Severity.Warning);
        }
        async Task UploadImage(InputFileChangeEventArgs e)
        {
            string wwwPath;
            string contentPath = $"Files/{EditedUser.Id}/Profile.png";
            string path = Path.Combine(Environment.WebRootPath, "Files", EditedUser.Id);
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            long maxFileSize = 1024 * 1024 * 5;
            var file = e.File;
            string imageType = file.ContentType;
            string[] imageTypes = { "image/jpeg", "image/jpeg", "image/png" };
            if (!imageTypes.Any(i => i.Contains(imageType)))
            {
                Snackbar.Clear();
                Snackbar.Add(@L["Wrong file format. Allowed file formats are: .jpg, .jpeg, .png."], Severity.Error);
                return;
            }
            if (file.Size > maxFileSize)
            {
                Snackbar.Clear();
                Snackbar.Add(@L["Max allowed size is 5MB"], Severity.Error);
                return;
            }

            if (imageTypes.Any(i => i.Contains(imageType)))
            {

                var resizedImage = await file.RequestImageFileAsync(imageType, 400, 400);
                using var imageStream = resizedImage.OpenReadStream(maxFileSize);
                wwwPath = $"{path}\\Profile.png";
                using FileStream fs = File.Create(wwwPath);
                await imageStream.CopyToAsync(fs);
                fs.Close();
                imageStream.Close();
                EditedUser.PhotoPath = contentPath;
                StateHasChanged();

            }
        }
        //prevent edit own role
        private async Task CanChangeRolesCheck()
        {
            await Task.Delay(0);
            if (CurrentUser.Id != OpenedUser.UserId)
            {
                canChangeRoles = true;
            }
        }

        // add rolesVM after user is created
        private async Task TryToAddUserRoles()
        {
            UserRolesVM = new();
            UserIdentityRoles = await IdentityUserRoleRepository.Get(u => u.UserId == EditedUser.Id);
            foreach (var role in Roles)
            {
                var userRoleVM = new UserRoleVM
                {
                    RoleId = role.Id,
                    RoleName = role.Name,
                    UserId = EditedUser.Id,
                    IsSelected = UserIdentityRoles.Any(r => r.RoleId == role.Id)
                };
                UserRolesVM.Add(userRoleVM);
            }
        }
    }
}