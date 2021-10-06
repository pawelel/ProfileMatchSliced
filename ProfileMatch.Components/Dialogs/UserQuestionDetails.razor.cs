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
       [Parameter] public UserAnswer UserAnswer { get; set; }
        protected override async Task OnInitializedAsync()
        {
            UserAnswer = await UserAnswerRepository.FindById(UserAnswer);
            Q.AnswerOptions = await AnswerOptionRepository.GetAnswerOptionsForQuestion(Q.Id);

        }
       
        private static bool CanSelect(UserAnswer answer, int optionId)
        {
            if (answer.AnswerOptionId==optionId)
            {
                return false;
            }
            return true;
        }
    }
}
