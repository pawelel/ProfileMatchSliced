using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

using MudBlazor;


using ProfileMatch.Data;
using ProfileMatch.Models.Models;
using ProfileMatch.Models.ViewModels;
using ProfileMatch.Repositories;
using ProfileMatch.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileMatch.Components.User.Dialogs
{
    public partial class UserClosedQuestionDialog : ComponentBase
    {
        [Inject] DataManager<UserClosedAnswer, ApplicationDbContext> UserAnswerRepository { get; set; }
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
        [Inject] ISnackbar Snackbar { get; set; }
        [Inject] DataManager<AnswerOption, ApplicationDbContext> AnswerOptionRepository { get; set; }
        [Parameter] public QuestionUserLevelVM Q { get; set; }
        int UserLevel;
        [Parameter] public string UserId { get; set; }
        AnswerOption tempAnswerOption;
        List<AnswerOption> answerOptions;
        protected override async Task OnInitializedAsync()
        { 
            if (Q.Level == 0||Q==null) Q.Level = 1;
            UserLevel = Q.Level;
            
            answerOptions = await AnswerOptionRepository.Get(q=>q.ClosedQuestionId==Q.ClosedQuestionId);
            
            
            tempAnswerOption = answerOptions.FirstOrDefault(q => q.ClosedQuestionId==Q.ClosedQuestionId&&q.Level==1);
            UserId = Q.UserId;
        }

        private async Task SelectLevelAsync()
        {
            string title;
            if (ShareResource.IsEn())
            {
                title = Q.QuestionName;
            }
            else { title = Q.QuestionNamePl; }
            var userAnswer = await UserAnswerRepository.GetById(UserId, tempAnswerOption.ClosedQuestionId);
            if (userAnswer == null)
            {
                userAnswer = new()
                {
                    ClosedQuestionId = tempAnswerOption.ClosedQuestionId,
                    AnswerOptionId = tempAnswerOption.Id,
                    ApplicationUserId = UserId,
                    IsConfirmed = false,
                    LastModified = DateTime.Now,
                };

                await UserAnswerRepository.Insert(userAnswer);
            }
            else
            {
                userAnswer.ClosedQuestionId = tempAnswerOption.ClosedQuestionId;
                userAnswer.ApplicationUserId = UserId;
                userAnswer.AnswerOptionId = tempAnswerOption.Id;
                userAnswer.IsConfirmed = false;
                userAnswer.LastModified = DateTime.Now;
                await UserAnswerRepository.Update(userAnswer);
            }

            MudDialog.Close(DialogResult.Ok(true));
            Snackbar.Add(@L["Answer updated"] + $": {title}", Severity.Success);
        }


        async Task SetOption(AnswerOption answerOption)
        {
            if (answerOption == null || answerOption.Id == 0)
            {
                tempAnswerOption = await AnswerOptionRepository.GetOne(a => a.ClosedQuestionId == Q.ClosedQuestionId && a.Level == 1);
            }
            else
            {
                tempAnswerOption = answerOption;
            }
        }
    }
}
