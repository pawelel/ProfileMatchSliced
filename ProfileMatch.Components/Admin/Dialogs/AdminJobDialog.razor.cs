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
    public partial class AdminJobDialog : ComponentBase
    {
        [Inject] private ISnackbar Snackbar { get; set; }
        [Inject] IUnitOfWork UnitOfWork { get; set; }
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
        [Parameter] public Job Job { get; set; } = new();
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
            TempName = Job.Name;
            TempNamePl = Job.NamePl;
            TempDescription = Job.Description;
            TempDescriptionPl = Job.DescriptionPl;
        }
        private IEnumerable<string> MaxCharacters(string ch)
        {
            if (!string.IsNullOrEmpty(ch) && 21 < ch?.Length)
                yield return L["Max 20 characters"];
        }
  

        private MudForm _form;

        private void Cancel()
        {
            MudDialog.Cancel();
            Snackbar.Add(L["Operation cancelled"], Severity.Warning);
        }

        protected async Task HandleSave()
        {
            await _form.Validate();
            if (_form.IsValid)
            {
                Job.Name = TempName;
                Job.Description = TempDescription;
                Job.NamePl = TempNamePl;
                Job.DescriptionPl = TempDescriptionPl;
                try
                {
                    await Save();
                }
                catch (Exception ex)
                {
                    Snackbar.Add(@L[$"There was an error:"] + $" {@L[ex.Message]}", Severity.Error);
                }

                MudDialog.Close(DialogResult.Ok(Job));
            }
        }
        private async Task Delete()
        {
            if (await UnitOfWork.Jobs.ExistById(Job.Id))
            {
                await UnitOfWork.Jobs.Delete(Job);
            }
            if (ShareResource.IsEn())
            {
                Snackbar.Add($"Job {Job.Name} deleted");
            }
            else
            {
                Snackbar.Add($"Kategoria {Job.NamePl} usunięta");
            }

        }
        private async Task Save()
        {
            string created;
            string updated;
            if (ShareResource.IsEn())
            {
                created = $"Job {Job.Name} created";
                updated = $"Job {Job.Name} updated";
            }
            else
            {
                created = $"Kategoria {Job.NamePl} utworzona";
                updated = $"Kategoria {Job.NamePl} zaktualizowana";
            }



            if (Job.Id == 0)
            {
                var result = await UnitOfWork.Jobs.Insert(Job);

                Snackbar.Add(created, Severity.Success);
            }
            else
            {
                var result = await UnitOfWork.Jobs.Update(Job);
                Snackbar.Add(updated, Severity.Success);
            }
        }

    }
}
