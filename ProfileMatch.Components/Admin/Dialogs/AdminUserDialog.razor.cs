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
using ProfileMatch.Models.Entities;
using ProfileMatch.Models.ViewModels;
using ProfileMatch.Repositories;
using ProfileMatch.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ProfileMatch.Components.Admin.Dialogs
{
    public partial class AdminUserDialog : ComponentBase
    {
        [Inject] NavigationManager NavigationManager { get; set; }
        [Inject] IUnitOfWork UnitOfWork { get; set; }
        [Inject] IWebHostEnvironment Environment { get; set; }
        [Inject] IMapper Mapper { get; set; }

        [Inject] IEmailSender EmailSender { get; set; }

        [Inject] UserManager<ApplicationUser> UserManager { get; set; }

        [Inject] ISnackbar Snackbar { get; set; }

        List<IdentityRole> _roles;
        List<IdentityUserRole<string>> _userRoles;
        string _userId;

        [Inject] public AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        protected MudForm Form { get; set; }
        [Parameter] public string UserId { get; set; }
        ApplicationUserVM _openedUser;
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
            await LoadDepartmentsJobsRoles();
            await GetEditedUser();
        }

        async Task GetEditedUser()
        {
            _editedUser = await UnitOfWork.ApplicationUsers.GetOne(a => a.Id == UserId, a => a.Include(a => a.Department).Include(a => a.Job));
            if (_editedUser == null)
            {
                   _openedUser = new()
                {
                    PhotoPath = "blank-profile.png",
                    DateOfBirth = DateTime.Now,
                    UserRolesVM = (from role in _roles
                                   where role.Name != "User"
                                   select new UserRoleVM()
                                   {
                                       IsSelected = false,
                                       RoleId = role.Id,
                                       RoleName = role.Name
                                   }).ToList()
                };
                _canChangeRoles = true;
            }
            else
            {
                _openedUser = Mapper.Map<ApplicationUserVM>(_editedUser);
                _openedUser.UserRolesVM = new();
                _openedUser.UserRolesVM = (from r in _roles
                                           select new UserRoleVM()
                                           {
                                               UserId = _openedUser.Id,
                                               IsSelected = _userRoles.Any(a => a.RoleId == r.Id),
                                               RoleId = r.Id,
                                               RoleName = r.Name
                                           }).ToList();
                Console.WriteLine(_openedUser);
                if (UserId != _userId)
                {
                    _canChangeRoles = true;
                }
                _created = true;
            }
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
            _currentUser = await UnitOfWork.ApplicationUsers.GetById(_userId);
        }

        /// <summary>
        /// initializes data from database
        /// </summary>
        /// <returns></returns>
        private async Task LoadDepartmentsJobsRoles()
        {
            _departments = await UnitOfWork.Departments.Get();
            _jobs = await UnitOfWork.Jobs.Get();
            _roles = await UnitOfWork.IdentityRoles.Get();
            _userRoles = await UnitOfWork.IdentityUserRoles.Get(a => a.UserId == UserId);
        }

        /// <summary>
        /// opens/closes dialog
        /// </summary>
        void ToggleOpen()
        {
            _isOpen = !_isOpen;
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
                ApplicationUser tempUser;
                _editedUser.DepartmentId = _openedUser.DepartmentId;
                _editedUser.JobId = _openedUser.JobId;
                _editedUser.FirstName = _openedUser.FirstName;
                _editedUser.LastName = _openedUser.LastName;
                _editedUser.Email = _openedUser.Email;
                _editedUser.PhotoPath= _openedUser.PhotoPath;
                _editedUser.DateOfBirth = _openedUser.DateOfBirth;
                _editedUser.UserName = _openedUser.Email;
                _editedUser.Gender = _openedUser.Gender;
                _editedUser.NormalizedEmail = _editedUser.Email.ToUpper();
                _editedUser.NormalizedUserName = _editedUser.Email.ToUpper();
                _editedUser.UserName = _editedUser.Email;
                tempUser = await UnitOfWork.ApplicationUsers.GetOne(a => a.NormalizedEmail == _editedUser.Email.ToUpper());
                if (tempUser != null&&_editedUser.Id != tempUser.Id)
                {
                    Snackbar.Add(L["User with this Email already exists"], Severity.Error);
                    return;
                }
                if (!string.IsNullOrWhiteSpace(_editedUser.Id))
                {
                    // Update the user
                    await UnitOfWork.ApplicationUsers.Update(_editedUser);
                    if (_canChangeRoles)
                    {
                        foreach (var role in _openedUser.UserRolesVM)
                        {
                            await UpdateIdentityUserRole(role);
                        }
                    }
                    Snackbar.Add(@L["Account"] + _editedUser.FullName + @L["has been updated[O]"], Severity.Success);
                }
                else
                {
                    await CreateUserWithUserRole();
                    await SendConfirmationEmail();
                }

                MudDialog.Close(DialogResult.Ok(true));

                NavigationManager.NavigateTo("admin/dashboard/0", true);
            }
        }

    
        async Task UpdateIdentityUserRole(UserRoleVM role)
        {
            var userRole = await UnitOfWork.IdentityUserRoles.GetById(role.UserId, role.RoleId);
            if (userRole == null && role.IsSelected)
            {
                userRole = new() { UserId = role.UserId, RoleId = role.RoleId };
                userRole = await UnitOfWork.IdentityUserRoles.Insert(userRole);
            }
            if (userRole != null && !role.IsSelected)
            {
                await UnitOfWork.IdentityUserRoles.Delete(userRole);
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
            _editedUser = await UnitOfWork.ApplicationUsers.Insert(_editedUser);
            Snackbar.Add(@L["Account"] + $" {_editedUser.FirstName} " + $" {_editedUser.LastName} " + @L["has been created[O]"], Severity.Success);
            
            // add "User" role
            await UnitOfWork.IdentityUserRoles.Insert(new() { UserId = _editedUser.Id, RoleId = "9588cfdb-8071-49c0-82cf-c51f20d305d2" });
            _created = true;
            MudDialog.Close(true);
            NavigationManager.NavigateTo("admin/dashboard/0", true);
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
            if (passwordString.Length<8)
            {
                Snackbar.Add(@L["Password minimum length is 8 characters"], Severity.Warning);
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
                var user = await UnitOfWork.ApplicationUsers.GetById(userId);
                if (user != null)
                {
                    var deleted = await UnitOfWork.ApplicationUsers.Delete(user);
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


    }
}