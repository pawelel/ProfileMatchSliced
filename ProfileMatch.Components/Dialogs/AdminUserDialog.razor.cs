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

namespace ProfileMatch.Components.Dialogs
{
    public partial class AdminUserDialog : ComponentBase
    {
        [Inject] IWebHostEnvironment Environment { get; set; }
        [Inject] DataManager<ApplicationUser, ApplicationDbContext> ApplicationUserRepository { get; set; }
        [Inject] DataManager<IdentityRole, ApplicationDbContext> IdentityRoleRepository { get; set; }
        [Inject] DataManager<IdentityUserRole<string>, ApplicationDbContext> IdentityUserRoleRepository { get; set; }
        [Inject] ISnackbar Snackbar { get; set; }
        [Inject] DataManager<Department, ApplicationDbContext> DepartmentRepository { get; set; }
        List<IdentityRole> Roles;
        readonly List<UserRoleVM> UserRoles = new();
        List<IdentityUserRole<string>> UserIdentityRoles;
        protected MudForm Form { get; set; } // TODO add validations
        private DateTime? _dob;
        bool created;
        [Parameter] public DepartmentUserVM OpenedUser { get; set; }
        [CascadingParameter] public ApplicationUser CurrentUser { get; set; }
        ApplicationUser EditedUser;
        private List<Department> Departments = new();
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
        
        IdentityRole UserRole;
        bool canChangeRoles;
        protected override async Task OnInitializedAsync()
        {
            await LoadData();
        }
        private async Task LoadData()
        {
            
            UserRole = await IdentityRoleRepository.GetOne(q => q.Name.Contains("User"));

            await CheckRoles(OpenedUser.UserId);
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
        private async Task CheckRoles(string userId)
        {
            EditedUser = await ApplicationUserRepository.GetById(userId) == null ? EditedUser = new() : EditedUser;
            if (CurrentUser.Id == userId || string.IsNullOrEmpty(userId))
            {
                canChangeRoles = false;
                created = false;
                return;
            }
            if (UserIdentityRoles.Count == 0&&!string.IsNullOrEmpty(EditedUser.Id))
            {
                await IdentityUserRoleRepository.Insert(new() { RoleId = UserRole.Id, UserId = EditedUser.Id });
            }
            Roles = await IdentityRoleRepository.Get();
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
                    await AddUserRole(EditedUser);
                }
                foreach (var role in UserRoles)
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
                await AddUserRole(EditedUser);
            }
        }

        private async Task AddUserRole(ApplicationUser updatedUser)
        {
           
            await CheckRoles(updatedUser.Id);
        }

        async Task OnChange(InputFileChangeEventArgs e)
        {
            string wwwPath;
            string contentPath = $"Files/{EditedUser.Id}/Profile.png";
            string path = Path.Combine(Environment.WebRootPath, "Files", EditedUser.Id);
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
                imageStream.Close();
                fs.Close();
                EditedUser.PhotoPath = contentPath;
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
