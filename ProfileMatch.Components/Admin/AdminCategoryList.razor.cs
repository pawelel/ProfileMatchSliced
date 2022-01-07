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
using System.Threading.Tasks;

namespace ProfileMatch.Components.Admin
{
    public partial class AdminCategoryList : ComponentBase
    {
        
        [Inject]
        public ISnackbar Snackbar { get; set; }

        [Inject]
        private IDialogService DialogService { get; set; }

        [Inject] DataManager<Category, ApplicationDbContext> CategoryRepository { get; set; }

        private async Task<List<Category>> GetCategoriesAsync()
        {
            return await CategoryRepository.Get(include: c => c.Include(c => c.Questions));
        }

        protected override async Task OnInitializedAsync()
        {
            Categories = await GetCategoriesAsync();
        }

        private string searchString1 = "";
        private Category selectedItem1 = null;
        private List<Category> Categories = new();

        private bool FilterFunc1(Category category) => FilterFunc(category, searchString1);

        private static bool FilterFunc(Category category, string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (category.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (category.Description != null && category.Description.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            return false;
        }

        private async Task CategoryUpdate(Category category)
        {
            var parameters = new DialogParameters { ["Cat"] = category };
            var dialog = DialogService.Show<AdminCategoryDialog>("Edytuj kategorie", parameters);
            await dialog.Result;
        }

        private async Task CategoryCreate()
        {
            var dialog = DialogService.Show<AdminCategoryDialog>("Stwórz kategorie");
            await dialog.Result;
            Categories = await GetCategoriesAsync();
        }

        private async Task QuestionCreate(Category category)
        {
            var parameters = new DialogParameters { ["CategoryId"] = category.Id };
            var dialog = DialogService.Show<AdminQuestionDialog>($"Dodaj pytanie dla: {category.Name}", parameters);
            await dialog.Result;
            Categories = await GetCategoriesAsync();
        }

        [Inject]
        private IStringLocalizer<LanguageService> L { get; set; }
    }
}