using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

using MudBlazor;

using ProfileMatch.Data;
using ProfileMatch.Models.Models;
using ProfileMatch.Repositories;
using ProfileMatch.Services;

using System.Threading.Tasks;

using System;

namespace ProfileMatch.Components.Dialogs
{
    public partial class AdminOpenQuestionDialog : ComponentBase
    {
        [Inject] private ISnackbar Snackbar { get; set; }
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
        [Parameter] public OpenQuestion EditedOpenQuestion { get; set; } = new();
        public string TempName { get; set; }
        public string TempDescription { get; set; }

        protected override void OnInitialized()
        {
            TempName = EditedOpenQuestion.Name;
            TempDescription = EditedOpenQuestion.Description;
        }

        [Inject] DataManager<OpenQuestion, ApplicationDbContext> OpenQuestionRepository { get; set; }

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
                EditedOpenQuestion.Name = TempName;
                EditedOpenQuestion.Description = TempDescription;
                try
                {
                    await Save();
                }
                catch (Exception ex)
                {
                    Snackbar.Add($"There was an error: {ex.Message}", Severity.Error);
                }

                MudDialog.Close(DialogResult.Ok(EditedOpenQuestion));
            }
        }

        private async Task Save()
        {
            if (EditedOpenQuestion.Id == 0)
            {
                var result = await OpenQuestionRepository.Insert(EditedOpenQuestion);
                Snackbar.Add($"Note {result.Name} created", Severity.Success);
            }
            else
            {
                var result = await OpenQuestionRepository.Update(EditedOpenQuestion);
                Snackbar.Add($"Note {result.Name} updated", Severity.Success);
            }
        }

        [Inject] private IStringLocalizer<LanguageService> L { get; set; }
    }
}