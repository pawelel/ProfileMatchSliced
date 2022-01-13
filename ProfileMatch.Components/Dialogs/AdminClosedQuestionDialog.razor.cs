using Microsoft.AspNetCore.Components;
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
    public partial class AdminClosedQuestionDialog : ComponentBase
    {
        bool first = true;
        [Inject] private ISnackbar Snackbar { get; set; }
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
        [Parameter] public ClosedQuestionVM Q { get; set; } = new();
        [Parameter] public bool DeleteEnabled { get; set; }
        [Inject] private IDialogService DialogService { get; set; }
        [Inject] DataManager<AnswerOption, ApplicationDbContext> AnswerOptionRepository { get; set; }
        public int ClosedQuestionId { get; set; }
        List<AnswerOption> answerOptions;
        public string TempName { get; set; }
        public string TempNamePl { get; set; }
        public string TempDescription { get; set; }
        public string TempDescriptionPl { get; set; }
        public bool TempIsActive { get; set; }
        AnswerOption tempOption;
        ClosedQuestion tempQuestion = new();

        bool _isOpen = false;
        public void ToggleOpen()
        {
            _isOpen = !_isOpen;
        }
        protected override async Task OnInitializedAsync()
        {
            answerOptions = await AnswerOptionRepository.Get(q => q.ClosedQuestionId == Q.ClosedQuestionId);
            if (answerOptions == null)
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
            tempOption = option;
        }
        private async Task<bool> QuestionExists(ClosedQuestionVM q)
        {
            // question parameteer null check
            if (q.ClosedQuestionId != 0 && q != null)
            {
                return (tempQuestion = await ClosedQuestionRepository.GetById(q.ClosedQuestionId)) != null;
            }

            return false;
        }

        [Inject] DataManager<ClosedQuestion, ApplicationDbContext> ClosedQuestionRepository { get; set; }

        private MudForm Form;

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
            var options = await AnswerOptionRepository.Get(q => q.ClosedQuestionId == question.ClosedQuestionId);
            if (options.Any())
            {
                ChangePage();
            }
            else
            {
                await AddLevels(question);
                tempOption = await AnswerOptionRepository.GetOne(o => o.ClosedQuestionId == question.ClosedQuestionId && o.Level == 1);
                ChangePage();
            }
        }
        void ChangePage()
        {
            first = !first;
        }
        protected async Task SaveAndClose()
        {
            await Save();
            MudDialog.Close(DialogResult.Ok(Q));
        }
        private async Task Save()
        {
            ClosedQuestion question = new();
            await Form.Validate();
            if (Form.IsValid)
            {
                question.Name = Q.QuestionName = TempName;
                question.NamePl = Q.QuestionNamePl = TempNamePl;
                question.Description = Q.Description = TempDescription;
                question.DescriptionPl = Q.DescriptionPl = TempDescriptionPl;
                question.CategoryId = Q.CategoryId = Q.CategoryId;
                question.IsActive = Q.IsActive = TempIsActive;
                try
                {

                    //has any other question the same name in the category?
                    var exists = (await ClosedQuestionRepository.Get(q => q.Name == Q.QuestionName));
                    if (Q.ClosedQuestionId == 0 && exists.Count == 0)
                    {
                        var result = await ClosedQuestionRepository.Insert(question);
                        if (ShareResource.IsEn())
                        {
                            Snackbar.Add($"Question {result.Name} has been created", Severity.Success);
                        }
                        else
                        {
                            Snackbar.Add($"Pytanie {result.NamePl} zostało utworzone", Severity.Success);
                        }
                    }
                    else if (exists.Count <= 1)
                    {
                        await ClosedQuestionRepository.Update(question);
                        if (ShareResource.IsEn())
                        {
                            Snackbar.Add($"Question {question.Name} has been updated", Severity.Success);
                        }
                        else
                        {
                            Snackbar.Add($"Pytanie  {question.NamePl} zostało zaktualizowane", Severity.Success);
                        }
                    }
                    else
                    {
                        if (ShareResource.IsEn())
                        {
                            Snackbar.Add($"Question{question.Name} already exists", Severity.Error);
                        }
                        else
                        {
                            Snackbar.Add($"Pytanie  {question.NamePl} już istnieje", Severity.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Snackbar.Add(@L[$"There was an error:"] + $" {ex.Message}", Severity.Error);
                }
            }
        }
        [Inject] private IStringLocalizer<LanguageService> L { get; set; }
        private async Task AddLevels(ClosedQuestionVM questionVM)
        {
            var exist = await AnswerOptionRepository.Get(a => a.ClosedQuestionId == questionVM.ClosedQuestionId);
            if (questionVM != null && questionVM.ClosedQuestionId != 0 || exist == null || exist.Count == 0)
                for (int i = 1; i < 6; i++)
                {
                    await AnswerOptionRepository.Insert(new AnswerOption()
                    {
                        ClosedQuestionId = questionVM.ClosedQuestionId,
                        DescriptionPl = string.Empty,
                        Description = string.Empty,
                        Level = i
                    }
               );
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
            List<AnswerOption> answerOptions = await AnswerOptionRepository.Get(q => q.ClosedQuestionId == question.ClosedQuestionId);
            if (answerOptions is not null && !answerOptions.Any(ao => string.IsNullOrWhiteSpace(ao.Description)))
            {

                question.IsActive = !question.IsActive;
                await ClosedQuestionRepository.Update(tempQuestion);
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
                var cat = await ClosedQuestionRepository.GetById(cqVM.ClosedQuestionId);
                var result = await ClosedQuestionRepository.Delete(cat);
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