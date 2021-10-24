using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

using MudBlazor;

using ProfileMatch.Contracts;
using ProfileMatch.Models.Models;
using ProfileMatch.Services;

namespace ProfileMatch.Components.Dialogs
{
    public partial class AdminQuestionDisplay : ComponentBase
    {
        [Inject]
        private IDialogService DialogService { get; set; }

        [Inject]
        public IAnswerOptionRepository AnswerOptionRepository { get; set; }

        [Inject]
        public IQuestionRepository QuestionRepository { get; set; }

        [Parameter] public Question Q { get; set; }

        private async Task AddLevels(Question question)
        {
            for (int i = 1; i < 6; i++)
            {
                question.AnswerOptions.Add(
                     await AnswerOptionRepository.Create(new AnswerOption()
                     {
                         QuestionId = question.Id,
                         Description = string.Empty,
                         Level = i
                     })
                );
            }
        }

        private async Task EditLevelDialog(AnswerOption answerOption)
        {
            DialogOptions maxWidth = new() { MaxWidth = MaxWidth.Large, FullWidth = true };
            var parameters = new DialogParameters { ["O"] = answerOption };
            var dialog = DialogService.Show<AdminLevelDialog>($"Edit Answer Level {answerOption.Level}", parameters, maxWidth);
            await dialog.Result;
        }

        protected override async Task OnInitializedAsync()
        {
            Q.AnswerOptions = await AnswerOptionRepository.GetAnswerOptionsForQuestion(Q.Id);

            if (Q.AnswerOptions.Count == 0 || Q.AnswerOptions == null)
            {
                await AddLevels(Q);
            }
        }

        private async Task<bool> IsActive()
        {
            Q.IsActive = !Q.IsActive;
            await QuestionRepository.Update(Q);
            StateHasChanged();
            return Q.IsActive;
        }

        private static bool CanActivate(Question question)
        {//does list of answerOptions exist and any answerOption is nullOwWhiteSpace
            if (question.AnswerOptions is not null)
            {
                bool hasEmptyDescription = question.AnswerOptions.Any(ao => string.IsNullOrWhiteSpace(ao.Description));
                return !hasEmptyDescription;
            }
            return false;
        }
        [Inject]
        IStringLocalizer<LanguageService> L { get; set; }
    }
}