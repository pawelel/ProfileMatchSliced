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
    public partial class UserClosedQuestionsTable : ComponentBase
    {



        private bool loading;
        private List<ClosedQuestion> questions;
        private List<Category> categories;
        private List<AnswerOption> answerOptions;
        private List<UserClosedAnswer> userAnswers;

        private string UserId;
        [Parameter] public int Id { get; set; }
        [CascadingParameter] private Task<AuthenticationState> AuthenticationStateTask { get; set; }

        [Inject] DataManager<Category, ApplicationDbContext> CategoryRepository { get; set; }
        [Inject] DataManager<UserClosedAnswer, ApplicationDbContext> UserAnswerRepository { get; set; }
        [Inject] DataManager<ClosedQuestion, ApplicationDbContext> ClosedQuestionRepository { get; set; }
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
            questions = await ClosedQuestionRepository.Get(q => q.IsActive == true);
            userAnswers = await UserAnswerRepository.Get(u => u.ApplicationUserId == UserId);
            answerOptions = await AnswOptionRepository.Get();// need to filter by active question
            answerOptions = (from q in questions join o in answerOptions on q.Id equals o.ClosedQuestionId select o).ToList();
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
            foreach (ClosedQuestion q in questions)
            {
                if (!userAnswers.Any(a => a.ClosedQuestionId == q.Id))
                {
                    var optionId = answerOptions.FirstOrDefault(o => o.ClosedQuestionId == q.Id && o.Level == 1).Id;
                    var answer = new UserClosedAnswer()
                    {
                        AnswerOptionId = optionId,
                        ClosedQuestionId = q.Id,
                        ApplicationUserId = UserId,
                        LastModified = DateTime.Now
                    };
                    userAnswers.Add(answer);
                }
            }
            var data = (from u in userAnswers
                        where u.ApplicationUserId == UserId
                        join q in questions on u.ClosedQuestionId equals q.Id
                        join c in categories on q.CategoryId equals c.Id
                        join a in answerOptions on u.AnswerOptionId equals a.Id
                        select new QuestionUserLevelVM()
                        {
                            ClosedQuestionId = q.Id,
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
            var UserClosedAnswer = userAnswers.FirstOrDefault(q => q.ClosedQuestionId == vM.ClosedQuestionId);
            ClosedQuestion question = questions.FirstOrDefault(q => q.Id == vM.ClosedQuestionId);
            Category category = categories.FirstOrDefault(c => c.Name == vM.CategoryName);
            DialogOptions maxWidth = new() { MaxWidth = MaxWidth.Small, FullWidth = true };
            var parameters = new DialogParameters
            {
                ["Q"] = question,
                ["UserId"] = UserId,
                ["UserClosedAnswer"] = UserClosedAnswer
            };
            var dialog = DialogService.Show<UserQuestionDialog>($"{vM.CategoryName}: {vM.QuestionName}", parameters, maxWidth);
            var data = (await dialog.Result).Data;
            await LoadData();
        }
    }
}