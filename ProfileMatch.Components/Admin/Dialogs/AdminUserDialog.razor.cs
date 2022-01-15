using System.Web;
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
using Microsoft.AspNetCore.WebUtilities;
using NPOI.SS.Formula.Functions;
using System.Text;

namespace ProfileMatch.Components.Admin.Dialogs
{
    public partial class AdminUserDialog : ComponentBase
    {
        [Inject] NavigationManager NavigationManager { get; set; }
        [Inject] IWebHostEnvironment Environment { get; set; }
        [Inject] DataManager<ApplicationUser, ApplicationDbContext> ApplicationUserRepository { get; set; }
        [Inject] DataManager<IdentityRole, ApplicationDbContext> IdentityRoleRepository { get; set; }
        [Inject] IEmailSender EmailSender { get; set; }
        [Inject] DataManager<JobTitle, ApplicationDbContext> JobTitleRepository { get; set; }
        [Inject] UserManager<ApplicationUser> UserManager { get; set; }
        [Inject] DataManager<IdentityUserRole<string>, ApplicationDbContext> IdentityUserRoleRepository { get; set; }
        [Inject] ISnackbar Snackbar { get; set; }
        [Inject] private IStringLocalizer<LanguageService> L { get; set; }
        [Inject] DataManager<Department, ApplicationDbContext> DepartmentRepository { get; set; }
        List<IdentityRole> Roles;
        string UserId;
        List<UserRoleVM> UserRolesVM;
        List<IdentityUserRole<string>> UserIdentityRoles;
        [Inject] public AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        protected MudForm Form { get; set; }
        [Parameter] public DepartmentUserVM OpenedUser { get; set; }
        ApplicationUser CurrentUser;
        ApplicationUser EditedUser;
        List<JobTitle> jobTitles;
        string PasswordHash;
        private List<Department> Departments = new();
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
        bool created;
        bool canChangeRoles;
        bool _isOpen;
        //on initialize
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

        //load data
        private async Task LoadData()
        {
            jobTitles = await JobTitleRepository.Get();
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

        void ToggleOpen()
        {
            _isOpen = !_isOpen;
        }


        /// <summary>
        /// add rolesVM after user is created
        /// </summary>
        /// <returns></returns>
        private async Task TryToAddUserRoles()
        {
            if (!string.IsNullOrWhiteSpace(EditedUser.Id))
            {
                UserRolesVM = new();
                UserIdentityRoles = await IdentityUserRoleRepository.Get(u => u.UserId == EditedUser.Id);
                foreach (var role in Roles.Where(r => r.Name != "User"))
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
        /// <summary>
        /// Handle user update
        /// </summary>
        /// <returns></returns>
        protected async Task HandleSave()
        {
            await Form.Validate();
            if (Form.IsValid)
            {
                EditedUser.NormalizedEmail = EditedUser.Email.ToUpper();
                EditedUser.NormalizedUserName = EditedUser.Email.ToUpper();
                if (created)
                {
                    // Update the user
                    await ApplicationUserRepository.Update(EditedUser);
                    Snackbar.Add(@L["Account"] + EditedUser.FullName + @L["has been updated[O]"], Severity.Success);
                }
                else
                {
                    await CreateUserWithUserRole();
                    await SendConfirmationEmail();
                }
                await TryToAddUserRoles();
                await UpdateUserRoles();
                MudDialog.Close(DialogResult.Ok(true));
                await Task.Delay(2000);
                NavigationManager.NavigateTo("admin/dashboard", true);
            }
        }
        /// <summary>
        /// add or remove Manager or Admin role
        /// </summary>
        /// <returns></returns>
        private async Task UpdateUserRoles()
        {
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
        }

        private async Task CreateUserWithUserRole()
        {
            var hasher = new PasswordHasher<ApplicationUser>();
            EditedUser.PasswordHash = hasher.HashPassword(null, PasswordHash);
            EditedUser = await ApplicationUserRepository.Insert(EditedUser);
            Snackbar.Add(@L["Account"] + $" {EditedUser.FirstName} " + $" {EditedUser.LastName} " + @L["has been created[O]"], Severity.Success);
            created = true;
            // add "User" role
            await IdentityUserRoleRepository.Insert(new() { RoleId = "9588cfdb-8071-49c0-82cf-c51f20d305d2", UserId = EditedUser.Id });
        }
        /// <summary>
        /// send confirmation email after creating user
        /// </summary>
        /// <returns></returns>
        private async Task SendConfirmationEmail()
        {
            var token = await UserManager.GenerateEmailConfirmationTokenAsync(EditedUser);
            token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
            var uriBuilder = new UriBuilder(NavigationManager.ToAbsoluteUri(NavigationManager.BaseUri))
            {
                Path = $"Identity/Account/ConfirmEmail",
                Query = $"userId={EditedUser.Id}&code={token}"
            };

            var subject = "Potwierdzenie rejestracji";
            var body = $"<h1>Witaj {EditedUser.FirstName} {EditedUser.LastName}</h1><br/><br/>" +
                $"<p>w serwisie <b>Profile Match</b></p><br/><br/>" +
                $"<p>Aby potwierdzić swoje konto kliknij w poniższy link:</p><br/><br/>" +
                $"<a href='{uriBuilder.Uri}'>Potwierdź konto</a><br/><br/>" +
                $"<p>Jeśli nie potwierdzisz konta w ciągu 24 godzin, zostanie ono usunięte.</p><br/><br/>" +
                $"<p>Pozdrawiamy,</p><br/><br/>" +
                $"<p>Zespół ProfileMatch.pl</p>";
            EmailSender.SendEmail(EditedUser, subject, body);
        }

        /// <summary>
        /// reset password for user
        /// </summary>
        /// <param name="passwordString"></param>
        /// <returns></returns>
        async Task ResetPassword(string passwordString)
        {
            if (string.IsNullOrEmpty(passwordString))
            {
                Snackbar.Add(@L["Password cannot be empty!"], Severity.Warning);
                return;
            }

            var resetToken = await UserManager.GeneratePasswordResetTokenAsync(EditedUser);

            var passwordChangeResult = await UserManager.ResetPasswordAsync(EditedUser, resetToken, passwordString);

            if (!passwordChangeResult.Succeeded)
            {
                if (passwordChangeResult.Errors.FirstOrDefault() != null)
                {
                    Snackbar.Add(passwordChangeResult
                        .Errors
                        .FirstOrDefault()
                        .Description);
                }
                else
                {
                    Snackbar.Add("Pasword error", Severity.Error);
                }
            }
            Snackbar.Add(@L["Password is Changed To:"] + passwordString, Severity.Info);

        }
        //prevent self destruction
        async Task DeleteUser(string userId)
        {
            if (CurrentUser != null && EditedUser != CurrentUser)
            {
                var user = await ApplicationUserRepository.GetById(userId);
                if (user != null)
                {
                    var deleted = await ApplicationUserRepository.Delete(user);
                    if (deleted)
                    {
                        Snackbar.Add(L["User Deleted"], Severity.Info);
                        _isOpen = false;
                        MudDialog.Close(DialogResult.Ok(true));
                        NavigationManager.NavigateTo("admin/dashboard", true);
                    }
                    else
                    {
                        Snackbar.Add(L["There is no user with those credentials"], Severity.Error);
                    }
                }
            }
            Snackbar.Add(L["You cannot delete your account here."], Severity.Error);
            return;
        }
        private void Cancel()
        {
            MudDialog.Cancel();
            Snackbar.Add(L["Operation cancelled"], Severity.Warning);
        }
        //upload profile image
        async Task UploadImage(InputFileChangeEventArgs e)
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


    }
}