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
    public partial class ManagerTestTable : ComponentBase
    {
        [Inject]
        private IDialogService DialogService { get; set; }

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
        private IEnumerable<string> Ppl { get; set; } = new HashSet<string>() { };

        protected override async Task OnInitializedAsync()
        {
            loading = true;
            categories = await CategoryRepository.Get();
            answerOptions = await AnswerOptionRepository.Get();
            questions = await QuestionRepository.Get(include: src => src.Include(q => q.Category));
            userAnswers = await UserAnswerRepository.Get();
            users = await UserRepository.Get();
            questionUserLevels =  (from q in questions  join ua in userAnswers on q.Id equals ua.QuestionId join u in users on ua.ApplicationUserId equals u.Id join ao in answerOptions on ua.AnswerOptionId equals ao.Id join c in categories on q.CategoryId equals c.Id select new QuestionUserLevelVM() { CategoryId = c.Id, CategoryName = c.Name, FirstName = u.FirstName, LastName =u.LastName, Level = ao.Level, QuestionId = q.Id, UserId = u.Id, QuestionName = q.Name }).ToList();
            loading = false;
        }

        private bool dense = true;
        private bool hover = true;
        private bool bordered = true;
        private bool striped = false;
        private string searchString1 = "";

        //private bool FilterFunc2(QuestionUserLevelVM question) => FilterFunc(question, searchString1);
        private bool FilterFunc1(QuestionUserLevelVM question) => FilterFunc(question, searchString1);

        private static bool FilterFunc(QuestionUserLevelVM question, string searchString)
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
        }

        private async Task QuestionDisplay(int id)
        {
            var question1 = await QuestionRepository.GetById(id);
            DialogOptions maxWidth = new() { MaxWidth = MaxWidth.Large, FullWidth = true };
            var parameters = new DialogParameters { ["Q"] = question1 };
            DialogService.Show<ManagerQuestionDisplay>($"{question1.Name}", parameters, maxWidth);
        }

        private readonly TableGroupDefinition<QuestionUserLevelVM> _groupDefinition = new()
        {
            GroupName = "Category",
            Indentation = true,
            Expandable = true,
            IsInitiallyExpanded = true,
            Selector = (e) => e.CategoryName,
            InnerGroup = new TableGroupDefinition<QuestionUserLevelVM>()
            {
                GroupName = "Question",
                Expandable = true,
                Selector = (e) => e.QuestionName
            }
        };

        private List<QuestionUserLevelVM> GetPpl(List<QuestionUserLevelVM> questions)
        {
            List<QuestionUserLevelVM> ppl = new();
            if (Ppl.Any())
            {
                ppl = (from q in questions
                       from p in Ppl
                       where q.FullName == p
                       select q).ToList();
            }
            else
            {
                ppl = questions;
            }
            return ppl;
        }

        private List<QuestionUserLevelVM> GetCategoriesAndQuestions()
        {
            List<QuestionUserLevelVM> qs = new();

            qs = (from q in questionUserLevels
                  from c in Cats
                  where q.CategoryName == c
                  select q).ToList();
            qs = GetPpl(qs);
            return qs;
        }

        [Inject]
        private IStringLocalizer<LanguageService> L { get; set; }
    }
}