using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

using MudBlazor;

using ProfileMatch.Data;
using ProfileMatch.Models.Models;
using ProfileMatch.Repositories;
using ProfileMatch.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileMatch.Components.Admin.Dialogs
{
    public partial class AdminJobTitleDialog : ComponentBase
    {
        [Inject] private ISnackbar Snackbar { get; set; }
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
        [Parameter] public JobTitle JobTitle { get; set; } = new();
        public string TempName { get; set; }
        public string TempNamePl { get; set; }
        public string TempDescription { get; set; }
        public string TempDescriptionPl { get; set; }
        bool _isOpen = false;
        public void ToggleOpen()
        {
            _isOpen = !_isOpen;
        }
        protected override void OnInitialized()
        {
            TempName = JobTitle.Name;
            TempNamePl = JobTitle.NamePl;
            TempDescription = JobTitle.Description;
            TempDescriptionPl = JobTitle.DescriptionPl;
        }
        private IEnumerable<string> MaxCharacters(string ch)
        {
            if (!string.IsNullOrEmpty(ch) && 21 < ch?.Length)
                yield return L["Max 20 characters"];
        }
        [Inject] DataManager<JobTitle, ApplicationDbContext> JobTitleRepository { get; set; }

        private MudForm Form;

        private void Cancel()
        {
            MudDialog.Cancel();
            Snackbar.Add(L["Operation cancelled"], Severity.Warning);
        }

        protected async Task HandleSave()
        {
            await Form.Validate();
            if (Form.IsValid)
            {
                JobTitle.Name = TempName;
                JobTitle.Description = TempDescription;
                JobTitle.NamePl = TempNamePl;
                JobTitle.DescriptionPl = TempDescriptionPl;
                try
                {
                    await Save();
                }
                catch (Exception ex)
                {
                    Snackbar.Add(@L[$"There was an error:"] + $" {@L[ex.Message]}", Severity.Error);
                }

                MudDialog.Close(DialogResult.Ok(JobTitle));
            }
        }
        private async Task Delete()
        {
            if (await JobTitleRepository.ExistById(JobTitle.Id))
            {
                await JobTitleRepository.Delete(JobTitle);
            }
            if (ShareResource.IsEn())
            {
                Snackbar.Add($"JobTitle {JobTitle.Name} deleted");
            }
            else
            {
                Snackbar.Add($"Kategoria {JobTitle.NamePl} usunięta");
            }

        }
        private async Task Save()
        {
            string created;
            string updated;
            if (ShareResource.IsEn())
            {
                created = $"JobTitle {JobTitle.Name} created";
                updated = $"JobTitle {JobTitle.Name} updated";
            }
            else
            {
                created = $"Kategoria {JobTitle.NamePl} utworzona";
                updated = $"Kategoria {JobTitle.NamePl} zaktualizowana";
            }



            if (JobTitle.Id == 0)
            {
                var result = await JobTitleRepository.Insert(JobTitle);

                Snackbar.Add(created, Severity.Success);
            }
            else
            {
                var result = await JobTitleRepository.Update(JobTitle);
                Snackbar.Add(updated, Severity.Success);
            }
        }

        [Inject]
        private IStringLocalizer<LanguageService> L { get; set; }
    }
}
