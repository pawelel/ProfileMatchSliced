using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

using ProfileMatch.Contracts;
using ProfileMatch.Data;
using ProfileMatch.Models.Models;
using ProfileMatch.Repositories;
using ProfileMatch.Services;

using System.Linq;
using System.Threading.Tasks;

namespace ProfileMatch.Components.Dialogs
{
    public partial class ManagerQuestionDisplay : ComponentBase
    {
        [Inject] DataManager<AnswerOption, ApplicationDbContext> AnswerOptionRepository { get; set; }
        [Inject] DataManager<Question, ApplicationDbContext> QuestionRepository { get; set;}
        [Inject] DataManager<Category, ApplicationDbContext> CategoryRepository { get; set; }
        [Parameter] public Question Q { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Q = await QuestionRepository.GetById(Q.Id);
           
            Q.AnswerOptions = await AnswerOptionRepository.Get(a => a.QuestionId == Q.Id);
        }
    }
}