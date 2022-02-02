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
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
        [Parameter] public int QuestionId { get; set; }
        [Parameter] public string CategoryName { get; set; }
        [Inject] private IDialogService DialogService { get; set; }
        [Inject] IMapper Mapper { get; set; }
        //new view model for question
        ClosedQuestionVM _closedQVM;
        ClosedQuestion _tempQuestion;
        bool _deleteEnabled;
        bool _isOpen = false;
        private MudForm _form;

        protected override async Task OnInitializedAsync()
        {
            if (QuestionId == 0)
            {
                _closedQVM = new();
            }
            else
            {
                //get question
                _tempQuestion = await UnitOfWork.ClosedQuestions.GetOne(q => q.Id == QuestionId, q => q.Include(q => q.Category));
                //map question to vm
                _closedQVM = Mapper.Map<ClosedQuestionVM>(_tempQuestion);
                _deleteEnabled = true;
            }
        }
        async Task SaveAndClose()
        {
            await Save();
            MudDialog.Close(true);
        }
        async Task Create()
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
            return;
        }

        async Task Save()
        {
            await _form.Validate();
            if (_form.IsValid)
            {
                switch (_tempQuestion.Id == 0)
                {
                    case true:
                        await Create();
                        break;
                    case false:
                        await Update();
                        break;
                }
            }
            else
            {

            }
        }


        async Task Update()
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


        async Task EditLevels()
        {
            await Save();

        }



        // open delete dialog
        public void ToggleOpen()
        {
            _isOpen = !_isOpen;
        }


        private void Cancel()
        {
            MudDialog.Cancel();
            Snackbar.Add(L["Operation cancelled"], Severity.Warning);
        }


    }
}