using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

using MudBlazor;

using ProfileMatch.Contracts;
using ProfileMatch.Data;
using ProfileMatch.Models.Models;
using ProfileMatch.Repositories;
using ProfileMatch.Services;

using System;
using System.Threading.Tasks;

namespace ProfileMatch.Components.Dialogs
{
    public partial class UserQuestionDialog : ComponentBase
    {
        [Inject] DataManager<UserAnswer, ApplicationDbContext> UserAnswerRepository { get; set; }
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
        [Inject] ISnackbar Snackbar { get; set; }
        [Inject] DataManager<AnswerOption, ApplicationDbContext> AnswerOptionRepository { get; set; }

        [Parameter] public Question Q { get; set; }
        [Parameter] public UserAnswer UserAnswer { get; set; } = new();
        [Parameter] public string UserId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            UserAnswer = await UserAnswerRepository.GetById(UserAnswer.ApplicationUserId, UserAnswer.QuestionId);
            //FindById(UserAnswer);
            Q.AnswerOptions = await AnswerOptionRepository.Get(a=>a.QuestionId==Q.Id);
        }

        private bool CanSelect(AnswerOption answerOption)
        {
            if (UserAnswer == null)
            {
                return true;
            }
            else if (UserAnswer.AnswerOptionId == answerOption.Id)
            {
                return false;
            }
            return true;
        }

        private async Task SelectLevelAsync(string UserId, int answerOptionId, int questionId)
        {
            var userAnswer = await UserAnswerRepository.GetById(UserId, questionId);
            if (userAnswer == null)
            {
                userAnswer = new()
                {
                    QuestionId = questionId,
                    AnswerOptionId = answerOptionId,
                    ApplicationUserId = UserId,
                    IsConfirmed = false,
                    LastModified = DateTime.Now,
                };
                await UserAnswerRepository.Insert(userAnswer);
            }
            else
            {
                userAnswer.QuestionId = questionId;
                userAnswer.ApplicationUserId = UserId;
                userAnswer.AnswerOptionId = answerOptionId;
                userAnswer.IsConfirmed = false;
                userAnswer.LastModified = DateTime.Now;
                await UserAnswerRepository.Update(userAnswer);
            }
            MudDialog.Close(DialogResult.Ok(userAnswer));
            Snackbar.Add("Answer updated");
        }

        [Inject]
        private IStringLocalizer<LanguageService> L { get; set; }
    }
}