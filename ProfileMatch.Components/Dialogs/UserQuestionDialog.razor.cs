using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

using MudBlazor;

using ProfileMatch.Contracts;
using ProfileMatch.Data;
using ProfileMatch.Models.Models;
using ProfileMatch.Models.ViewModels;
using ProfileMatch.Repositories;
using ProfileMatch.Services;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProfileMatch.Components.Dialogs
{
    public partial class UserQuestionDialog : ComponentBase
    {
        [Inject] DataManager<UserClosedAnswer, ApplicationDbContext> UserAnswerRepository { get; set; }
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
        [Inject] ISnackbar Snackbar { get; set; }
        [Inject] DataManager<AnswerOption, ApplicationDbContext> AnswerOptionRepository { get; set; }
        [Parameter] public string CategoryName { get; set; }
        [Parameter] public ClosedQuestion Q { get; set; }
        [Parameter] public UserClosedAnswer UserClosedAnswer { get; set; }
        [Parameter] public string UserId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Q.AnswerOptions = await AnswerOptionRepository.Get(a => a.ClosedQuestionId == Q.Id);
        }

        private bool CanSelect(AnswerOption answerOption)
        {
            if (UserClosedAnswer.AnswerOptionId == answerOption.Id)
            {
                return false;
            }
            if (UserClosedAnswer == null)
            {
                return true;
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
                    ClosedQuestionId = questionId,
                    AnswerOptionId = answerOptionId,
                    ApplicationUserId = UserId,
                    IsConfirmed = false,
                    LastModified = DateTime.Now,
                };
                await UserAnswerRepository.Insert(userAnswer);
            }
            else
            {
                userAnswer.ClosedQuestionId = questionId;
                userAnswer.ApplicationUserId = UserId;
                userAnswer.AnswerOptionId = answerOptionId;
                userAnswer.IsConfirmed = false;
                userAnswer.LastModified = DateTime.Now;
                await UserAnswerRepository.Update(userAnswer);
            }

            MudDialog.Close(DialogResult.Ok(true));
            Snackbar.Add(@L["Answer updated"]);
        }

        [Inject] private IStringLocalizer<LanguageService> L { get; set; }

    }
}
