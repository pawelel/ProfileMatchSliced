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
        [Inject] NavigationManager NavigationManager { get; set; }
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
        [Parameter] public int DepartmentId { get; set; }
        private Department _dep;
        public string TempNamePl { get; set; }
        public string TempName { get; set; }
        public string TempDescription { get; set; }
        public string TempDescriptionPl { get; set; }

        protected override async Task OnInitializedAsync()
        {
            _dep = await UnitOfWork.Departments.GetOne(d=>d.Id == DepartmentId);
            if (_dep is not null &&_dep.Id>0)
            {
            TempNamePl = _dep.NamePl;
            TempName = _dep.Name;
            TempDescriptionPl = _dep.DescriptionPl;
            TempDescription = _dep.Description;
            }
            else
            {
                _dep = new();
            }
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
                _dep.NamePl = TempNamePl;
                _dep.Name = TempName;
                _dep.DescriptionPl = TempDescriptionPl;
                _dep.Description = TempDescription;
                try
                {
                    await Save();
                }
                catch (Exception ex)
                {
                    Snackbar.Add(@L[$"There was an error:"] + $" {ex.Message}", Severity.Error);
                }

                MudDialog.Close(DialogResult.Ok(_dep));
                NavigationManager.NavigateTo("admin/dashboard/0", true);
            }
        }

        private async Task Save()
        {
            string created;
            string updated;
            if (ShareResource.IsEn())
            {
                created = $"Department {_dep.Name} created";
                updated = $"Department {_dep.Name} updated";
            }
            else
            {
                updated = $"Nazwa działu {_dep.NamePl} zaktualizowana";
                created = $"Nazwa działu {_dep.NamePl} utworzona";
            }


            if (_dep.Id == 0)
            {
                var result = await UnitOfWork.Departments.Insert(_dep);
                Snackbar.Add(created, Severity.Success);
            }
            else
            {
                var result = await UnitOfWork.Departments.Update(_dep);
                Snackbar.Add(updated, Severity.Success);
            }
        }
    }
}