using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

using ProfileMatch.Contracts;
using ProfileMatch.Data;
using ProfileMatch.Models.Models;
using ProfileMatch.Repositories;
using ProfileMatch.Services;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileMatch.Components.Dialogs
{
    public partial class ManagerQuestionDisplay : ComponentBase
    {
        List<AnswerOption> QAnswerOptions;
        [Inject] DataManager<AnswerOption, ApplicationDbContext> AnswerOptionRepository { get; set; }
        [Inject] DataManager<Question, ApplicationDbContext> QuestionRepository { get; set;}
        [Parameter] public Question Q { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Q = await QuestionRepository.GetOne(q=>q.Id==Q.Id, include:src=>src.Include(q=>q.Category).Include(q=>q.Category));
           
            QAnswerOptions = await AnswerOptionRepository.Get(a => a.QuestionId == Q.Id);
        }
    }
}