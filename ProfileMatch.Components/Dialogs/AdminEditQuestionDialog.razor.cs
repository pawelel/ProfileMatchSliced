using System.Threading.Tasks;

using System;
using Microsoft.AspNetCore.Components;
using MudBlazor;

using ProfileMatch.Contracts;

using ProfileMatch.Models.Models;
using ProfileMatch.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace ProfileMatch.Components.Dialogs
{
    public partial class AdminEditQuestionDialog : ComponentBase
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

        [Inject] public IQuestionRepository QuestionRepository { get; set; }
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
            if (Q.Id == 0 && !await QuestionRepository.IsDuplicated(Q))
            {
                var result = await QuestionRepository.Create(Q);
                Snackbar.Add($"Question {result.Name} created", Severity.Success);
            }
            else if (!await QuestionRepository.IsDuplicated(Q))
            {
                await QuestionRepository.Update(Q);
                Snackbar.Add($"Question {Q.Name} updated", Severity.Success);
            }
            else
            {
                Snackbar.Add($"Question {Q.Name} for this category already exists.", Severity.Error);
            }
        }
    }
}