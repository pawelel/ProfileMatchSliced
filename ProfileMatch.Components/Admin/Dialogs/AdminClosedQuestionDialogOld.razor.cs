using AutoMapper;

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

using MudBlazor;

using ProfileMatch.Data;
using ProfileMatch.Models.Entities;
using ProfileMatch.Models.ViewModels;
using ProfileMatch.Repositories;
using ProfileMatch.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileMatch.Components.Admin.Dialogs
{
    public partial class AdminClosedQuestionDialogOld : ComponentBase
    {
        bool _first = true;
        [Inject] private ISnackbar Snackbar { get; set; }
        [Inject] IUnitOfWork UnitOfWork { get; set; }
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
        [Parameter] public int QuestionId { get; set; }
        [Parameter] public string CategoryName { get; set; }
        [Inject] private IDialogService DialogService { get; set; }

        List<AnswerOption> _answerOptions;
        int _categoryId;
       
        string _tempName;
        string _tempNamePl;
        string _tempDescription;
        string _tempDescriptionPl;
        bool _tempIsActive;
        AnswerOption _tempOption;
        ClosedQuestion _tempQuestion;
        bool _deleteEnabled;
        bool _isOpen = false;
     
        public void ToggleOpen()
        {
            _isOpen = !_isOpen;
        }
        protected override async Task OnInitializedAsync()
        {
            if (QuestionId == 0)
            {
                if (ShareResource.IsEn())
                {
                _categoryId = (await UnitOfWork.Categories.GetOne(c => c.Name == CategoryName)).Id;
                }
                else
                {
                    _categoryId = (await UnitOfWork.Categories.GetOne(c => c.NamePl == CategoryName)).Id;
                }


                _tempQuestion = new();
                _deleteEnabled = false;
            }
            else
            {
                _tempQuestion = await UnitOfWork.ClosedQuestions.GetOne(q => q.Id == QuestionId);
                _answerOptions = await UnitOfWork.AnswerOptions.Get(q => q.ClosedQuestionId == QuestionId);
                _tempName = _tempQuestion.Name;
                _tempNamePl = _tempQuestion.NamePl;
                _tempDescription = _tempQuestion.Description;
                _tempDescriptionPl = _tempQuestion.DescriptionPl;
                _tempIsActive = _tempQuestion.IsActive;
                _tempQuestion.AnswerOptions = _answerOptions;
                _deleteEnabled = true;
            }
        }
        void SetOption(AnswerOption option)
        {
            if (option.Id == 0 || option == null)
            {
                return;
            }
            _tempOption = option;
        }


        private MudForm _form;

        private void Cancel()
        {
            MudDialog.Cancel();
            Snackbar.Add(L["Operation cancelled"], Severity.Warning);
        }

        // check question id
        async Task CanLevel(ClosedQuestion question)
        {
            string title;
            title = ShareResource.IsEn() ?
             "You need to fill question first" :
            "Najpierw uzupełnij pytanie";

            if (question != null && question.Id == 0)
            {
                Snackbar.Add(title, Severity.Error);
                return;
            }

            if (_answerOptions != null && _answerOptions.Any())
            {
                ChangeDialogPage();
            }
            else
            {
                await AddLevels(question.Id);
                _tempOption = await UnitOfWork.AnswerOptions.GetOne(o => o.ClosedQuestionId == question.Id && o.Level == 1);
                ChangeDialogPage();
            }
        }
        void ChangeDialogPage()
        {
            _first = !_first;
        }

        protected async Task SaveAndClose()
        {

            await _form.Validate();
            if (_form.IsValid)
            {
                _tempQuestion.Name = _tempName;
                _tempQuestion.NamePl = _tempNamePl;
                _tempQuestion.Description = _tempDescription;
                _tempQuestion.DescriptionPl = _tempDescriptionPl;
                _tempQuestion.IsActive = _tempIsActive;
                if (_tempQuestion.Id != 0)
                {
                    await UnitOfWork.ClosedQuestions.Update(_tempQuestion);
                    switch (ShareResource.IsEn())
                    {
                        case true:
                            Snackbar.Add($"Question {_tempQuestion.Name} has been updated", Severity.Success);
                            break;
                        default:
                            Snackbar.Add($"Pytanie  {_tempQuestion.NamePl} zostało zaktualizowane", Severity.Success);
                            break;
                    }
                    return;
                }

                _tempQuestion.CategoryId = _categoryId;
                var result = await UnitOfWork.ClosedQuestions.Insert(_tempQuestion);
                switch (ShareResource.IsEn())
                {
                    case true:
                        Snackbar.Add($"Question {result.Name} has been created", Severity.Success);
                        break;
                    default:
                        Snackbar.Add($"Pytanie {result.NamePl} zostało utworzone", Severity.Success);
                        break;
                }
                return;
            }
            MudDialog.Close(DialogResult.Ok(_tempQuestion));
        }
        private async Task AddLevels(int closedQuestionId)
        {
            if (_answerOptions == null && closedQuestionId > 0)
            {
                List<AnswerOption> answerOptions = new();
                for (int i = 1; i <= 5; i++)
                {
                    answerOptions.Add(new AnswerOption
                    {
                        ClosedQuestionId = closedQuestionId,
                        DescriptionPl = string.Empty,
                        Description = string.Empty,
                        Level = i
                    });
                }

                _answerOptions = await UnitOfWork.AnswerOptions.InsertRange(answerOptions);
            }
            else
            {
                Snackbar.Add(L["You need to save question first"]);
            }
        }
        private async Task EditLevelDialog(AnswerOption answerOption)
        {
            DialogOptions maxWidth = new() { MaxWidth = MaxWidth.Large, FullWidth = true };
            var parameters = new DialogParameters { ["O"] = answerOption };
            var dialog = DialogService.Show<AdminAnswerOptionDialog>(L["Edit Answer Level"] + $" {answerOption.Level}", parameters, maxWidth);
            await dialog.Result;
        }
        private async Task<bool> ChangeActiveState(ClosedQuestion question)
        {
            string titleOn;
            string titleOff;
            string warning;
            if (ShareResource.IsEn())
            {
                titleOn = "Question is Activated";
                titleOff = "Question is Disabled";
                warning = "Unable to activate question - not all levels are filled";
            }
            else
            {
                titleOn = "Pytanie jest aktywne";
                titleOff = "Pytanie zostało wyłączone";
                warning = "Nie można aktywować pytania - nie wszystkie poziomy są wypełnione";
            }
            //does list of answerOptions exist and any answerOption is nullOwWhiteSpace
            List<AnswerOption> answerOptions = await UnitOfWork.AnswerOptions.Get(q => q.ClosedQuestionId == question.Id);
            if (answerOptions is not null && !answerOptions.Any(ao => string.IsNullOrWhiteSpace(ao.Description)))
            {

                question.IsActive = !question.IsActive;
                await UnitOfWork.ClosedQuestions.Update(_tempQuestion);
                if (question.IsActive)
                {
                    Snackbar.Add(titleOn, Severity.Success);
                }
                else
                {
                    Snackbar.Add(titleOff, Severity.Warning);
                }
                StateHasChanged();
            }
            Snackbar.Add(warning, Severity.Warning);
            return false;
        }
        async void Delete(ClosedQuestion cq)
        {
            string success;
            string error;
            if (_deleteEnabled && cq != null)
            {
                var question = await UnitOfWork.ClosedQuestions.GetById(cq.Id);
                var result = await UnitOfWork.ClosedQuestions.Delete(question);
                if (result)
                {
                    try
                    {
                        if (ShareResource.IsEn())
                        {
                            success = $"Question {cq.Name} deleted";
                            Snackbar.Add(success, Severity.Info);
                        }
                        else
                        {
                            success = $"Pytanie {cq.NamePl} usunięte";
                        }
                    }
                    catch (Exception ex)
                    {
                        if (ShareResource.IsEn())
                        {
                            error = $"There was an error during deletion: {ex.Message}";
                            Snackbar.Add(error, Severity.Error);
                        }
                        else
                        {
                            error = $"Wystąpił problem podczas usuwania: {ex.Message}";
                            Snackbar.Add(error, Severity.Error);
                        }
                    }
                }
            }
        }
    }
}