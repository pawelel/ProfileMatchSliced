using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

using MudBlazor;

using ProfileMatch.Components.Dialogs;
using ProfileMatch.Contracts;
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
    public partial class AdminClosedQuestions : ComponentBase
    {
        [Inject] ISnackbar Snackbar  { get; set; }
        [Inject] private IDialogService DialogService { get; set; }

        [Inject] DataManager<Category, ApplicationDbContext> CategoryRepository { get; set; }

        [Inject] DataManager<ClosedQuestion, ApplicationDbContext> ClosedQuestionRepository { get; set; }


        private bool loading;
        [Parameter] public int Id { get; set; }
        private List<ClosedQuestion> questions = new();
        private List<Category> categories;
        private IEnumerable<string> Cats { get; set; }

        public bool ShowDetails { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            loading = true;
            categories = await CategoryRepository.Get();
            questions = await ClosedQuestionRepository.Get(include: src => src.Include(q => q.Category).Include(q => q.AnswerOptions));
            Cats = new HashSet<string>() { };
            loading = false;
        }

        private async Task CategoryUpdate(string category)
        { var Cat = await CategoryRepository.GetOne(c=>c.Name == category);
            if (Cat != null)
            {
                var parameters = new DialogParameters { ["Cat"] = Cat };
                var dialog = DialogService.Show<AdminCategoryDialog>(L["Edit Category"], parameters);
                await dialog.Result;
               await LoadData();
            }
            else
            {
                Snackbar.Add(L["Error"]);
            }
        }
        private async Task CategoryCreate()
        {
            var dialog = DialogService.Show<AdminCategoryDialog>("Stwórz kategorię");
            await dialog.Result;
            await LoadData();
        }
        private async Task QuestionCreate(string category)
        {
            var cat =   await CategoryRepository.GetOne(c=>c.Name==category);
            var parameters = new DialogParameters { ["CategoryId"] = cat.Id };
            var dialog = DialogService.Show<AdminClosedQuestionDialog>(L["Add Question"] + $": {category}", parameters);
            await dialog.Result;
            await LoadData();
        }



        private string searchString1 = "";
        private ClosedQuestion selectedItem1 = null;

        private bool FilterFunc1(ClosedQuestion question) => FilterFunc(question, searchString1);

        private static bool FilterFunc(ClosedQuestion question, string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (question.Category.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (question.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            return false;
        }

        private List<ClosedQuestion> GetQuestions()
        {
            if (!Cats.Any())
            {
                return questions;
            }
            else
            {
                return (from q in questions
                              from c in Cats
                              where q.Category.Name == c
                              select q).ToList();
            }
        }

        private async Task QuestionDialog(ClosedQuestion question)
        {
            var parameters = new DialogParameters { ["Q"] = question };
            var dialog = DialogService.Show<AdminClosedQuestionDialog>($"Edytuj pytanie: {question.Name}", parameters);
            await dialog.Result;
        }

        private async Task QuestionDisplay(ClosedQuestion question)
        {
            DialogOptions maxWidth = new() { MaxWidth = MaxWidth.Large, FullWidth = true };
            var parameters = new DialogParameters { ["Q"] = question };
            var dialog = DialogService.Show<AdminQuestionDisplay>($"Wyświetl pytanie", parameters, maxWidth);
            await dialog.Result;
        }

        [Inject]
        private IStringLocalizer<LanguageService> L { get; set; }
    }
}