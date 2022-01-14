using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MudBlazor.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;

using MudBlazor;

using ProfileMatch.Services;

using ProfileMatch.Models.Models;
using ProfileMatch.Repositories;
using ProfileMatch.Data;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace ProfileMatch.Components.Dialogs
{
    public partial class UserCertDialog : ComponentBase
    {

        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
        [Inject] IWebHostEnvironment Environment { get; set; }
        [Inject] DataManager<Certificate, ApplicationDbContext> CertificateRepository { get; set; }
        [Inject] ISnackbar Snackbar { get; set; }
        [Inject] IRedirection Redirection { get; set; }
        [Parameter] public Certificate OpenCertificate { get; set; } = new();
        [Parameter] public ApplicationUser CurrentUser { get; set; } = new();
        [Inject] IStringLocalizer<LanguageService> L { get; set; }
        public string TempName { get; set; }
        public string TempImage { get; set; }
        public string TempUrl { get; set; }
        DateTime? TempDate = DateTime.Today;
        DateTime? TempValidTo = DateTime.Today;



        public string TempDescription { get; set; }
        public string TempDescriptionPl { get; set; }
        bool _isOpen = false;
        MudForm Form;

        public void ToggleOpen()
        {
            _isOpen = !_isOpen;
        }



        protected override async void OnInitialized()
        {
            if (CurrentUser == null)
            {
                CurrentUser = await Redirection.GetUser();
            }

            if (OpenCertificate == null)
            {
                OpenCertificate = new();

            }
            else
            {
                TempDate = OpenCertificate.DateCreated;
                TempValidTo = OpenCertificate.ValidToDate;
                TempDescription = OpenCertificate.Description;
                TempDescriptionPl = OpenCertificate.DescriptionPl;
                TempImage = OpenCertificate.Image;
                TempName = OpenCertificate.Name;
                TempUrl = OpenCertificate.Url;
            }

        }
        private void Cancel()
        {
            MudDialog.Cancel();
            Snackbar.Add(@L["Operation cancelled"], Severity.Warning);
        }
        protected async Task HandleSave()
        {
            await Form.Validate();
            if (Form.IsValid)
            {
                OpenCertificate.Description = TempDescription;
                OpenCertificate.Image = TempImage;
                OpenCertificate.Name = TempName;
                OpenCertificate.Description = TempDescription;
                OpenCertificate.Url = TempUrl;
                OpenCertificate.UserId = CurrentUser.Id;
                OpenCertificate.DateCreated = (DateTime)TempDate;
                OpenCertificate.ValidToDate = (DateTime)TempValidTo;
                OpenCertificate.DescriptionPl = TempDescriptionPl;

                try
                {
                    await Save();
                }
                catch (Exception ex)
                {
                    Snackbar.Add(@L[$"There was an error:"] + $" {ex.Message}", Severity.Error);
                }

                MudDialog.Close(DialogResult.Ok(OpenCertificate));
            }
        }
        private IEnumerable<string> MaxCharacters(string ch)
        {
            if (!string.IsNullOrEmpty(ch) && 21 < ch?.Length)
                yield return L["Max 20 characters"];
        }
        private async Task Save()
        {
            string created;
            string updated;
            if (ShareResource.IsEn())
            {
                created = $"Certificate {OpenCertificate.Name} created";
                updated = $"Certificate {OpenCertificate.Name} updated";
            }
            else
            {
                created = $"Certyfikat {OpenCertificate.Name} utworzony";
                updated = $"Certyfikat {OpenCertificate.Name} zaktualizowany";
            }

            if (OpenCertificate.Id == 0)
            {

                var result = await CertificateRepository.Insert(OpenCertificate);

                Snackbar.Add(created, Severity.Success);
            }
            else
            {
                var result = await CertificateRepository.Update(OpenCertificate);
                Snackbar.Add(updated, Severity.Success);
            }
        }

        async Task UploadImage(InputFileChangeEventArgs e)
        {
            var file = e.File;
            var name = file.Name;
            string wwwPath;
            string contentPath = $"Files/{CurrentUser.Id}/{name}";
            string path = Path.Combine(Environment.WebRootPath, "Files", CurrentUser.Id);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            long maxFileSize = 1024 * 1024 * 5;
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
                wwwPath = $"{path}\\{name}";
                using FileStream fs = File.Create(wwwPath);
                await imageStream.CopyToAsync(fs);
                fs.Close();
                imageStream.Close();
                TempImage = contentPath;
                StateHasChanged();

            }
        }

        private async Task Delete()
        {
            if (await CertificateRepository.ExistById(OpenCertificate.Id))
            {
                await CertificateRepository.Delete(OpenCertificate);
            }
            if (ShareResource.IsEn())
            {
                Snackbar.Add($"Certificate {OpenCertificate.Name} deleted");
            }
            else
            {
                Snackbar.Add($"Certyfikat {OpenCertificate.Name} usunięty");
            }

        }
    }
}
