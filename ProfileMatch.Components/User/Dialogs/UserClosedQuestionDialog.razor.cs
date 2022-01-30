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
        [Inject] IUnitOfWork UnitOfWork { get; set; }
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
        [Inject] ISnackbar Snackbar { get; set; }
       
        [Parameter] public QuestionUserLevelVM Q { get; set; }
        int _userLevel;
        [Parameter] public string UserId { get; set; }
        AnswerOption _tempAnswerOption;
        List<AnswerOption> _answerOptions;
        protected override async Task OnInitializedAsync()
        { 
            if (Q.Level == 0||Q==null) Q.Level = 1;
            _userLevel = Q.Level;
            
            _answerOptions = await UnitOfWork.AnswerOptions.Get(q=>q.ClosedQuestionId==Q.ClosedQuestionId);
            
            
            _tempAnswerOption = _answerOptions.FirstOrDefault(q => q.ClosedQuestionId==Q.ClosedQuestionId&&q.Level==1);
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
            var userAnswer = await UnitOfWork.UserClosedAnswers.GetById(UserId, _tempAnswerOption.ClosedQuestionId);
            if (userAnswer == null)
            {
                userAnswer = new()
                {
                    ClosedQuestionId = _tempAnswerOption.ClosedQuestionId,
                    AnswerOptionId = _tempAnswerOption.Id,
                    ApplicationUserId = UserId,
                    IsConfirmed = false,
                    LastModified = DateTime.Now,
                };

                await UnitOfWork.UserClosedAnswers.Insert(userAnswer);
            }
            else
            {
                userAnswer.ClosedQuestionId = _tempAnswerOption.ClosedQuestionId;
                userAnswer.ApplicationUserId = UserId;
                userAnswer.AnswerOptionId = _tempAnswerOption.Id;
                userAnswer.IsConfirmed = false;
                userAnswer.LastModified = DateTime.Now;
                await UnitOfWork.UserClosedAnswers.Update(userAnswer);
            }

            MudDialog.Close(DialogResult.Ok(true));
            Snackbar.Add(@L["Answer updated"] + $": {title}", Severity.Success);
        }


        async Task SetOption(AnswerOption answerOption)
        {
            if (answerOption == null || answerOption.Id == 0)
            {
                _tempAnswerOption = await UnitOfWork.AnswerOptions.GetOne(a => a.ClosedQuestionId == Q.ClosedQuestionId && a.Level == 1);
            }
            else
            {
                _tempAnswerOption = answerOption;
            }
        }
    }
}
