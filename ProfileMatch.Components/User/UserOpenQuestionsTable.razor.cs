using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Extensions.Localization;

using MudBlazor;

using ProfileMatch.Components.Dialogs;
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
    public partial class UserOpenQuestionsTable
    {
        [Inject] public ISnackbar Snackbar { get; set; }
        [CascadingParameter] private Task<AuthenticationState> AuthenticationStateTask { get; set; }
        [Inject] private IDialogService DialogService { get; set; }
        List<OpenQuestion> OpenQuestions = new();
        List<UserOpenAnswer> UserOpenAnswers = new();
        [Inject] DataManager<OpenQuestion, ApplicationDbContext> OpenQuestionRepository { get; set; }
        [Inject] DataManager<UserOpenAnswer, ApplicationDbContext> UserNoteRepository { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }
        string UserId;
        private async Task<List<OpenQuestion>> GetNotesAsync()
        {
            return await OpenQuestionRepository.Get();
        }
        private async Task<List<UserOpenAnswer>> GetUserNotesAsync()
        {
            return await UserNoteRepository.Get(u => u.ApplicationUserId == UserId);
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
            UserOpenAnswers = await GetUserNotesAsync();
            OpenQuestions = await GetNotesAsync();

            foreach (var openQuestion in OpenQuestions)
            {
                UserAnswerVM userNoteVM;
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
                    userNoteVM = new UserAnswerVM
                    {
                        UserId = UserId,
                        UserDescription = userNote.UserAnswer,
                        IsDisplayed = userNote.IsDisplayed,
                        AnswerId = openQuestion.Id,
                        OpenQuestionName = openQuestion.Name,
                        OpenQuestionDescription = openQuestion.Description
                    };
                }
                else
                {
                    userNoteVM = new UserAnswerVM
                    {
                        UserId = UserId,
                        UserDescription = String.Empty,
                        IsDisplayed = false,
                        AnswerId = openQuestion.Id,
                        OpenQuestionName = openQuestion.Name,
                        OpenQuestionDescription = openQuestion.Description
                    };
                }
                UserNotesVM.Add(userNoteVM);
            }
        }

        
        
        
        
        private string searchString1 = "";
        private UserAnswerVM selectedItem1 = null;
        private readonly List<UserAnswerVM> UserNotesVM = new();

        private bool FilterFunc1(UserAnswerVM UserNoteVM) => FilterFunc(UserNoteVM, searchString1);

        private static bool FilterFunc(UserAnswerVM UserNoteVM, string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (UserNoteVM.OpenQuestionName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (UserNoteVM.OpenQuestionDescription != null && UserNoteVM.OpenQuestionDescription.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (UserNoteVM.UserDescription != null && UserNoteVM.UserDescription.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            return false;
        }

        private async Task UserNoteUpdate(UserAnswerVM UserNoteVM)
        {
            var parameters = new DialogParameters { ["UserNoteVM"] = UserNoteVM };
            var dialog = DialogService.Show<UserOpenQuestionDialog>("Edytuj pytanie", parameters);
            await dialog.Result;
        }



        [Inject] private IStringLocalizer<LanguageService> L { get; set; }
    }
}
