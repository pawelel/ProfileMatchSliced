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

namespace ProfileMatch.Components.Dialogs
{
    public partial class UserQuestionDetails : ComponentBase
    {
        [Inject]
        IUserAnswerRepository UserAnswerRepository { get; set; }
        [Inject]
        public IAnswerOptionRepository AnswerOptionRepository { get; set; }
        [Parameter] public Question Q { get; set; }
        [Parameter] public UserAnswer UserAnswer { get; set; } = new();
        [Parameter] public string UserId { get; set; }
        protected override async Task OnInitializedAsync()
        {
            UserAnswer = await UserAnswerRepository.FindById(UserAnswer);
            Q.AnswerOptions = await AnswerOptionRepository.GetAnswerOptionsForQuestion(Q.Id);
        }

        private bool CanSelect(AnswerOption answerOption)
        {//has user userAnswer this answerOption on this question
            UserAnswer = UserAnswerRepository.FindById(UserId, answerOption.QuestionId);
            if (UserAnswer==null)
            {
                return true;
            }else  if (UserAnswer.AnswerOptionId == answerOption.Id)
            {
                return false;
            }
            return true;
        }
       async Task<int> SelectLevelAsync(string UserId, int answerOptionId, int questionId)
        {var userAnswer = await UserAnswerRepository.GetUserAnswer(UserId, questionId);
            if (userAnswer == null)
            {

                userAnswer = new()
                {
                    QuestionId = questionId,
                    SupervisorId = null,
                    AnswerOptionId = answerOptionId,
                    ApplicationUserId = UserId,
                    IsConfirmed = false,
                    LastModified = DateTime.Now
                };
               await UserAnswerRepository.Create(userAnswer);
            }
            else
            {
                userAnswer.QuestionId = questionId;
                userAnswer.ApplicationUserId = UserId;
                userAnswer.AnswerOptionId = answerOptionId;
                userAnswer.IsConfirmed = false;
                userAnswer.LastModified = DateTime.Now;
                userAnswer.SupervisorId = null;
                await UserAnswerRepository.Update(userAnswer);
            }
            var answer = await AnswerOptionRepository.FindByUserIdAndQuestionId(userAnswer.ApplicationUserId, questionId);
            return answer.Level;
        }
    }
}
