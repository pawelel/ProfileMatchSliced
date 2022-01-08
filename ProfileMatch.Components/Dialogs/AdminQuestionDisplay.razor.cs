using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

using MudBlazor;

using ProfileMatch.Contracts;
using ProfileMatch.Data;
using ProfileMatch.Models.Models;
using ProfileMatch.Repositories;
using ProfileMatch.Services;

using System.Linq;
using System.Threading.Tasks;

namespace ProfileMatch.Components.Dialogs
{
    public partial class AdminQuestionDisplay : ComponentBase
    {
        [Inject]
        private IDialogService DialogService { get; set; }

        [Inject] DataManager<AnswerOption, ApplicationDbContext> AnswerOptionRepository { get; set; }

        [Inject] DataManager<ClosedQuestion, ApplicationDbContext> ClosedQuestionRepository { get; set; }

        [Parameter] public ClosedQuestion Q { get; set; }

        private async Task AddLevels(ClosedQuestion question)
        {
            for (int i = 1; i < 6; i++)
            {
                question.AnswerOptions.Add(
                     await AnswerOptionRepository.Insert(new AnswerOption()
                     {
                         ClosedQuestionId = question.Id,
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
            var dialog = DialogService.Show<AdminLevelDialog>(L["Edit Answer Level"]+$" {answerOption.Level}", parameters, maxWidth);
            await dialog.Result;
        }

        protected override async Task OnInitializedAsync()
        {
            Q.AnswerOptions = await AnswerOptionRepository.Get(q => q.ClosedQuestionId == Q.Id);

            if (Q.AnswerOptions.Count == 0 || Q.AnswerOptions == null)
            {
                await AddLevels(Q);
            }
        }

        private async Task<bool> IsActive()
        {
            Q.IsActive = !Q.IsActive;
            await ClosedQuestionRepository.Update(Q);
            StateHasChanged();
            return Q.IsActive;
        }

        private static bool CanActivate(ClosedQuestion question)
        {//does list of answerOptions exist and any answerOption is nullOwWhiteSpace
            if (question.AnswerOptions is not null)
            {
                bool hasEmptyDescription = question.AnswerOptions.Any(ao => string.IsNullOrWhiteSpace(ao.Description));
                return !hasEmptyDescription;
            }
            return false;
        }

        [Inject]
        private IStringLocalizer<LanguageService> L { get; set; }
    }
}