using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using MudBlazor;
using ProfileMatch.Components.Dialogs;
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

        private async Task<List<OpenQuestion>> GetNotesAsync()
        {
            return await OpenQuestionRepository.Get();
        }

        protected override async Task OnInitializedAsync()
        {
            OpenQuestions = await GetNotesAsync();
        }

        
        
        
        
        private string searchString1 = "";
        private OpenQuestion selectedItem1 = null;
        private List<OpenQuestion> OpenQuestions = new();

        private bool FilterFunc1(OpenQuestion OpenQuestion) => FilterFunc(OpenQuestion, searchString1);

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

        private async Task NoteUpdate(OpenQuestion OpenQuestion)
        {
            var parameters = new DialogParameters { ["EditedOpenQuestion"] = OpenQuestion };
            var dialog = DialogService.Show<AdminOpenQuestionDialog>("Edytuj pytanie", parameters);
            await dialog.Result;
        }

        private async Task NoteCreate()
        {
            var dialog = DialogService.Show<AdminOpenQuestionDialog>("Stwórz pytanie");
            await dialog.Result;
            OpenQuestions = await GetNotesAsync();
        }

        

        [Inject]
        private IStringLocalizer<LanguageService> L { get; set; }
    }
}
