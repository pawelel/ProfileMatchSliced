
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Extensions.Localization;

using MudBlazor;

using ProfileMatch.Data;
using ProfileMatch.Models.Models;
using ProfileMatch.Models.ViewModels;
using ProfileMatch.Repositories;

using ProfileMatch.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace ProfileMatch.Components.User.Dialogs
{
    public partial class UserOpenQuestionDialog
    {
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }

       
        [Inject] private ISnackbar Snackbar { get; set; }
        [Inject] IUnitOfWork UnitOfWork { get; set; }
        UserOpenAnswer _editUserAnswer;
        [Parameter] public UserAnswerVM UserAnswerVM { get; set; }
        string _tempDescription;
        bool _isDisplayed;
        bool _exists;
#pragma warning disable IDE0052 // Remove unread private members - reference to count
        private MudTextField<string> _multilineReference;
#pragma warning restore IDE0052 // Remove unread private members

        protected override async Task OnInitializedAsync()
        { 
            _exists = await UnitOfWork.UserOpenAnswers.ExistById(UserAnswerVM.UserId, UserAnswerVM.AnswerId);
            if (_exists)
            {
                _editUserAnswer = await UnitOfWork.UserOpenAnswers.GetById(UserAnswerVM.UserId, UserAnswerVM.AnswerId);
                _tempDescription = _editUserAnswer.UserAnswer;
            }
            else
            {
                _editUserAnswer = new()
                {
                    IsDisplayed = UserAnswerVM.IsDisplayed,
                    OpenQuestionId = UserAnswerVM.AnswerId,
                    ApplicationUserId = UserAnswerVM.UserId
                };
            }
            _isDisplayed = _editUserAnswer.IsDisplayed;
        }

        private MudForm _form;

        private void Cancel()
        {
            MudDialog.Cancel();
            Snackbar.Add(@L["Operation cancelled"], Severity.Warning);
        }

        protected async Task HandleSave()
        {
            await _form.Validate();
            if (_form.IsValid)
            {
                _editUserAnswer.IsDisplayed = _isDisplayed;
                _editUserAnswer.UserAnswer = _tempDescription;
                try
                {
                    await Save();
                }
                catch (Exception ex)
                {
                    Snackbar.Add(@L[$"There was an error: {ex.Message}"], Severity.Error);
                }
                UserAnswerVM.IsDisplayed = _isDisplayed;
                UserAnswerVM.UserDescription = _tempDescription;
                MudDialog.Close(DialogResult.Ok(UserAnswerVM));
            }
        }

        private async Task Save()
        {
            if (!_exists)
            {
                var result = await UnitOfWork.UserOpenAnswers.Insert(_editUserAnswer);
                Snackbar.Add(@L["Answer added"], Severity.Success);
            }
            else
            {
                var result = await UnitOfWork.UserOpenAnswers.Update(_editUserAnswer);
                Snackbar.Add(@L["Answer updated"], Severity.Success);
            }
        }

        private IEnumerable<string> MaxCharacters(string ch)
        {

            if (!string.IsNullOrEmpty(ch) && 199 < ch?.Length)
                yield return @L["Max 200 characters"];
            
        }
    }
}
