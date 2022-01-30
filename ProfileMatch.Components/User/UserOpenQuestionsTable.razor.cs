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
        [Inject] IUnitOfWork UnitOfWork { get; set; }
        [Inject] private IDialogService DialogService { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }
        [CascadingParameter] private Task<AuthenticationState> AuthenticationStateTask { get; set; }
        List<OpenQuestion> _openQuestions = new();
        List<UserOpenAnswer> _userOpenAnswers = new();
        string _userId;
        private async Task<List<OpenQuestion>> GetOpenQuestions()
        {
            return await UnitOfWork.OpenQuestions.Get();
        }
        private async Task<List<UserOpenAnswer>> GetUserOpenAnswers()
        {
            return await UnitOfWork.UserOpenAnswers.Get(u => u.ApplicationUserId == _userId);
        }
        
        protected override async Task OnInitializedAsync()
{
            var authState = await AuthenticationStateTask;
            if (authState.User.Identity.IsAuthenticated)
            {
                _userId = authState.User.Claims.FirstOrDefault().Value;
            }
            else
            {
                NavigationManager.NavigateTo("Identity/Account/Login", true);
            }
            _userOpenAnswers = await GetUserOpenAnswers();
            _openQuestions = await GetOpenQuestions();

            foreach (var openQuestion in _openQuestions)
            {
                UserAnswerVM userAnswerVM;
                UserOpenAnswer userNote;
                try
                {
                    userNote = _userOpenAnswers.FirstOrDefault(un => un.OpenQuestionId == openQuestion.Id);
                }
                catch (Exception)
                {

                    userNote = new();
                }

                if (userNote != null)
                {
                    userAnswerVM = new UserAnswerVM
                    {
                        UserId = _userId,
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
                        UserId = _userId,
                        UserDescription = String.Empty,
                        IsDisplayed = false,
                        AnswerId = openQuestion.Id,
                        OpenQuestionName = openQuestion.Name,
                        OpenQuestionNamePl = openQuestion.NamePl,
                        OpenQuestionDescription = openQuestion.Description,
                        OpenQuestionDescriptionPl = openQuestion.DescriptionPl
                    };
                }
                _userOpenAnswersVM.Add(userAnswerVM);
            }
        }

        private string _searchString = "";
        private UserAnswerVM _selectedItem1 = null;
        private readonly List<UserAnswerVM> _userOpenAnswersVM = new();

        private Func<UserAnswerVM, bool> QuickFilter => question =>
        {
            if (string.IsNullOrWhiteSpace(_searchString))
                return true;
            if (question.OpenQuestionName.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (question.OpenQuestionDescription.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (question.UserDescription.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
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



    }
}
