using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

using MudBlazor;


using ProfileMatch.Data;
using ProfileMatch.Models.Entities;
using ProfileMatch.Repositories;
using ProfileMatch.Services;

using System;
using System.Threading.Tasks;

namespace ProfileMatch.Components.Admin.Dialogs
{
    public partial class AdminDepartmentDialog : ComponentBase
    {
        [Inject] private ISnackbar Snackbar { get; set; }
        [Inject] IUnitOfWork UnitOfWork { get; set; }
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
        [Parameter] public Department Dep { get; set; } = new();
        public string TempNamePl { get; set; }
        public string TempName { get; set; }
        public string TempDescription { get; set; }
        public string TempDescriptionPl { get; set; }

        protected override void OnInitialized()
        {
            TempNamePl = Dep.NamePl;
            TempName = Dep.Name;
            TempDescriptionPl = Dep.DescriptionPl;
            TempDescription = Dep.Description;
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
                Dep.NamePl = TempNamePl;
                Dep.Name = TempName;
                Dep.DescriptionPl = TempDescriptionPl;
                Dep.Description = TempDescription;
                try
                {
                    await Save();
                }
                catch (Exception ex)
                {
                    Snackbar.Add(@L[$"There was an error:"] + $" {ex.Message}", Severity.Error);
                }

                MudDialog.Close(DialogResult.Ok(Dep));
            }
        }

        private async Task Save()
        {
            string created;
            string updated;
            if (ShareResource.IsEn())
            {
                created = $"Department {Dep.Name} created";
                updated = $"Department {Dep.Name} updated";
            }
            else
            {
                updated = $"Nazwa działu {Dep.NamePl} zaktualizowana";
                created = $"Nazwa działu {Dep.NamePl} utworzona";
            }


            if (Dep.Id == 0)
            {
                var result = await UnitOfWork.Departments.Insert(Dep);
                Snackbar.Add(created, Severity.Success);
            }
            else
            {
                var result = await UnitOfWork.Departments.Update(Dep);
                Snackbar.Add(updated, Severity.Success);
            }
        }
    }
}