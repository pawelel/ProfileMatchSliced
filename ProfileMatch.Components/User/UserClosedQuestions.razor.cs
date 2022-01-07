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

        protected override async Task OnInitializedAsync()
        {
            loading = true;
            var authState = await AuthenticationStateTask;
            if (authState.User.Identity.IsAuthenticated)
            {
                UserId = authState.User.Claims.FirstOrDefault().Value;
                await LoadData();
            }
            else
            {
                NavigationManager.NavigateTo("Identity/Account/Login", true);
            }

            loading = false;
        }

        private async Task LoadData()
        {
            categories = await CategoryRepository.Get();
            userAnswers = await UserAnswerRepository.Get(u => u.ApplicationUserId == UserId);
            questions = await QuestionRepository.Get(q => q.IsActive == true);
            answerOptions = await AnswOptionRepository.Get();// need to filter by active question
            answerOptions = (from q in questions join o in answerOptions on q.Id equals o.QuestionId select o).ToList();
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

        private List<QuestionUserLevelVM> QuestionUserLevelVMs()
        {
            foreach (Question q in questions)
            {
                if (!userAnswers.Any(a => a.QuestionId == q.Id))
                {
                    var answer = new UserAnswer()
                    {
                        AnswerOptionId = answerOptions.Find(o => o.QuestionId == q.Id && o.Level == 1).Id,
                        QuestionId = q.Id,
                        ApplicationUserId = UserId,
                        LastModified = DateTime.Now
                    };
                    userAnswers.Add(answer);
                }
            }
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
        private async Task UserAnswerDialog(QuestionUserLevelVM vM)
        {
           var UserAnswer = userAnswers.Find(q => q.QuestionId== vM.QuestionId);
            Question question = questions.FirstOrDefault(q => q.Id == vM.QuestionId);
            Category category = categories.FirstOrDefault(c => c.Name == vM.CategoryName);
            DialogOptions maxWidth = new() { MaxWidth = MaxWidth.Large, FullWidth = true };
            var parameters = new DialogParameters
            {
                ["Q"] = question,
                ["UserId"] = UserId,
                ["UserAnswer"]= UserAnswer
            };
            var dialog = DialogService.Show<UserQuestionDialog>($"{vM.CategoryName}: {vM.QuestionName}", parameters, maxWidth);
            var data = (await dialog.Result).Data;
           await LoadData();
        }
    }
}