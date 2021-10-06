using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using MudBlazor;
using ProfileMatch.Contracts;

using ProfileMatch.Models.Models;

namespace ProfileMatch.Components.Dialogs
{
    public partial class AdminEditLevelDialog : ComponentBase
    {
        [Inject] private ISnackbar Snackbar { get; set; }
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
        [Parameter] public AnswerOption O { get; set; } = new();
        public string TempDescription { get; set; }

        protected override void OnInitialized()
        {

            TempDescription = O.Description;
        }

        [Inject] public IAnswerOptionRepository AnswerOptionRepository { get; set; }

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
         
                O.Description = TempDescription;
                try
                {
                    await Save();
                }
                catch (Exception ex)
                {
                    Snackbar.Add($"There was an error: {ex.Message}", Severity.Error);
                }

                MudDialog.Close(DialogResult.Ok(O));
            }
        }

        private async Task Save()
        {
            if (O.Id == 0)
            {
                var result = await AnswerOptionRepository.Create(O);
                Snackbar.Add($"Answer Option created", Severity.Success);
            }
            else
            {
                await AnswerOptionRepository.Update(O);
                Snackbar.Add($"Answer Option updated", Severity.Success);
            }
        }
    }
}