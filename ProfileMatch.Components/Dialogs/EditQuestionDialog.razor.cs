using System.Threading.Tasks;

using System;
using Microsoft.AspNetCore.Components;
using MudBlazor;

using ProfileMatch.Contracts;

using ProfileMatch.Models.Models;
using ProfileMatch.Repositories;
using System.Collections.Generic;

namespace ProfileMatch.Components.Dialogs
{
    public partial class EditQuestionDialog : ComponentBase
    {
        [Inject]
        IAnswerOptionRepository AnswerOptionRepository { get; set; }
        [Inject] private ISnackbar Snackbar { get; set; }
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
        [Parameter] public Question Q { get; set; } = new();
        [Parameter] public int CategoryId { get; set; }

        public int QuestionId { get; set; }
        public int AoLevel { get; set; }
        public string AoDescription { get; set; }
        public string TempName { get; set; }
        public string TempDescription { get; set; }

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
        private async Task<List<AnswerOption>> AddLevels(Question question)
        {
            List<AnswerOption> answerOptions = new();
             answerOptions = question.AnswerOptions;
            if (answerOptions == null)
            {
                for (int i = 1; i < 6; i++)
                {
                    AnswerOption lvl = new()
                    {
                        QuestionId = question.Id,
                        Description = string.Empty,
                        Level = i
                    };
                    await AnswerOptionRepository.Create(lvl);
                    answerOptions.Add(lvl);
                }
            }
            StateHasChanged();
            return answerOptions;
        }

        protected async Task HandleSave()
        {
            await Form.Validate();
            if (Form.IsValid)
            {
               Q.AnswerOptions = await AddLevels(Q);
                Q.Name = TempName;
                Q.Description = TempDescription;
                Q.CategoryId = CategoryId;
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
        {
            if (Q.Id == 0)
            {
                var result = await QuestionRepository.Create(Q);
                Snackbar.Add($"Question {result.Name} created", Severity.Success);
            }
            else
            {
                var result = await QuestionRepository.Update(Q);
                Snackbar.Add($"Question {result.Name} updated", Severity.Success);
            }
        }
    }
}