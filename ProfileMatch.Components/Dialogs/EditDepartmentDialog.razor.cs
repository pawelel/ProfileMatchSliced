using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;

using MudBlazor;

using ProfileMatch.Contracts;
using ProfileMatch.Models.Models;

namespace ProfileMatch.Components.Dialogs
{
    public partial class EditDepartmentDialog : ComponentBase
    {
        [Inject] private ISnackbar Snackbar { get; set; }
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
        [Parameter] public Department Dep { get; set; } = new();
        public string TempName { get; set; }
        public string TempDescription { get; set; }

        protected override void OnInitialized()
        {
            TempName = Dep.Name;
            TempDescription = Dep.Description;
        }

        [Inject] public IDepartmentRepository DepartmentRepository { get; set; }

        private MudForm Form;

        private void Cancel()
        {
            MudDialog.Cancel();
            Snackbar.Add("Operation cancelled", Severity.Warning);
        }

        protected async Task HandleSave()
        {
            await Form.Validate();
            if (Form.IsValid)
            {
                Dep.Name = TempName;
                Dep.Description = TempDescription;
                try
                {
                    await Save();
                }
                catch (Exception ex)
                {
                    Snackbar.Add($"There was an error: {ex.Message}", Severity.Error);
                }

                MudDialog.Close(DialogResult.Ok(Dep));
            }
        }

        private async Task Save()
        {
            if (Dep.Id == 0)
            {
                var result = await DepartmentRepository.Create(Dep);
                Snackbar.Add($"Department {result.Name} created", Severity.Success);
            }
            else
            {
                var result = await DepartmentRepository.Update(Dep);
                Snackbar.Add($"Department {result.Name} updated", Severity.Success);
            }
        }
    }
}