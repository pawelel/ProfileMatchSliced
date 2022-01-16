using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

using MudBlazor;

using ProfileMatch.Data;
using ProfileMatch.Models.Models;
using ProfileMatch.Models.ViewModels;
using ProfileMatch.Repositories;
using ProfileMatch.Services;

namespace ProfileMatch.Components.User
{
    public partial class UserCategoryList : ComponentBase
    {
        [Inject] ISnackbar Snackbar { get; set; }
        [Inject] private IStringLocalizer<LanguageService> L { get; set; }
        [Parameter] public string UserID { get; set; }
        List<UserCategoryVM> UserCategoryVMs;

        [Inject] DataManager<UserCategory, ApplicationDbContext> UserCategoryManager { get; set; }
        [Inject] DataManager<Category, ApplicationDbContext> CategoryRepository { get; set; }
        [Parameter] public ApplicationUser CurrentUser { get; set; }
        List<UserCategory> userCategories;
        List<Category> categories;
        bool edit;

        protected override async Task OnInitializedAsync()
        {
            await LoadUserCategories();
        }

        /// <summary>
        /// Load or create user categories if they doesn't exist
        /// </summary>
        /// <returns></returns>
        private async Task LoadUserCategories()
        {
            UserCategoryVMs = new();
            userCategories = await UserCategoryManager.Get(uc => uc.ApplicationUserId == CurrentUser.Id);
            if (userCategories == null) userCategories = new();
            categories = await CategoryRepository.Get();
            if (categories == null)
            {
                categories = new();
                return;
            }
            foreach (var (c, u) in from Category c in categories
                                   let u = new UserCategoryVM()
                                   {
                                       CategoryName = c.Name,
                                       CategoryNamePl = c.NamePl,
                                       CategoryDescription = c.Description,
                                       CategoryDescriptionPl = c.DescriptionPl,
                                       CategoryId = c.Id,
                                       IsSelected = userCategories.Any(q => q.CategoryId == c.Id && q.IsSelected),
                                       UserId = CurrentUser.Id
                                   }
                                   select (c, u))
            {
                UserCategoryVMs.Add(u);
                if (!userCategories.Any(a => a.CategoryId == c.Id))
                {
                    UserCategory uc = new()
                    {
                        ApplicationUserId = CurrentUser.Id,
                        CategoryId = c.Id,
                        IsSelected = false
                    };
                    await UserCategoryManager.Insert(uc);
                }
            }
        }

        //updates list of categories
        async Task SetCategoryAsync(UserCategoryVM userCategory)
        {
            var data = await UserCategoryManager.GetOne(c => c.ApplicationUserId == CurrentUser.Id && c.CategoryId == userCategory.CategoryId);
            if (data != null && data.IsSelected != userCategory.IsSelected)
            {
                data.IsSelected = userCategory.IsSelected;
                await UserCategoryManager.Update(data);
            }
            string title = ShareResource.IsEn() ? userCategory.CategoryName : userCategory.CategoryNamePl;
            if (userCategory.IsSelected)
            {
                Snackbar.Add(L["Category"] + $" {title} {L["checked[F]"]}", Severity.Success);
            }
            else
            {
                Snackbar.Add(L["Category"] + $" { userCategory.CategoryName} { L["unchecked[F]"]}", Severity.Success);
            }
        }
    }
}