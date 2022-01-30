using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

using MudBlazor;


using ProfileMatch.Data;
using ProfileMatch.Models.Models;
using ProfileMatch.Repositories;
using ProfileMatch.Services;

using System;
using System.Threading.Tasks;

namespace ProfileMatch.Components.Admin.Dialogs
{
    public partial class AdminLevelDialog : ComponentBase
    {
        [Inject] private ISnackbar Snackbar { get; set; }
        [Inject] IUnitOfWork UnitOfWork { get; set; }
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
        [Parameter] public AnswerOption O { get; set; } = new();
        public string TempDescription { get; set; }

        protected override void OnInitialized()
        {
            TempDescription = O.Description;
        }



        private MudForm _form;

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
                O.Description = TempDescription;
                try
                {
                    await Save();
                }
                catch (Exception ex)
                {
                    Snackbar.Add(@L[$"There was an error:"] + $" {ex.Message}", Severity.Error);
                }

                MudDialog.Close(DialogResult.Ok(O));
            }
        }

        private async Task Save()
        {
            if (O.Id == 0)
            {
                var result = await UnitOfWork.AnswerOptions.Insert(O);
                Snackbar.Add(@L["Answer option created"], Severity.Success);
            }
            else
            {
                await UnitOfWork.AnswerOptions.Update(O);
                Snackbar.Add(@L["Answer option updated"], Severity.Success);
            }
        }

    }
}