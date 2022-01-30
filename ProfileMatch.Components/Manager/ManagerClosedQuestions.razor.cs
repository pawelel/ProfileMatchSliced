using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

using MudBlazor;

using ProfileMatch.Components.Manager.Dialogs;
using ProfileMatch.Data;
using ProfileMatch.Models.Entities;
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
     

        [Inject] IUnitOfWork UnitOfWork { get; set; }
        private bool _loading;
        [Parameter] public int Id { get; set; }
        private List<QuestionUserLevelVM> _questionUserLevels = new();
        private List<Category> _categories;
        List<UserClosedAnswer> _userAnswers;
        private List<ApplicationUser> _users;
        List<AnswerOption> _answerOptions;
        List<UserCategory> _userCategories;
        List<ClosedQuestion> _questions;
        private IEnumerable<string> Cats { get; set; } = new HashSet<string>() { };
        string _searchString;
        public ManagerClosedQuestions()
        {

        }
        public ManagerClosedQuestions(bool loading)
        {
            _loading = loading;
        }

        protected override async Task OnInitializedAsync()
        {
            _loading = true;
            await LoadData();

            _loading = false;
        }

        private async Task LoadData()
        {
            _categories = await UnitOfWork.Categories.Get();
            _answerOptions = await UnitOfWork.AnswerOptions.Get();
            _userCategories = await UnitOfWork.UserCategories.Get();
            _questions = await UnitOfWork.ClosedQuestions.Get(include: src => src.Include(q => q.Category));
            _userAnswers = await UnitOfWork.UserClosedAnswers.Get();
            _users = await UnitOfWork.ApplicationUsers.Get();
            _questionUserLevels = (from q in _questions
                                  join ua in _userAnswers
                                  on q.Id
                                  equals ua.ClosedQuestionId
                                  join u in _users
                                  on ua.ApplicationUserId
                                  equals u.Id
                                  join ao in _answerOptions
                                  on ua.AnswerOptionId
                                  equals ao.Id
                                  join c in _categories
                                  on q.CategoryId
                                  equals c.Id
                                  join uc in _userCategories
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
            if (string.IsNullOrWhiteSpace(_searchString))
                return true;
            if (question.FullName.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (question.Level.ToString().Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (ShareResource.IsEn())
            {
            if (question.CategoryName.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (question.QuestionName.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            }
            else
            {
                if (question.CategoryNamePl.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                    return true;
                if (question.QuestionNamePl.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        };
        private async Task QuestionDisplay(int id)
        {
            var question1 = await UnitOfWork.ClosedQuestions.GetOne(q => q.Id == id);
            DialogOptions maxWidth = new() { MaxWidth = MaxWidth.Small, FullWidth = true };
            var parameters = new DialogParameters { ["Q"] = question1 };

            if (ShareResource.IsEn())
            {
                DialogService.Show<ManagerQuestionDisplay>($"{question1.Name}", parameters, maxWidth);
            }
            else
            {
            DialogService.Show<ManagerQuestionDisplay>($"{question1.NamePl}", parameters, maxWidth);
            }
        }

        private List<QuestionUserLevelVM> GetCategoriesAndQuestions()
        {
            if (!Cats.Any())
            {
                return _questionUserLevels;
            }
            List<QuestionUserLevelVM> qs = new();
            if (ShareResource.IsEn())
            {
                qs = (from q in _questionUserLevels
                      from c in Cats
                      where q.CategoryName == c
                      select q).ToList();
                return qs;
            }
            qs = (from q in _questionUserLevels
                  from c in Cats
                  where q.CategoryNamePl == c
                  select q).ToList();
            return qs;
        }

    }
}