
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



namespace ProfileMatch.Components.Dialogs
{
    public partial class UserOpenQuestionDialog
    {
       [Inject]  private  IStringLocalizer<LanguageService> L { get; set; }
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }

       
        [Inject] private ISnackbar Snackbar { get; set; }
        UserOpenAnswer EditUserAnswer;
        [Parameter] public UserAnswerVM UserAnswerVM { get; set; }
        string TempDescription;
        bool IsDisplayed;
        bool exists;
        private MudTextField<string> multilineReference;

        protected override async Task OnInitializedAsync()
        {
            exists = await UserOpenAnswerRepository.ExistById(UserAnswerVM.UserId, UserAnswerVM.AnswerId);
            if (exists)
            {
                EditUserAnswer = await UserOpenAnswerRepository.GetById(UserAnswerVM.UserId, UserAnswerVM.AnswerId);
                TempDescription = EditUserAnswer.UserAnswer;
            }
            else
            {
                EditUserAnswer = new()
                {
                    IsDisplayed = UserAnswerVM.IsDisplayed,
                    OpenQuestionId = UserAnswerVM.AnswerId,
                    ApplicationUserId = UserAnswerVM.UserId
                };
            }
            IsDisplayed = EditUserAnswer.IsDisplayed;
        }

        [Inject] DataManager<UserOpenAnswer, ApplicationDbContext> UserOpenAnswerRepository { get; set; }

        private MudForm Form;

        private void Cancel()
        {
            MudDialog.Cancel();
            Snackbar.Add(@L["Operation cancelled"], Severity.Warning);
        }

        protected async Task HandleSave()
        {
            await Form.Validate();
            if (Form.IsValid)
            {
                EditUserAnswer.IsDisplayed = IsDisplayed;
                EditUserAnswer.UserAnswer = TempDescription;
                try
                {
                    await Save();
                }
                catch (Exception ex)
                {
                    Snackbar.Add(@L[$"There was an error: {ex.Message}"], Severity.Error);
                }
                UserAnswerVM.IsDisplayed = IsDisplayed;
                UserAnswerVM.UserDescription = TempDescription;
                MudDialog.Close(DialogResult.Ok(UserAnswerVM));
            }
        }

        private async Task Save()
        {
            if (!exists)
            {
                var result = await UserOpenAnswerRepository.Insert(EditUserAnswer);
                Snackbar.Add(@L["Answer added"], Severity.Success);
            }
            else
            {
                var result = await UserOpenAnswerRepository.Update(EditUserAnswer);
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
