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
    public partial class ManagerQuestionDisplay : ComponentBase
    {
        [Inject]
        public IAnswerOptionRepository AnswerOptionRepository { get; set; }
        [Inject]
        public IQuestionRepository QuestionRepository { get; set; }
        [Parameter] public Question Q { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Q.AnswerOptions = await AnswerOptionRepository.GetAnswerOptionsForQuestion(Q.Id);
        }
    }
}
