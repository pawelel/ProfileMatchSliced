using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

using MudBlazor;

using ProfileMatch.Models.Entities;
using ProfileMatch.Repositories;
using ProfileMatch.Services;

namespace ProfileMatch.Components.User.Dialogs
{
    public partial class UserCertDialog : ComponentBase
    {

        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
        [Inject] private IWebHostEnvironment Environment { get; set; }
        [Inject] ILogger<UserCertDialog> Logger { get; set; }
        [Inject] private IUnitOfWork UnitOfWork { get; set; }
        [Inject] private ISnackbar Snackbar { get; set; }
        [Inject] private IRedirection Redirection { get; set; }
        [Parameter] public Certificate OpenCertificate { get; set; }
        [Parameter] public ApplicationUser CurrentUser { get; set; }

        private string _tempName;
        private string _tempImagePath;
        private string _tempUrl;
        bool _hasValidToDate;
        private IBrowserFile _file;
        private DateTime? _tempDate = DateTime.Today;
        private DateTime? _tempValidTo;
        private string _tempDescription;
        private string _tempDescriptionPl;
        private bool _isOpen = false;
        private readonly string[] _imageTypes = { "image/jpeg", "image/jpg", "image/png", "image/img", "image/bmp", "image/tiff" };
        private readonly string _pdfFile = "application/pdf";
        private MudForm _form;
        private readonly long _maxFileSize = 1024 * 1024 * 5;
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
                _tempDate = DateTime.Today;
            }
            else
            {
                _tempDate = OpenCertificate.DateCreated;
                _tempValidTo = OpenCertificate.ValidToDate;
                _tempDescription = OpenCertificate.Description;
                _tempDescriptionPl = OpenCertificate.DescriptionPl;
                _tempName = OpenCertificate.Name;
                _tempUrl = OpenCertificate.Url;
                _tempImagePath = OpenCertificate.ImagePath;

            }

        }
        private void Cancel()
        {
            MudDialog.Cancel();
            Snackbar.Add(@L["Operation cancelled"], Severity.Warning);
        }
        protected async Task HandleSave()
        {
            await _form.Validate();
            if (_form.IsValid)
            {
                OpenCertificate.Description = _tempDescription;
                OpenCertificate.Name = _tempName;
                OpenCertificate.Description = _tempDescription;
                OpenCertificate.Url = _tempUrl;
                OpenCertificate.UserId = CurrentUser.Id;
                OpenCertificate.DateCreated = (DateTime)_tempDate;
                OpenCertificate.ValidToDate = _tempValidTo;
                OpenCertificate.DescriptionPl = _tempDescriptionPl;

                try
                {
                    await Save();
                }
                catch (Exception ex)
                {
                    Snackbar.Add(@L[$"There was an error:"] + $" {ex.Message}", Severity.Error);
                    Logger.LogError("ex",ex);
                }

                MudDialog.Close(DialogResult.Ok(OpenCertificate));
            }
        }
        private IEnumerable<string> MaxCharacters(string ch)
        {
            if (!string.IsNullOrEmpty(ch) && 21 < ch?.Length)
            {
                yield return L["Max 20 characters"];
            }
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

                OpenCertificate = await UnitOfWork.Certificates.Insert(OpenCertificate);

                OpenCertificate = await SaveCertificate(_file, OpenCertificate);
                OpenCertificate = await UnitOfWork.Certificates.Update(OpenCertificate);

                Snackbar.Add(created, Severity.Success);
            }
            else
            {
                OpenCertificate = await SaveCertificate(_file, OpenCertificate);
                OpenCertificate = await UnitOfWork.Certificates.Update(OpenCertificate);
                Snackbar.Add(updated, Severity.Success);
            }
        }

        private void UploadImage(InputFileChangeEventArgs e)
        {

            _file = e.File;
            //verify file size
            if (_file.Size > _maxFileSize)
            {
                Snackbar.Clear();
                Snackbar.Add(@L["Max allowed size is 5MB"], Severity.Error);
                _file = null;
                return;
            }
            string _fileType = _file.ContentType;

            //verify file type
            if (_fileType == _pdfFile || _imageTypes.Any(i => i.Contains(_fileType)))
            {
                if (ShareResource.IsEn())
                {
                    Snackbar.Add($"File {_file.Name} selected", Severity.Success);
                }
                else
                {
                    Snackbar.Add($"Plik {_file.Name} wybrany", Severity.Success);
                }
                return;
            }

            Snackbar.Add($"{L["Wrong file format. Allowed file formats are"]}: jpeg, jpg, png, img, bmp, tiff, pdf", Severity.Error);
            _file = null;
            return;
        }

        private async Task<Certificate> SaveCertificate(IBrowserFile _file, Certificate _certificate)
        {
            if (_file is not null)
            {
                string _fileType = _file.ContentType;
                //create/update
                _tempImagePath = $"Files/{CurrentUser.Id}/{_certificate.Id}.png";
                //user's directory
                string path = Path.Combine(Environment.WebRootPath, "Files", CurrentUser.Id);
                string wwwPath = $"{path}\\{_certificate.Id}.png";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                if (_fileType == _pdfFile)
                {
                    //get only first page
                    await using MemoryStream ms = new();
                     await _file.OpenReadStream(_maxFileSize).CopyToAsync(ms);
                    var imageData = ms.ToArray();
                    byte[] temp = Freeware.Pdf2Png.Convert(ms, 1, 300);
                    using Image image = Image.FromStream(new MemoryStream(temp));
                    image.Save(wwwPath);
                    image.Dispose();
                    ms.Close();
                    _certificate.ImagePath = _tempImagePath;
                    StateHasChanged();
                    return _certificate;
                }

                if (_imageTypes.Any(i => i.Contains(_fileType)))
                {
                    IBrowserFile resizedImage = await _file.RequestImageFileAsync(_fileType, 1000, 1000);
                    await using Stream imageStream = resizedImage.OpenReadStream(_maxFileSize);
                    await using FileStream fs = File.Create(wwwPath);
                    await imageStream.CopyToAsync(fs);
                    fs.Close();
                    imageStream.Close();
                    OpenCertificate.ImagePath = _tempImagePath;
                    StateHasChanged();
                    return _certificate;
                }
            }
            return _certificate;
        }

        private async Task Delete()
        {
            if (await UnitOfWork.Certificates.ExistById(OpenCertificate.Id))
            {
                await UnitOfWork.Certificates.Delete(OpenCertificate);
            }
            if (ShareResource.IsEn())
            {
                Snackbar.Add($"Certificate {OpenCertificate.Name} deleted");
            }
            else
            {
                Snackbar.Add($"Certyfikat {OpenCertificate.Name} usunięty");
            }
            return;
        }


    }

}
