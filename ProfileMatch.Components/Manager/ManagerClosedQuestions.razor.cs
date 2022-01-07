using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

using MudBlazor;

using ProfileMatch.Components.Dialogs;
using ProfileMatch.Contracts;
using ProfileMatch.Data;
using ProfileMatch.Models.Models;
using ProfileMatch.Models.ViewModels;
using ProfileMatch.Repositories;
using ProfileMatch.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileMatch.Components.Manager
{
    public partial class ManagerClosedQuestions : ComponentBase
    {
        [Inject] DataManager<Category, ApplicationDbContext> CategoryRepository { get; set; }

        [Inject] DataManager<ApplicationUser, ApplicationDbContext> UserRepository { get; set; }
        [Inject] DataManager<UserAnswer, ApplicationDbContext> UserAnswerRepository { get; set; }
        [Inject] DataManager<AnswerOption, ApplicationDbContext> AnswerOptionRepository { get; set; }


        [Inject] DataManager<Question, ApplicationDbContext> QuestionRepository { get; set; }
 

        private bool loading;
        [Parameter] public int Id { get; set; }
        private List<QuestionUserLevelVM> questionUserLevels = new();
        private List<Category> categories;
        List<UserAnswer> userAnswers;
        private List<ApplicationUser> users;
        List<AnswerOption> answerOptions;
        List<Question> questions;
        private IEnumerable<string> Cats { get; set; } = new HashSet<string>() { };
        string searchString;
        protected override async Task OnInitializedAsync()
        {
            loading = true;
            categories = await CategoryRepository.Get();
            answerOptions = await AnswerOptionRepository.Get();
            questions = await QuestionRepository.Get(include: src => src.Include(q => q.Category));
            userAnswers = await UserAnswerRepository.Get();
            users = await UserRepository.Get();
            questionUserLevels =  (from q in questions  join ua in userAnswers on q.Id equals ua.QuestionId join u in users on ua.ApplicationUserId equals u.Id join ao in answerOptions on ua.AnswerOptionId equals ao.Id join c in categories on q.CategoryId equals c.Id select new QuestionUserLevelVM() { CategoryId = c.Id, CategoryName = c.Name, FirstName = u.FirstName, LastName =u.LastName, Description=q.Description, Level = ao.Level, QuestionId = q.Id, UserId = u.Id, QuestionName = q.Name }).ToList();
            loading = false;
        }
        private Func<QuestionUserLevelVM, bool> QuickFilter => question =>
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (question.CategoryName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (question.QuestionName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (question.FullName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (question.Level.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            return false;
        };
        

        private List<QuestionUserLevelVM> GetCategoriesAndQuestions()
        {
            if (!Cats.Any())
            {
                return questionUserLevels;
            }
            List<QuestionUserLevelVM> qs = new();

            qs = (from q in questionUserLevels
                  from c in Cats
                  where q.CategoryName == c
                  select q).ToList();
            return qs;
        }

        [Inject]
        private IStringLocalizer<LanguageService> L { get; set; }
    }
}