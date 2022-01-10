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
        [Inject] IDialogService DialogService { get; set; }
        [Inject] DataManager<Category, ApplicationDbContext> CategoryRepository { get; set; }
        [Inject] DataManager<UserCategory, ApplicationDbContext> UserCategoryRepository { get; set; }
        [Inject] DataManager<ApplicationUser, ApplicationDbContext> UserRepository { get; set; }
        [Inject] DataManager<UserClosedAnswer, ApplicationDbContext> UserAnswerRepository { get; set; }
        [Inject] DataManager<AnswerOption, ApplicationDbContext> AnswerOptionRepository { get; set; }
        [Inject] DataManager<ClosedQuestion, ApplicationDbContext> ClosedQuestionRepository { get; set; }


        private bool loading;
        [Parameter] public int Id { get; set; }
        private List<QuestionUserLevelVM> questionUserLevels = new();
        private List<Category> categories;
        List<UserClosedAnswer> userAnswers;
        private List<ApplicationUser> users;
        List<AnswerOption> answerOptions;
        List<UserCategory> userCategories;
        List<ClosedQuestion> questions;
        private IEnumerable<string> Cats { get; set; } = new HashSet<string>() { };
        string searchString;
        protected override async Task OnInitializedAsync()
        {
            loading = true;
            await LoadData();

            loading = false;
        }

        private async Task LoadData()
        {
            categories = await CategoryRepository.Get();
            answerOptions = await AnswerOptionRepository.Get();
            userCategories = await UserCategoryRepository.Get();
            questions = await ClosedQuestionRepository.Get(include: src => src.Include(q => q.Category));
            userAnswers = await UserAnswerRepository.Get();
            users = await UserRepository.Get();
            questionUserLevels = (from q in questions
                                  join ua in userAnswers
                                  on q.Id
                                  equals ua.ClosedQuestionId
                                  join u in users
                                  on ua.ApplicationUserId
                                  equals u.Id
                                  join ao in answerOptions
                                  on ua.AnswerOptionId
                                  equals ao.Id
                                  join c in categories
                                  on q.CategoryId
                                  equals c.Id
                                  join uc in userCategories
                                  on new { ua.ApplicationUserId, q.CategoryId }
                                  equals new { uc.ApplicationUserId, uc.CategoryId }
                                  where uc.CategoryId == q.CategoryId
                                  where uc.ApplicationUserId == ua.ApplicationUserId
                                  select new QuestionUserLevelVM()
                                  {
                                      CategoryId = c.Id,
                                      CategoryName = c.Name,
                                      CategoryNamePl = c.NamePl,
                                      FirstName = u.FirstName,
                                      LastName = u.LastName,
                                      Description = q.Description,
                                      DescriptionPl = q.DescriptionPl,
                                      Level = ao.Level,
                                      ClosedQuestionId = q.Id,
                                      UserId = u.Id,
                                      QuestionName = q.Name,
                                      QuestionNamePl = q.NamePl,
                                      IsUserCategory = uc.IsSelected ? 1 : 0
                                  }).ToList();
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
        private async Task QuestionDisplay(int id)
        {
            var question1 = await ClosedQuestionRepository.GetOne(q => q.Id == id);
            DialogOptions maxWidth = new() { MaxWidth = MaxWidth.Small, FullWidth = true };
            var parameters = new DialogParameters { ["Q"] = question1 };
            DialogService.Show<ManagerQuestionDisplay>($"{question1.Name}", parameters, maxWidth);
        }

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