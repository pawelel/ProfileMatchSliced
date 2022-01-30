using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

using MudBlazor;

using ProfileMatch.Components.User.Dialogs;
using ProfileMatch.Data;
using ProfileMatch.Models.Entities;
using ProfileMatch.Models.ViewModels;
using ProfileMatch.Repositories;
using ProfileMatch.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileMatch.Components.User
{
    public partial class UserClosedQuestionsTable : ComponentBase
    {



        private bool _loading;
        private List<ClosedQuestion> _questions;
        private List<Category> _categories;
        private List<AnswerOption> _answerOptions;
        private List<UserClosedAnswer> _userAnswers;

        private string _userId;
        [Parameter] public int Id { get; set; }
        [CascadingParameter] private Task<AuthenticationState> AuthenticationStateTask { get; set; }

        [Inject] IUnitOfWork UnitOfWork { get; set; }

        [Inject] private IDialogService DialogService { get; set; }


        [Inject] private NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            _loading = true;
            var authState = await AuthenticationStateTask;
            if (authState.User.Identity.IsAuthenticated)
            {
                _userId = authState.User.Claims.FirstOrDefault().Value;
                await LoadData();
            }
            else
            {
                NavigationManager.NavigateTo("Identity/Account/Login", true);
            }

            _loading = false;
        }

        private async Task LoadData()
        {
            _categories = await UnitOfWork.Categories.Get();
            _questions = await UnitOfWork.ClosedQuestions.Get(q => q.IsActive == true);
            _userAnswers = await UnitOfWork.UserClosedAnswers.Get(u => u.ApplicationUserId == _userId);
            _answerOptions = await UnitOfWork.AnswerOptions.Get();// need to filter by active question
            _answerOptions = (from q in _questions join o in _answerOptions on q.Id equals o.ClosedQuestionId select o).ToList();
        }

        private string _searchString;

        private Func<QuestionUserLevelVM, bool> QuickFilter => x =>
        {
            if (string.IsNullOrWhiteSpace(_searchString))
                return true;
            if (ShareResource.IsEn())
            {
                if (x.CategoryName.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                    return true;
                if (x.QuestionName.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            else
            {
                if (x.CategoryNamePl.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                    return true;
                if (x.QuestionNamePl.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        };
        private IEnumerable<string> Cats { get; set; } = new HashSet<string>() { };
        private List<QuestionUserLevelVM> QuestionUserLevelVMs()
        {
            foreach (ClosedQuestion q in _questions)
            {
                if (!_userAnswers.Any(a => a.ClosedQuestionId == q.Id))
                {
                    var optionId = _answerOptions.FirstOrDefault(o => o.ClosedQuestionId == q.Id && o.Level == 1).Id;
                    var answer = new UserClosedAnswer()
                    {
                        AnswerOptionId = optionId,
                        ClosedQuestionId = q.Id,
                        ApplicationUserId = _userId,
                        LastModified = DateTime.Now
                    };
                    _userAnswers.Add(answer);
                }
            }
            var data = (from u in _userAnswers
                        where u.ApplicationUserId == _userId
                        join q in _questions on u.ClosedQuestionId equals q.Id
                        join c in _categories on q.CategoryId equals c.Id
                        join a in _answerOptions on u.AnswerOptionId equals a.Id
                        select new QuestionUserLevelVM()
                        {
                            ClosedQuestionId = q.Id,
                            QuestionName = q.Name,
                            QuestionNamePl = q.NamePl,
                            Description = q.Description,
                            DescriptionPl = q.DescriptionPl,
                            CategoryId = c.Id,
                            CategoryName = c.Name,
                            CategoryNamePl = c.NamePl,
                            Level = a.Level,
                            UserId = _userId
                        }
              ).ToList();
            if (!Cats.Any())
            {
                return data;
            }
            List<QuestionUserLevelVM> qs = new();

            if (ShareResource.IsEn())
            {
                qs = (from q in data
                      from c in Cats
                      where q.CategoryName == c
                      select q).ToList();
                return qs;
            }
            else
            {
                qs = (from q in data
                      from c in Cats
                      where q.CategoryNamePl == c
                      select q).ToList();
                return qs;
            }
            
        }
    
        private async Task UserAnswerDialog(QuestionUserLevelVM vM)
        {
            DialogOptions maxWidth = new() { MaxWidth = MaxWidth.Small, FullWidth = true };
            var parameters = new DialogParameters
            {
                ["Q"] = vM
            };
            var title = ShareResource.IsEn() ? $"{vM.CategoryName}: {vM.QuestionName}" : $"{vM.CategoryNamePl}: {vM.QuestionNamePl}";
            var dialog = DialogService.Show<UserClosedQuestionDialog>(title, parameters, maxWidth);
            await dialog.Result;
            await LoadData();
        }
    }
}