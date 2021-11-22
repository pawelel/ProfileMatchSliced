using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

using MudBlazor;

using ProfileMatch.Components.Dialogs;
using ProfileMatch.Contracts;
using ProfileMatch.Data;
using ProfileMatch.Models.Models;
using ProfileMatch.Repositories;
using ProfileMatch.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileMatch.Components.User
{
    public partial class UserQuestionList : ComponentBase
    {
        private bool bordered = true;
        private List<Category> categories;
        private bool dense = true;
        private bool hover = true;
        private bool loading;
        private List<Question> questions = new();
        private List<Question> questions1;
        private string searchString1 = "";
        private bool striped = false;
        private string UserId;
        [Parameter] public int Id { get; set; }
        [CascadingParameter] private Task<AuthenticationState> AuthenticationStateTask { get; set; }

        [Inject] DataManager<Category, ApplicationDbContext> CategoryRepository { get; set; }

        [Inject] private IDialogService DialogService { get; set; }

        [Inject] private IStringLocalizer<LanguageService> L { get; set; }

        [Inject] private NavigationManager NavigationManager { get; set; }
        private IEnumerable<string> Options { get; set; } = new HashSet<string>() { };

        [Inject] DataManager<Question, ApplicationDbContext> QuestionRepository { get; set; }
        protected override async Task OnInitializedAsync()
        {
            loading = true;
            var authState = await AuthenticationStateTask;
            if (authState.User.Identity.IsAuthenticated)
            {
                UserId = authState.User.Claims.FirstOrDefault().Value;
                categories = await CategoryRepository.Get();
            }
            else
            {
                NavigationManager.NavigateTo("Identity/Account/Login", true);
            }

            loading = false;
        }

        protected override async Task OnParametersSetAsync()
        {
            loading = true;

            questions = await QuestionRepository.Get(q => q.IsActive == true, include: src => src.Include(q => q.AnswerOptions).Include(q => q.Category));
            questions1 = questions;
            loading = false;
        }
        private static bool FilterFunc(Question question, string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (question.Category.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (question.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            return false;
        }

        private bool FilterFunc1(Question question) => FilterFunc(question, searchString1);
        private List<Question> GetQuestions()
        {
            if (!Options.Any())
            {
                questions1 = questions;
            }
            else
            {
                questions1 = (from q in questions
                              from o in Options
                              where q.Category.Name == o
                              select q).ToList();
            }
            return questions1;
        }

        private int ShowLevel(Question question)
        {
            //find user answer
            // select level for answer option and user answer
            UserAnswer userAnswer = new()
            {
                QuestionId = question.Id,
                AnswerOptionId = null,
                SupervisorId = null,
                ApplicationUserId = UserId,
                IsConfirmed = false
            };
            var query1 = (from a in question.UserAnswers
                          where a is not null
                          where a.ApplicationUserId == UserId
                          select a).Any();
            if (query1)
            {
                userAnswer = question.UserAnswers.Find(a => a.ApplicationUserId == UserId);
            }

            var query2 = question.AnswerOptions.FirstOrDefault(o => o.Id == userAnswer.AnswerOptionId);
            if (query2 == null)
            {
                return 0;
            }
            else
            {
                return query2.Level;
            }
        }

        private async Task UserAnswerDialog(Question question)
        {
            DialogOptions maxWidth = new() { MaxWidth = MaxWidth.Large, FullWidth = true };
            var parameters = new DialogParameters
            {
                ["Q"] = question,
                ["UserId"] = UserId
            };
            var dialog = DialogService.Show<UserQuestionDialog>($"{question.Name}", parameters, maxWidth);
            var data = (await dialog.Result).Data;
            var answer = (UserAnswer)data;
            var a = question.UserAnswers.FirstOrDefault(u => u.ApplicationUserId == UserId);
            var index = question.UserAnswers.IndexOf(a);
            if (index != -1)
                question.UserAnswers[index] = answer;
            else
            {
                question.UserAnswers.Add(answer);
            }
        }
    }
}