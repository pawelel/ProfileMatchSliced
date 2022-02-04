using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

using MudBlazor;

using ProfileMatch.Data;
using ProfileMatch.Models.Entities;
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
        [Parameter] public int JobId { get; set; }
        private Job _job;
        public string TempName { get; set; }
        public string TempNamePl { get; set; }
        public string TempDescription { get; set; }
        public string TempDescriptionPl { get; set; }
        bool _isOpen = false;
        public void ToggleOpen()
        {
            _isOpen = !_isOpen;
        }
        protected override async Task OnInitializedAsync()
        {_job = await UnitOfWork.Jobs.GetOne(a=>a.Id == JobId);
            if (_job is not null)
            {
            TempName = _job.Name;
            TempNamePl = _job.NamePl;
            TempDescription = _job.Description;
            TempDescriptionPl = _job.DescriptionPl;
            }
            else
            {
                _job = new();
            }
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
                _job.Name = TempName;
                _job.Description = TempDescription;
                _job.NamePl = TempNamePl;
                _job.DescriptionPl = TempDescriptionPl;
                try
                {
                    await Save();
                }
                catch (Exception ex)
                {
                    Snackbar.Add(@L[$"There was an error:"] + $" {@L[ex.Message]}", Severity.Error);
                }

                MudDialog.Close(DialogResult.Ok(_job));
            }
        }
        private async Task Delete()
        {
            if (await UnitOfWork.Jobs.ExistById(_job.Id))
            {
                await UnitOfWork.Jobs.Delete(_job);
            }
            if (ShareResource.IsEn())
            {
                Snackbar.Add($"Job {_job.Name} deleted");
            }
            else
            {
                Snackbar.Add($"Kategoria {_job.NamePl} usunięta");
            }

        }
        private async Task Save()
        {
            string created;
            string updated;
            if (ShareResource.IsEn())
            {
                created = $"Job {_job.Name} created";
                updated = $"Job {_job.Name} updated";
            }
            else
            {
                created = $"Kategoria {_job.NamePl} utworzona";
                updated = $"Kategoria {_job.NamePl} zaktualizowana";
            }



            if (_job.Id == 0)
            {
                var result = await UnitOfWork.Jobs.Insert(_job);

                Snackbar.Add(created, Severity.Success);
            }
            else
            {
                var result = await UnitOfWork.Jobs.Update(_job);
                Snackbar.Add(updated, Severity.Success);
            }
        }

    }
}
