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
    public partial class UserClosedQuestionDialog : ComponentBase
    {
        [Inject] DataManager<UserClosedAnswer, ApplicationDbContext> UserAnswerRepository { get; set; }
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
        [Inject] ISnackbar Snackbar { get; set; }
        [Inject] DataManager<AnswerOption, ApplicationDbContext> AnswerOptionRepository { get; set; }
        [Parameter] public string CategoryName { get; set; }
        [Parameter] public ClosedQuestion Q { get; set; }
        [Parameter] public UserClosedAnswer UserClosedAnswer { get; set; }
        [Parameter] public string UserId { get; set; }
        AnswerOption tempAnswerOption;
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

        private async Task SelectLevelAsync(AnswerOption answerOption)
        {
            string title;
            if (ShareResource.IsEn())
            {
                title = Q.Name;
            }
            else { title = Q.NamePl; }
            var userAnswer = await UserAnswerRepository.GetById(UserId, answerOption.ClosedQuestionId);
            if (userAnswer == null)
            {
                userAnswer = new()
                {
                    ClosedQuestionId = answerOption.ClosedQuestionId,
                    AnswerOptionId = answerOption.Id,
                    ApplicationUserId = UserId,
                    IsConfirmed = false,
                    LastModified = DateTime.Now,
                };
                
                await UserAnswerRepository.Insert(userAnswer);
            }
            else
            {
                userAnswer.ClosedQuestionId = answerOption.ClosedQuestionId;
                userAnswer.ApplicationUserId = UserId;
                userAnswer.AnswerOptionId = answerOption.Id;
                userAnswer.IsConfirmed = false;
                userAnswer.LastModified = DateTime.Now;
                await UserAnswerRepository.Update(userAnswer);
            }

            MudDialog.Close(DialogResult.Ok(true));
            Snackbar.Add(@L["Answer updated"]+ $": {title}", Severity.Success);
        }

        [Inject] private IStringLocalizer<LanguageService> L { get; set; }

       async Task SetOption(AnswerOption answerOption)
        {
            if (answerOption == null || answerOption.Id == 0)
            {
                tempAnswerOption = await AnswerOptionRepository.GetOne(a=>a.ClosedQuestionId==Q.Id&&a.Level==1);
            }
            else
            {
            tempAnswerOption = answerOption;
            }
        }
    }
}
