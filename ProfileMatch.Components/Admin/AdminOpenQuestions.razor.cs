using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using MudBlazor;

using ProfileMatch.Components.Admin.Dialogs;
using ProfileMatch.Data;
using ProfileMatch.Models.Models;

using ProfileMatch.Repositories;

using ProfileMatch.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ProfileMatch.Components.Admin
{
    public partial class AdminOpenQuestions : ComponentBase
    {
        [Inject]
        public ISnackbar Snackbar { get; set; }

        [Inject]
        private IDialogService DialogService { get; set; }

        [Inject] DataManager<OpenQuestion, ApplicationDbContext> OpenQuestionRepository { get; set; }

        private async Task<List<OpenQuestion>> GetOpenQuestions()
        {
            return await OpenQuestionRepository.Get();
        }

        protected override async Task OnInitializedAsync()
        {
            _openQuestions = await GetOpenQuestions();
        }

        
        
        
        
        private string _searchString1 = "";
        private OpenQuestion _selectedItem1 = null;
        private List<OpenQuestion> _openQuestions = new();

        private bool FilterFunc1(OpenQuestion OpenQuestion) => FilterFunc(OpenQuestion, _searchString1);

        private static bool FilterFunc(OpenQuestion OpenQuestion, string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (OpenQuestion.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (OpenQuestion.Description != null && OpenQuestion.Description.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            return false;
        }

        private async Task OpenQuestionUpdate(OpenQuestion OpenQuestion = null)
        {
            var parameters = new DialogParameters { ["EditedOpenQuestion"] = OpenQuestion };
            if (OpenQuestion.Id > 0) { 
            var dialog = DialogService.Show<AdminOpenQuestionDialog>(L["Edit Question"], parameters);
                await dialog.Result;
            }
            else
            {
                var dialog = DialogService.Show<AdminOpenQuestionDialog>(L["Create Question"]);
                await dialog.Result;
            }
            _openQuestions = await GetOpenQuestions();
        }

       

        

    }
}
