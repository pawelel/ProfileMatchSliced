using AutoMapper;

using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
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
    public partial class AdminClosedQuestionDialog : ComponentBase
    {

        [Inject] private ISnackbar Snackbar { get; set; }
        [Inject] IUnitOfWork UnitOfWork { get; set; }
        [Inject] private IDialogService DialogService { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }

        [Parameter] public int QuestionId { get; set; }
        [Parameter] public int CategoryId { get; set; }
        //new view model for question
        ClosedQuestion _tempQuestion;
        List<bool> _answerOptionLevels = new();
        List<AnswerOption> _answerOptions;
        bool _deleteEnabled;
        bool _isOpen = false;
        bool _canActivate = false;
        private MudForm _form;
        bool _levelsEnabled = false;
        protected override async Task OnInitializedAsync()
        {
            _tempQuestion = await UnitOfWork.ClosedQuestions.GetOne(q => q.Id == QuestionId, q => q.Include(q => q.Category));
            if (_tempQuestion == null)
            {
                _tempQuestion = new()
                {
                    CategoryId = CategoryId,
                    IsActive = false,
                    AnswerOptions = new()
                };
            }
            else
            {
                _deleteEnabled = true;
                _answerOptions = await UnitOfWork.AnswerOptions.Get(a => a.ClosedQuestionId == QuestionId);
                if (_answerOptions is null)
                {
                    _answerOptionLevels = Enumerable.Repeat(false, 5).ToList();
                }
                else
                {
                    GetLevels();
                    _levelsEnabled = true;
                }
            }
            CanActivate();
        }
        async Task SaveAndClose()
        {
            await Save();
            MudDialog.Close(true);
            NavigationManager.NavigateTo("admin/dashboard/1", true);
        }

        void ToggleEnable()
        {
            _tempQuestion.IsActive = !_tempQuestion.IsActive;
        }
        async Task Save()
        {
            await _form.Validate();
            if (_form.IsValid)
            {
                if (_tempQuestion.Id == 0)
                {

                    _tempQuestion = await UnitOfWork.ClosedQuestions.Insert(_tempQuestion);
                    switch (ShareResource.IsEn())
                    {
                        case true:
                            Snackbar.Add($"Question {_tempQuestion.Name} has been created", Severity.Success);
                            break;
                        default:
                            Snackbar.Add($"Pytanie {_tempQuestion.NamePl} zostało utworzone", Severity.Success);
                            break;
                    }
                }
                else
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
                }
            }
            else
            {
                Snackbar.Add(L["Fill all fields"], Severity.Error);
            }
        }

        async Task EditLevels()
        {if (_tempQuestion.Id >0)
            {
                DialogOptions maxWidth = new() { MaxWidth = MaxWidth.Medium };
                var parameters = new DialogParameters { ["QuestionId"] = _tempQuestion.Id };
                var dialog = DialogService.Show<AdminAnswerOptionDialog>(L["Edit Answer Options"],
                    parameters, maxWidth);
                _answerOptions = await UnitOfWork.AnswerOptions.Get(a => a.ClosedQuestionId == _tempQuestion.Id);
                GetLevels();
                _levelsEnabled = false;
                await dialog.Result;
            }
        }

        private void GetLevels()
        {
            _answerOptionLevels = new();
            for (int i = 1; i < 6; i++)
            {
                if (_answerOptions.Any(a => a.Level == i && !string.IsNullOrWhiteSpace(a.Description)))
                {
                    _answerOptionLevels.Add(true);
                }
                else
                {
                    _answerOptionLevels.Add(false);
                }
            }
        }

        // open delete dialog
        public void ToggleOpenDelete()
        {
            _isOpen = !_isOpen;
        }

        void CanActivate()
        {
            if (QuestionId == 0)
                return;
            if (_answerOptions.Any(a => string.IsNullOrWhiteSpace(a.Description)) || _answerOptions == null || _answerOptions.Count < 5 || string.IsNullOrWhiteSpace(_tempQuestion.NamePl) || string.IsNullOrWhiteSpace(_tempQuestion.Name)||_tempQuestion == null)
            {
                _canActivate = false;
            }
            else
            {
                _canActivate = true;
            }
        }

        private void Cancel()
        {
            MudDialog.Cancel();
            Snackbar.Add(L["Operation cancelled"], Severity.Warning);
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
                        }
                        else
                        {
                            success = $"Pytanie {cq.NamePl} usunięte";
                        }
                        Snackbar.Add(success, Severity.Info);
                        _isOpen = false;
                        MudDialog.Close(true);
                        NavigationManager.NavigateTo("admin/dashboard/1", true);
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