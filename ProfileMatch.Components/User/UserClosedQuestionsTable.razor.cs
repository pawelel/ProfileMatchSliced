using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

using MudBlazor;

using ProfileMatch.Components.User.Dialogs;
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

        private Func<QuestionUserLevelVM, bool> QuickFilter => x =>
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (ShareResource.IsEn())
            {
                if (x.CategoryName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    return true;
                if (x.QuestionName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            else
            {
                if (x.CategoryNamePl.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    return true;
                if (x.QuestionNamePl.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        };
        private IEnumerable<string> Cats { get; set; } = new HashSet<string>() { };
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
                            QuestionNamePl = q.NamePl,
                            Description = q.Description,
                            DescriptionPl = q.DescriptionPl,
                            CategoryId = c.Id,
                            CategoryName = c.Name,
                            CategoryNamePl = c.NamePl,
                            Level = a.Level,
                            UserId = UserId
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