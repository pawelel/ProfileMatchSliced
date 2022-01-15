using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Rendering;
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
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;


namespace ProfileMatch.Components.User
{
    public partial class UserOpenQuestionsTable
    {
        [Inject] public ISnackbar Snackbar { get; set; }
        [CascadingParameter] private Task<AuthenticationState> AuthenticationStateTask { get; set; }
        [Inject] private IDialogService DialogService { get; set; }
        List<OpenQuestion> OpenQuestions = new();
        List<UserOpenAnswer> UserOpenAnswers = new();
        [Inject] DataManager<OpenQuestion, ApplicationDbContext> OpenQuestionRepository { get; set; }
        [Inject] DataManager<UserOpenAnswer, ApplicationDbContext> UserOpenAnswerRepository { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }
        string UserId;
        private async Task<List<OpenQuestion>> GetOpenQuestions()
        {
            return await OpenQuestionRepository.Get();
        }
        private async Task<List<UserOpenAnswer>> GetUserOpenAnswers()
        {
            return await UserOpenAnswerRepository.Get(u => u.ApplicationUserId == UserId);
        }
        
        protected override async Task OnInitializedAsync()
{
            var authState = await AuthenticationStateTask;
            if (authState.User.Identity.IsAuthenticated)
            {
                UserId = authState.User.Claims.FirstOrDefault().Value;
            }
            else
            {
                NavigationManager.NavigateTo("Identity/Account/Login", true);
            }
            UserOpenAnswers = await GetUserOpenAnswers();
            OpenQuestions = await GetOpenQuestions();

            foreach (var openQuestion in OpenQuestions)
            {
                UserAnswerVM userAnswerVM;
                UserOpenAnswer userNote;
                try
                {
                    userNote = UserOpenAnswers.FirstOrDefault(un => un.OpenQuestionId == openQuestion.Id);
                }
                catch (Exception)
                {

                    userNote = new();
                }

                if (userNote != null)
                {
                    userAnswerVM = new UserAnswerVM
                    {
                        UserId = UserId,
                        UserDescription = userNote.UserAnswer,
                        IsDisplayed = userNote.IsDisplayed,
                        AnswerId = openQuestion.Id,
                        OpenQuestionName = openQuestion.Name,
                        OpenQuestionNamePl = openQuestion.NamePl,
                        OpenQuestionDescription = openQuestion.Description,
                        OpenQuestionDescriptionPl = openQuestion.DescriptionPl
                    };
                }
                else
                {
                    userAnswerVM = new UserAnswerVM
                    {
                        UserId = UserId,
                        UserDescription = String.Empty,
                        IsDisplayed = false,
                        AnswerId = openQuestion.Id,
                        OpenQuestionName = openQuestion.Name,
                        OpenQuestionNamePl = openQuestion.NamePl,
                        OpenQuestionDescription = openQuestion.Description,
                        OpenQuestionDescriptionPl = openQuestion.DescriptionPl
                    };
                }
                UserOpenAnswersVM.Add(userAnswerVM);
            }
        }

        private string searchString = "";
        private UserAnswerVM selectedItem1 = null;
        private readonly List<UserAnswerVM> UserOpenAnswersVM = new();

        private Func<UserAnswerVM, bool> QuickFilter => question =>
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (question.OpenQuestionName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (question.OpenQuestionDescription.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (question.UserDescription.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            return false;
        };
        private async Task UserNoteUpdate(UserAnswerVM userAnswerVM)
        {
            var Title = ShareResource.IsEn()?userAnswerVM.OpenQuestionName:userAnswerVM.OpenQuestionNamePl;

            var parameters = new DialogParameters { ["userAnswerVM"] = userAnswerVM };
            var dialog = DialogService.Show<UserOpenQuestionDialog>(Title, parameters);
            await dialog.Result;
        }



        [Inject] private IStringLocalizer<LanguageService> L { get; set; }
    }
}
