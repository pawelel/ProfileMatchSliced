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
        {if(QuestionId==0)
            {
                _closedQVM = new();
            }
            else
            {
                //get question
            _tempQuestion = await UnitOfWork.ClosedQuestions.GetOne(q => q.Id == QuestionId, q=>q.Include(q=>q.Category));
                //map question to vm
                _closedQVM = Mapper.Map<ClosedQuestionVM>(_tempQuestion);
                _deleteEnabled = true;
            }            
        }
        
        async Task SaveAndClose()
        {

        }
        async Task EditLevels()
        {
            if (_closedQVM.AnswerOptionsVM==null|| _closedQVM.AnswerOptionsVM.Count==0)
            {

            }
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