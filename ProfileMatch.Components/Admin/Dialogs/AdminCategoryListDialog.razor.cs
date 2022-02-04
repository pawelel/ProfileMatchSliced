using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using MudBlazor;
using ProfileMatch.Models.Entities;

using ProfileMatch.Repositories;

namespace ProfileMatch.Components.Admin.Dialogs
{
    public partial class AdminCategoryListDialog : ComponentBase
    {
        bool _loading;
        [Inject] IUnitOfWork UnitOfWork { get; set; }
        [Inject] private IDialogService DialogService { get; set; }
        private IEnumerable<Category> _categories = new List<Category>();
        private string _searchString;
        protected override async Task OnInitializedAsync()
        {
            _loading = true;
            _categories = await UnitOfWork.Categories.Get();
            _loading = false;
        }
        // quick filter - filter gobally across multiple columns with the same input
        private Func<Category, bool> QuickFilter => x =>
        {
            if (string.IsNullOrWhiteSpace(_searchString))
                return true;

            if (x.Name.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            if (x.NamePl.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            return false;
        };
        private async Task CategoryUpdate(Category category = null)
        {
            if (category == null)
            {
                var dialog = DialogService.Show<AdminCategoryDialog>(L["Create Category"]);
                await dialog.Result;
            }
            else
            {
                var parameters = new DialogParameters { ["CategoryId"] = category.Id };
                var dialog = DialogService.Show<AdminCategoryDialog>(L["Edit Category"], parameters);
                await dialog.Result;
            }

        }
    }
}
