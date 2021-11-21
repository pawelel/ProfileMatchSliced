using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

using MudBlazor;

using ProfileMatch.Contracts;
using ProfileMatch.Data;
using ProfileMatch.Models.Models;
using ProfileMatch.Repositories;
using ProfileMatch.Services;

using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileMatch.Components.Dialogs
{
    public partial class AdminQuestionDialog : ComponentBase
    {
        [Inject] private ISnackbar Snackbar { get; set; }
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
        [Parameter] public Question Q { get; set; } = new();
        [Parameter] public int CategoryId { get; set; }

        public int QuestionId { get; set; }
        public string TempName { get; set; }
        public string TempDescription { get; set; }
        public bool TempIsActive { get; set; }
        public bool CanActivateState { get; set; } = false;

        protected override void OnInitialized()
        {
            TempName = Q.Name;
            TempDescription = Q.Description;
        }

        [Inject] DataManager<Question, ApplicationDbContext> QuestionRepository { get; set; }
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
                Q.Name = TempName;
                Q.Description = TempDescription;
                Q.CategoryId = CategoryId;
                Q.IsActive = TempIsActive;
                try
                {
                    await Save();
                }
                catch (Exception ex)
                {
                    Snackbar.Add($"There was an error: {ex.Message}", Severity.Error);
                }

                MudDialog.Close(DialogResult.Ok(Q));
            }
        }

        private async Task Save()
        {//has any other question the same name in the category?
            var exists = (await QuestionRepository.Get(q => q.Name.Contains(Q.Name))).Any();
            if (Q.Id == 0 && !exists)
            {
                var result = await QuestionRepository.Insert(Q);
                Snackbar.Add($"Question {result.Name} created", Severity.Success);
            }
            else if (!exists)
            {
                await QuestionRepository.Update(Q);
                Snackbar.Add($"Question {Q.Name} updated", Severity.Success);
            }
            else
            {
                Snackbar.Add($"Question {Q.Name} already exists.", Severity.Error);
            }
        }

        [Inject]
        private IStringLocalizer<LanguageService> L { get; set; }
    }
}