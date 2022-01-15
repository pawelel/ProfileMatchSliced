using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

using MudBlazor;

using ProfileMatch.Data;
using ProfileMatch.Models.Models;
using ProfileMatch.Repositories;
using ProfileMatch.Services;

using System.Threading.Tasks;

using System;

namespace ProfileMatch.Components.Admin.Dialogs
{
    public partial class AdminOpenQuestionDialog : ComponentBase
    {
        [Inject] private ISnackbar Snackbar { get; set; }
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
        [Parameter] public OpenQuestion EditedOpenQuestion { get; set; } = new();
        public string TempName { get; set; }
        public string TempNamePl { get; set; }

        public string TempDescription { get; set; }
        public string TempDescriptionPl { get; set; }
        protected override void OnInitialized()
        {

            TempName = EditedOpenQuestion.Name;
            TempNamePl = EditedOpenQuestion.NamePl;
            TempDescription = EditedOpenQuestion.Description;
            TempDescriptionPl = EditedOpenQuestion.DescriptionPl;
        }

        [Inject] DataManager<OpenQuestion, ApplicationDbContext> OpenQuestionRepository { get; set; }

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
                EditedOpenQuestion.Name = TempName;
                EditedOpenQuestion.NamePl = TempNamePl;
                EditedOpenQuestion.Description = TempDescription;
                EditedOpenQuestion.DescriptionPl = TempDescriptionPl;
                try
                {
                    await Save();
                }
                catch (Exception ex)
                {
                    Snackbar.Add(@L[$"There was an error:"] + $" {ex.Message}", Severity.Error);
                }

                MudDialog.Close(DialogResult.Ok(EditedOpenQuestion));
            }
        }

        private async Task Save()
        {
            string title;
            string updated;
            string created;
            if (ShareResource.IsEn())
            {
                title = $"Question {TempName} has been";
                created = "created";
                updated = "updated";
            }
            else
            {
                title = $"Pytanie {TempNamePl} zostało";
                created = "utworzone";
                updated = "zaktualizowane";
            }
            if (EditedOpenQuestion.Id == 0)
            {
                var result = await OpenQuestionRepository.Insert(EditedOpenQuestion);
                Snackbar.Add( $"{title} {created}" , Severity.Success);
            }
            else
            {
                var result = await OpenQuestionRepository.Update(EditedOpenQuestion);
                Snackbar.Add($"{title} {updated}", Severity.Success);
            }
        }

        [Inject] private IStringLocalizer<LanguageService> L { get; set; }
    }
}