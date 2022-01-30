using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;


using ProfileMatch.Data;
using ProfileMatch.Models.Models;
using ProfileMatch.Repositories;
using ProfileMatch.Services;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileMatch.Components.Manager.Dialogs
{
    public partial class ManagerQuestionDisplay : ComponentBase
    {

        private List<AnswerOption> _qAnswerOptions;
        [Inject] IUnitOfWork UnitOfWork { get; set; }
        [Parameter] public ClosedQuestion Q { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Q = await UnitOfWork.ClosedQuestions.GetOne(q=>q.Id==Q.Id, include:src=>src.Include(q=>q.Category).Include(q=>q.Category));
           
            _qAnswerOptions = await UnitOfWork.AnswerOptions.Get(a => a.ClosedQuestionId == Q.Id);
        }
    }
}