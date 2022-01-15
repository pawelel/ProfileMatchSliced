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
        [Inject] IStringLocalizer<LanguageService> L { get; set; }

        private List<AnswerOption> QAnswerOptions;
        [Inject] DataManager<AnswerOption, ApplicationDbContext> AnswerOptionRepository { get; set; }
        [Inject] DataManager<ClosedQuestion, ApplicationDbContext> ClosedQuestionRepository { get; set;}
        [Parameter] public ClosedQuestion Q { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Q = await ClosedQuestionRepository.GetOne(q=>q.Id==Q.Id, include:src=>src.Include(q=>q.Category).Include(q=>q.Category));
           
            QAnswerOptions = await AnswerOptionRepository.Get(a => a.ClosedQuestionId == Q.Id);
        }
    }
}