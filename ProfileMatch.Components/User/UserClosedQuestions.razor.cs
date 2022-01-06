using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
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

namespace ProfileMatch.Components.User
{
    public partial class UserClosedQuestions : ComponentBase
    {



        private bool loading;
        private List<Question> questions;
        private List<Category> categories;
        private List<AnswerOption> answerOptions;
        private List<UserAnswer> userAnswers;

        private List<Question> questions1;

        private string UserId;
        [Parameter] public int Id { get; set; }
        [CascadingParameter] private Task<AuthenticationState> AuthenticationStateTask { get; set; }

        [Inject] DataManager<Category, ApplicationDbContext> CategoryRepository { get; set; }
        [Inject] DataManager<UserAnswer, ApplicationDbContext> UserAnswerRepository { get; set; }
        [Inject] DataManager<Question, ApplicationDbContext> QuestionRepository { get; set; }
        [Inject] DataManager<AnswerOption, ApplicationDbContext> AnswOptionRepository { get; set; }

        [Inject] private IDialogService DialogService { get; set; }

        [Inject] private IStringLocalizer<LanguageService> L { get; set; }

        [Inject] private NavigationManager NavigationManager { get; set; }
        private IEnumerable<string> Options { get; set; } = new HashSet<string>() { };

        protected override async Task OnInitializedAsync()
        {
            loading = true;
            var authState = await AuthenticationStateTask;
            if (authState.User.Identity.IsAuthenticated)
            {
                UserId = authState.User.Claims.FirstOrDefault().Value;
                categories = await CategoryRepository.Get();
                userAnswers = await UserAnswerRepository.Get(u => u.ApplicationUserId == UserId);
                answerOptions = await AnswOptionRepository.Get();
                questions = await QuestionRepository.Get(q => q.IsActive == true);
            }
            else
            {
                NavigationManager.NavigateTo("Identity/Account/Login", true);
            }

            loading = false;
        }
        private string searchString;

        private Func<QuestionUserLevelVM, bool> FilterFunc => x =>
       {
           if (string.IsNullOrWhiteSpace(searchString))
               return true;

           if (x.CategoryName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
               return true;
           if (x.QuestionName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
               return true;
           return false;
       };


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

        private List<QuestionUserLevelVM> QuestionUserLevelVMs()
        {
            var data = (from u in userAnswers
                        where u.ApplicationUserId == UserId
                        join q in questions on u.QuestionId equals q.Id
                        join c in categories on q.CategoryId equals c.Id
                        join a in answerOptions on u.AnswerOptionId equals a.Id

                        select new QuestionUserLevelVM()
                        {
                            QuestionId = q.Id,
                            QuestionName = q.Name,
                            Description = q.Description,
                            CategoryId = c.Id,
                            CategoryName = c.Name,
                            Level = a.Level,
                            UserId = UserId
                        }

              ).ToList();
            return data;
        }

        private int ShowLevel(Question question)
        {
            //find user answer
            // select level for answer option and user answer
            UserAnswer userAnswer = new()
            {
                QuestionId = question.Id,
                AnswerOptionId = null,
                ApplicationUserId = UserId,
                IsConfirmed = false
            };
            var answer = (from a in question.UserAnswers
                          where a is not null
                          where a.ApplicationUserId == UserId
                          select a);

            if (!answer.Any())
                return 0;

            userAnswer = question.UserAnswers.Find(a => a.ApplicationUserId == UserId);
            var answerOption = question.AnswerOptions.FirstOrDefault(o => o.Id == userAnswer.AnswerOptionId);
            if (answerOption == null)
            {
                return 0;
            }
            else
            {
                return answerOption.Level;
            }
        }

        private async Task UserAnswerDialog(QuestionUserLevelVM vM)
        {
            Question question = questions.FirstOrDefault(q => q.Id == vM.QuestionId);
            Category category = categories.FirstOrDefault(c => c.Name == vM.CategoryName);
            DialogOptions maxWidth = new() { MaxWidth = MaxWidth.Large, FullWidth = true };
            var parameters = new DialogParameters
            {
                ["Q"] = question,
                ["UserId"] = UserId
            };
            var dialog = DialogService.Show<UserQuestionDialog>($"{vM.CategoryName}: {vM.QuestionName}", parameters, maxWidth);
            var data = (await dialog.Result).Data;
            var answer = (UserAnswer)data;
            var a = userAnswers.FirstOrDefault(u => u.ApplicationUserId == UserId);
            var index = userAnswers.IndexOf(a);
            if (index != -1)
                question.UserAnswers[index] = answer;
            else
            {
                userAnswers.Add(answer);
            }
        }
    }
}