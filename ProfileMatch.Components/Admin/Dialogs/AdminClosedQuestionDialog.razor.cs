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
    public partial class AdminClosedQuestionDialog : ComponentBase
    {
        bool _first = true;
        [Inject] private ISnackbar Snackbar { get; set; }
        [Inject] IUnitOfWork UnitOfWork { get; set; }
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
        [Parameter] public ClosedQuestionVM Q { get; set; } = new();
        [Parameter] public bool DeleteEnabled { get; set; }
        [Inject] private IDialogService DialogService { get; set; }

        public int ClosedQuestionId { get; set; }
        List<AnswerOption> _answerOptions;
        public string TempName { get; set; }
        public string TempNamePl { get; set; }
        public string TempDescription { get; set; }
        public string TempDescriptionPl { get; set; }
        public bool TempIsActive { get; set; }
        AnswerOption _tempOption;
        ClosedQuestion _tempQuestion = new();

        bool _isOpen = false;
        public void ToggleOpen()
        {
            _isOpen = !_isOpen;
        }
        protected override async Task OnInitializedAsync()
        {
            _answerOptions = await UnitOfWork.AnswerOptions.Get(q => q.ClosedQuestionId == Q.ClosedQuestionId);
            if (_answerOptions == null)
            {

            }
            await QuestionExists(Q);
            TempName = Q.QuestionName;
            TempNamePl = Q.QuestionNamePl;
            TempDescription = Q.Description;
            TempDescriptionPl = Q.DescriptionPl;
            TempIsActive = Q.IsActive;
        }
        void SetOption(AnswerOption option)
        {
            if (option.Id == 0 || option == null)
            {
                return;
            }
            _tempOption = option;
        }
        private async Task<bool> QuestionExists(ClosedQuestionVM q)
        {
            // question parameteer null check
            if (q.ClosedQuestionId != 0 && q != null)
            {
                return (_tempQuestion = await UnitOfWork.ClosedQuestions.GetById(q.ClosedQuestionId)) != null;
            }

            return false;
        }


        private MudForm _form;

        private void Cancel()
        {
            MudDialog.Cancel();
            Snackbar.Add(L["Operation cancelled"], Severity.Warning);
        }

        // check question id
        async Task CanLevel(ClosedQuestionVM question)
        {


            string title;
            if (ShareResource.IsEn())
            {
                title = "You need to fill question first";
            }
            else
            {
                title = "Najpierw uzupełnij pytanie";
            }

            if (!await QuestionExists(question))
            {
                Snackbar.Add(title, Severity.Error);
                return;
            }
            var options = await UnitOfWork.AnswerOptions.Get(q => q.ClosedQuestionId == question.ClosedQuestionId);
            if (options.Any())
            {
                ChangePage();
            }
            else
            {
                await AddLevels(question);
                _tempOption = await UnitOfWork.AnswerOptions.GetOne(o => o.ClosedQuestionId == question.ClosedQuestionId && o.Level == 1);
                ChangePage();
            }
        }
        void ChangePage()
        {
            _first = !_first;
        }

        protected async Task SaveAndClose()
        {
            ClosedQuestion question = new();
            await _form.Validate();
            if (_form.IsValid)
            {
                question.Name = TempName;
                question.NamePl = TempNamePl;
                question.Description = TempDescription;
                question.DescriptionPl = TempDescriptionPl;
                question.CategoryId = Q.CategoryId;
                question.IsActive = TempIsActive;
                try
                {
                    if (Q.ClosedQuestionId == 0)
                    {
                        var result = await UnitOfWork.ClosedQuestions.Insert(question);
                        switch (ShareResource.IsEn())
                        {
                            case true:
                                Snackbar.Add($"Question {result.Name} has been created", Severity.Success);
                                break;
                            default:
                                Snackbar.Add($"Pytanie {result.NamePl} zostało utworzone", Severity.Success);
                                break;
                        }
                    }
                    else
                    {
                        await UnitOfWork.ClosedQuestions.Update(question);
                        switch (ShareResource.IsEn())
                        {
                            case true:
                                Snackbar.Add($"Question {question.Name} has been updated", Severity.Success);
                                break;
                            default:
                                Snackbar.Add($"Pytanie  {question.NamePl} zostało zaktualizowane", Severity.Success);
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Snackbar.Add(@L[$"There was an error:"] + $" {ex.Message}", Severity.Error);
                }
            }
            MudDialog.Close(DialogResult.Ok(Q));
        }
        private async Task AddLevels(ClosedQuestionVM questionVM)
        {
            if (await UnitOfWork.AnswerOptions.Get(a => a.ClosedQuestionId == questionVM.ClosedQuestionId) == null && questionVM.ClosedQuestionId > 0)
            {
                for (int i = 1; i < 6; i++)
                {
                    await UnitOfWork.AnswerOptions.Insert(new AnswerOption()
                    {
                        ClosedQuestionId = questionVM.ClosedQuestionId,
                        DescriptionPl = string.Empty,
                        Description = string.Empty,
                        Level = i
                    }
               );
                }
            }
        }
        private async Task EditLevelDialog(AnswerOption answerOption)
        {
            DialogOptions maxWidth = new() { MaxWidth = MaxWidth.Large, FullWidth = true };
            var parameters = new DialogParameters { ["O"] = answerOption };
            var dialog = DialogService.Show<AdminLevelDialog>(L["Edit Answer Level"] + $" {answerOption.Level}", parameters, maxWidth);
            await dialog.Result;
        }
        private async Task<bool> ChangeActiveState(ClosedQuestionVM question)
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
            List<AnswerOption> answerOptions = await UnitOfWork.AnswerOptions.Get(q => q.ClosedQuestionId == question.ClosedQuestionId);
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
        async void Delete(ClosedQuestionVM cqVM)
        {
            string success;
            string error;
            if (cqVM != null)
            {
                var cat = await UnitOfWork.ClosedQuestions.GetById(cqVM.ClosedQuestionId);
                var result = await UnitOfWork.ClosedQuestions.Delete(cat);
                if (result)
                {
                    try
                    {
                        if (ShareResource.IsEn())
                        {
                            success = $"Question {cqVM.QuestionName} deleted";
                            Snackbar.Add(success, Severity.Info);
                        }
                        else
                        {
                            success = $"Pytanie {cqVM.QuestionName} usunięte";
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