using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Localization;

using MudBlazor;

using ProfileMatch.Components.Dialogs;
using ProfileMatch.Contracts;
using ProfileMatch.Models.Models;
using ProfileMatch.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileMatch.Components.User
{
    public partial class UserQuestionList : ComponentBase
    {
        [CascadingParameter] private Task<AuthenticationState> AuthenticationStateTask { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        private IDialogService DialogService { get; set; }

        [Inject]
        private ICategoryRepository CategoryRepository { get; set; }

        [Inject]
        private IQuestionRepository QuestionRepository { get; set; }

        private bool loading;
        [Parameter] public int Id { get; set; }
        private string UserId;
        private List<Question> questions = new();
        private List<Question> questions1;
        private List<Category> categories;
        private HashSet<string> Options { get; set; } = new HashSet<string>() { };

        protected override async Task OnParametersSetAsync()
        {
            loading = true;

            questions = await QuestionRepository.GetActiveQuestionsWithCategoriesAndOptions();

            questions1 = questions;
            loading = false;
        }

        protected override async Task OnInitializedAsync()
        {
            loading = true;
            var authState = await AuthenticationStateTask;
            if (authState.User.Identity.IsAuthenticated)
            {
                UserId = authState.User.Claims.FirstOrDefault().Value;
                categories = await CategoryRepository.GetCategories();
            }
            else
            {
                NavigationManager.NavigateTo("Identity/Account/Login", true);
            }

            loading = false;
        }

        private bool dense = true;
        private bool hover = true;
        private bool bordered = true;
        private bool striped = false;
        private string searchString1 = "";

        private bool FilterFunc1(Question question) => FilterFunc(question, searchString1);

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

        private List<Question> GetQuestions()
        {
            if (Options.Count == 0)
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

        [Inject]
        private IStringLocalizer<LanguageService> L { get; set; }
    }
}