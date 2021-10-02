
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

using Microsoft.AspNetCore.Components;

using ProfileMatch.Contracts;

using ProfileMatch.Models.Models;
using System.Linq;
using MudBlazor;
using ProfileMatch.Components.Dialogs;

namespace ProfileMatch.Components.Admin
{
    public partial class CategoriesList : ComponentBase
    {
        [Inject]
        public ISnackbar Snackbar { get; set; }
        [Inject]
        IDialogService DialogService { get; set; }
        [Inject]
        public ICategoryRepository CategoryRepository { get; set; }

        private async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await CategoryRepository.GetCategoriesWithQuestions();
        }

        protected override async Task OnInitializedAsync()
        {
            Categories = await GetCategoriesAsync();
        }

        private bool dense = false;
        private bool hover = true;
        private bool striped = false;
        private bool bordered = false;
        private string searchString1 = "";
        private Category selectedItem1 = null;
        private IEnumerable<Category> Categories = new List<Category>();

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
        async Task CategoryUpdate(Category category)
        {
            var parameters = new DialogParameters { ["Cat"] = category };
            var dialog = DialogService.Show<EditCategoryDialog>("Update Category", parameters);
            await dialog.Result;
        }
        async Task CategoryCreate()
        {
            var dialog = DialogService.Show<EditCategoryDialog>("Create Category");
            await dialog.Result;
        }
    }
}