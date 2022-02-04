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
using System.Threading.Tasks;

namespace ProfileMatch.Components.Admin.Dialogs
{
    public partial class AdminAnswerOptionDialog : ComponentBase
    {
        [Inject] private ISnackbar Snackbar { get; set; }
        [Inject] IUnitOfWork UnitOfWork { get; set; }
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
        [Parameter] public int QuestionId { get; set; }
        List<AnswerOption> _answerOptions;

        protected override async Task OnInitializedAsync()
        {
            _answerOptions = await UnitOfWork.AnswerOptions.Get(a => a.ClosedQuestionId == QuestionId);

            if (_answerOptions is null || _answerOptions.Count < 5)
            {
                _answerOptions = new();
                for (int i = 1; i < 6; i++)
                {
                    _answerOptions.Add(new()
                    {
                        ClosedQuestionId = QuestionId,
                        DescriptionPl = string.Empty,
                        Description = string.Empty,
                        Level = i
                    });
                }
            }
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
                foreach (var option in _answerOptions)
                {
                    if (option.Id > 0)
                    {
                        await UnitOfWork.AnswerOptions.Update(option);
                    }
                    else
                    {
                        await UnitOfWork.AnswerOptions.Insert(option);
                    }
                }
                MudDialog.Close(true);
            }
        }
    }
}