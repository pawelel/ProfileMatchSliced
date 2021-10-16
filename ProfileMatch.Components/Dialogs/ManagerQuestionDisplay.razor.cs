using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;

using ProfileMatch.Contracts;
using ProfileMatch.Models.Models;

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