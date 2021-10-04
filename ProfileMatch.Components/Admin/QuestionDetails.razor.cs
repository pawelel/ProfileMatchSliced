using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;

using MudBlazor;

using ProfileMatch.Components.Dialogs;
using ProfileMatch.Contracts;
using ProfileMatch.Models.Models;

using ProfileMatch.Repositories;

namespace ProfileMatch.Components.Admin
{
    public partial class QuestionDetails : ComponentBase
    {
        [Inject]
        IDialogService DialogService { get; set; }
        [Inject]
        public IAnswerOptionRepository AnswerOptionRepository { get; set; }
        [Inject]
        IQuestionRepository QuestionRepository { get; set; }
        [Parameter] public Question Q { get; set; }
        List<AnswerOption> answerOptions = new();
        private async Task AddLevels(Question question)
        {               
            for (int i = 1; i < 6; i++)
            {
                AnswerOption lvl = new()
                {
                    QuestionId = question.Id,
                    Description = string.Empty,
                    Level = i
                };
                answerOptions.Add (await AnswerOptionRepository.Create(lvl));
                
            }
            question.AnswerOptions = answerOptions;
           
        }
        private async Task EditQuestionDialog(Question question)
        {
            var parameters = new DialogParameters { ["Q"] = question };
            var dialog = DialogService.Show<EditQuestionDialog>($"Edit Question {question.Name}", parameters);
            await dialog.Result;
        }
        private async Task EditLevelDialog(AnswerOption answerOption)
        {
            DialogOptions maxWidth = new DialogOptions() { MaxWidth = MaxWidth.ExtraExtraLarge, FullWidth = true };
            var parameters = new DialogParameters { ["O"] = answerOption };
            var dialog = DialogService.Show<EditLevelDialog>($"Edit Answer Level {answerOption.Level}", parameters,maxWidth);
            await dialog.Result;
        }
        protected override async Task OnInitializedAsync()
        {
            answerOptions = Q.AnswerOptions;
            if (answerOptions==null)
            {
              await  AddLevels(Q);
            }
        }

    }
}
