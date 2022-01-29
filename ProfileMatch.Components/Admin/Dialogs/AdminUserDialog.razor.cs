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
        [Inject] DataManager<Job, ApplicationDbContext> JobRepository { get; set; }
        [Inject] UserManager<ApplicationUser> UserManager { get; set; }
        [Inject] DataManager<IdentityUserRole<string>, ApplicationDbContext> IdentityUserRoleRepository { get; set; }
        [Inject] ISnackbar Snackbar { get; set; }
        [Inject] DataManager<Department, ApplicationDbContext> DepartmentRepository { get; set; }
        List<IdentityRole> _roles;
        string _userId;
        List<UserRoleVM> _userRolesVM;
        List<IdentityUserRole<string>> _userIdentityRoles;
        [Inject] public AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        protected MudForm Form { get; set; }
        [Parameter] public DepartmentUserVM OpenedUser { get; set; }
        ApplicationUser _currentUser;
        ApplicationUser _editedUser;
        List<Job> _jobs;
        string _passwordHash;
        private List<Department> _departments = new();
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
        bool _created;
        bool _canChangeRoles;
        bool _isOpen;
        //on user dialog initialize
        protected override async Task OnInitializedAsync()
        {
            await GetCurrentUserAsync();
            await LoadDepartmentsJobsRolesUser();
            await CanChangeRolesCheck();
        }

        /// <summary>
        /// Load logged in user
        /// </summary>
        /// <returns></returns>
        private async Task GetCurrentUserAsync()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var principal = authState.User;
            if (principal != null)
                _userId = principal.FindFirst("UserId").Value;
            _currentUser = await ApplicationUserRepository.GetById(_userId);
        }

        /// <summary>
        /// initializes data from database
        /// </summary>
        /// <returns></returns>
        private async Task LoadDepartmentsJobsRolesUser()
        {
            _jobs = await JobRepository.Get();
            _departments = await DepartmentRepository.Get();
            _roles = await IdentityRoleRepository.Get();

            if (!string.IsNullOrEmpty(OpenedUser.UserId) && OpenedUser != null)
            {
                try
                {
                    _editedUser = await ApplicationUserRepository.GetById(OpenedUser.UserId);
                    _created = true;
                    await TryToAddUserRoles();
                }
                catch (Exception ex)
                {
                    Snackbar.Add(ex.Message, Severity.Error);
                }
            }
            else
            {
                _created = false;
                _editedUser = new()
                {
                    PhotoPath = "blank-profile.png",
                    DateOfBirth = DateTime.Now
                };
            }
        }

        /// <summary>
        /// opens/closes dialog
        /// </summary>
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
            if (!string.IsNullOrWhiteSpace(_editedUser.Id))
            {
                _userRolesVM = new();
                _userIdentityRoles = await IdentityUserRoleRepository.Get(u => u.UserId == _editedUser.Id);
                foreach (var role in _roles.Where(r => r.Name != "User"))
                {
                    var userRoleVM = new UserRoleVM
                    {
                        RoleId = role.Id,
                        RoleName = role.Name,
                        UserId = _editedUser.Id,
                        IsSelected = _userIdentityRoles.Any(r => r.RoleId == role.Id)
                    };
                    _userRolesVM.Add(userRoleVM);
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
                _editedUser.NormalizedEmail = _editedUser.Email.ToUpper();
                _editedUser.NormalizedUserName = _editedUser.Email.ToUpper();
                if (_created)
                {
                    // Update the user
                    await ApplicationUserRepository.Update(_editedUser);
                    Snackbar.Add(@L["Account"] + _editedUser.FullName + @L["has been updated[O]"], Severity.Success);
                }
                else
                {
                    await CreateUserWithUserRole();
                    await SendConfirmationEmail();
                }
                await TryToAddUserRoles();
                //await UpdateUserRoles();
                MudDialog.Close(DialogResult.Ok(true));
                await Task.Delay(2000);
                NavigationManager.NavigateTo("admin/dashboard", true);
            }
        }
        /// <summary>
        /// add or remove Manager or Admin role
        /// </summary>
        /// <returns></returns>
        //private async Task UpdateUserRoles()
        //{
        //    foreach (var role in UserRolesVM)
        //    {
        //        if (role.RoleName == "Admin")
        //        {
        //            var roles = await IdentityUserRoleRepository.Get(q => q.RoleId == role.RoleId);

        //        }

        //        if (role.IsSelected && !await IdentityUserRoleRepository.ExistById(EditedUser.Id, role.RoleId))
        //        {
        //            IdentityUserRole<string> roleToInsert = new() { RoleId = role.RoleId, UserId = EditedUser.Id };
        //            await IdentityUserRoleRepository.Insert(roleToInsert);
        //        }
        //        if (!role.IsSelected && await IdentityUserRoleRepository.ExistById(EditedUser.Id, role.RoleId))
        //        {
        //            IdentityUserRole<string> roleToRemove = new() { UserId = EditedUser.Id, RoleId = role.RoleId };
        //            await IdentityUserRoleRepository.Delete(roleToRemove);
        //        }
        //    }
        //}

        /// <summary>
        /// add or remove Manager or Admin role
        /// </summary>
        /// <returns></returns>
        private async Task UpdateUserRole(UserRoleVM uVM)
        {
            var roles = await IdentityUserRoleRepository.Get();
            var n = uVM.RoleName;
            var id = uVM.RoleId;
            var userRole = await IdentityUserRoleRepository.GetById(id);
            var filteredRoles = roles.Where(r => r.RoleId == id);

            if (n == "Admin" && filteredRoles.Count() > 1)
            {
                switch (uVM.IsSelected)
                {
                    case true:
                        await IdentityUserRoleRepository.Insert(new() { RoleId = uVM.RoleId, UserId = uVM.UserId });
                        break;
                    case false:
                        await IdentityUserRoleRepository.Delete(new() { UserId = uVM.UserId, RoleId = uVM.RoleId });
                        break;
                }
            }
            else
            {
                switch (uVM.IsSelected)
                {
                    case true:
                        await IdentityUserRoleRepository.Insert(new() { RoleId = uVM.RoleId, UserId = uVM.UserId });
                        break;
                    case false:
                        await IdentityUserRoleRepository.Delete(new() { UserId = uVM.UserId, RoleId = uVM.RoleId });
                        break;
                }
            }



        }

        /// <summary>
        /// creates user and sends confirmation email
        /// </summary>
        /// <returns></returns>
        private async Task CreateUserWithUserRole()
        {
            var hasher = new PasswordHasher<ApplicationUser>();
            _editedUser.PasswordHash = hasher.HashPassword(null, _passwordHash);
            _editedUser.UserName = _editedUser.Email;
            _editedUser = await ApplicationUserRepository.Insert(_editedUser);
            Snackbar.Add(@L["Account"] + $" {_editedUser.FirstName} " + $" {_editedUser.LastName} " + @L["has been created[O]"], Severity.Success);
            _created = true;
            // add "User" role
            await IdentityUserRoleRepository.Insert(new() { RoleId = "9588cfdb-8071-49c0-82cf-c51f20d305d2", UserId = _editedUser.Id });
        }
        /// <summary>
        /// send confirmation email after creating user
        /// </summary>
        /// <returns></returns>
        private async Task SendConfirmationEmail()
        {
            var token = await UserManager.GenerateEmailConfirmationTokenAsync(_editedUser);
            token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
            var uriBuilder = new UriBuilder(NavigationManager.ToAbsoluteUri(NavigationManager.BaseUri))
            {
                Path = $"Identity/Account/ConfirmEmail",
                Query = $"userId={_editedUser.Id}&code={token}"
            };

            var subject = "Potwierdzenie rejestracji";
            var body = $"<h1>Witaj {_editedUser.FirstName} {_editedUser.LastName}</h1><br/><br/>" +
                $"<p>w serwisie <b>Profile Match</b></p><br/><br/>" +
                $"<p>Aby potwierdzić swoje konto kliknij w poniższy link:</p><br/><br/>" +
                $"<a href='{uriBuilder.Uri}'>Potwierdź konto</a><br/><br/>" +
                $"<p>Jeśli nie potwierdzisz konta w ciągu 24 godzin, zostanie ono usunięte.</p><br/><br/>" +
                $"<p>Pozdrawiamy,</p><br/><br/>" +
                $"<p>Zespół ProfileMatch.pl</p>";
            EmailSender.SendEmail(_editedUser, subject, body);
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

            var resetToken = await UserManager.GeneratePasswordResetTokenAsync(_editedUser);

            var passwordChangeResult = await UserManager.ResetPasswordAsync(_editedUser, resetToken, passwordString);

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
        /// <summary>
        /// prevent self destruction
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        async Task DeleteUser(string userId)
        {
            if (_currentUser != null && _editedUser != _currentUser)
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
                        await Task.Delay(1000);
                        NavigationManager.NavigateTo("admin/dashboard", true);
                    }
                    else
                    {
                        Snackbar.Add(L["There is no user with those credentials"], Severity.Error);
                    }
                }
            }
            else
            {
            Snackbar.Add(L["You cannot delete your account here."], Severity.Error);
            return;
            }
        }
        private void Cancel()
        {
            MudDialog.Cancel();
            Snackbar.Add(L["Operation cancelled"], Severity.Warning);
        }
        /// <summary>
        /// upload image for user profile
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        async Task UploadImage(InputFileChangeEventArgs e)
        {
            string wwwPath;
            string contentPath = $"Files/{_editedUser.Id}/Profile.png";
            string path = Path.Combine(Environment.WebRootPath, "Files", _editedUser.Id);
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
                _editedUser.PhotoPath = contentPath;
                StateHasChanged();

            }
        }
        //prevent edit own role

        /// <summary>
        /// prevent of taking back admin role for current user - this way there should be always one admin - but it only partially
        /// </summary>
        /// <returns></returns>
        private async Task CanChangeRolesCheck()
        {
            await Task.Delay(0);
            if (_currentUser.Id != OpenedUser.UserId&&OpenedUser!=null&&!string.IsNullOrWhiteSpace( OpenedUser.UserId))
            {
                _canChangeRoles = true;
            }
        }


    }
}