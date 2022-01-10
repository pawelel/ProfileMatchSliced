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
        List<UserCategoryVM> UserCategoryVMs = new();
        List<UserCategoryVM> publicCategoriesVM=new();
        [Inject] DataManager<UserCategory, ApplicationDbContext> UserCategoryManager { get; set; }
        [Inject] DataManager<Category, ApplicationDbContext> CategoryManager { get; set; }
        [Parameter] public ApplicationUser CurrentUser { get; set; }
        List<UserCategory> userCategories;
        List<Category> categories;
        protected override async Task OnInitializedAsync()
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            userCategories = await UserCategoryManager.Get(uc => uc.ApplicationUserId == CurrentUser.Id);
            categories = await CategoryManager.Get();
            UserCategoryVMs = (from uc in userCategories
                               join c in categories on uc.CategoryId equals c.Id
                               select new UserCategoryVM()
                               {
                                   CategoryName = c.Name,
                                   CategoryNamePl = c.NamePl,
                                   CategoryDescription = c.Description,
                                   CategoryDescriptionPl = c.DescriptionPl,
                                   CategoryId = uc.CategoryId,
                                   IsSelected = uc.IsSelected,
                                   UserId = CurrentUser.Id
                               }
                               ).ToList();
            UserCategoryVM ucVM;
            foreach (var cat in categories)
            {
                if (!UserCategoryVMs.Any(uc => uc.CategoryId == cat.Id))
                {
                    ucVM = new()
                    {
                        CategoryName = cat.Name,
                        CategoryNamePl = cat.NamePl,
                        CategoryDescription = cat.DescriptionPl,
                        CategoryDescriptionPl = cat.DescriptionPl,
                        CategoryId = cat.Id,
                        IsSelected = false,
                        UserId = CurrentUser.Id
                    };
                    UserCategory uc = new()
                    {
                        ApplicationUserId = CurrentUser.Id,
                        CategoryId = cat.Id,
                        IsSelected = false
                    };
                    await UserCategoryManager.Insert(uc);
                    UserCategoryVMs.Add(ucVM);
                }
            }
            publicCategoriesVM = UserCategoryVMs.Where(c => c.IsSelected).ToList();
        }

        bool edit = false;
        async Task<bool> SetCategoryAsync(UserCategoryVM category)
        {
            var data = await UserCategoryManager.GetOne(c => c.ApplicationUserId == CurrentUser.Id && c.CategoryId == category.CategoryId);
            if (data != null)
            {
                data.IsSelected = category.IsSelected;
                await UserCategoryManager.Update(data);
            }
            if (category.IsSelected)
            {
                if (ShareResource.IsEn())
                {
                    Snackbar.Add(L["Category"] + $" {category.CategoryName} {L["checked[F]"]}", Severity.Success);
                }if (!ShareResource.IsEn())
                {
                    Snackbar.Add(L["Category"] + $" {category.CategoryNamePl} {L["checked[F]"]}", Severity.Success);
                }
            }
            else {
                if (ShareResource.IsEn())
                {

                Snackbar.Add(L["Category"] + $" { category.CategoryName} { L["unchecked[F]"]}", Severity.Success);
                }
                if (!ShareResource.IsEn())
                {
                    Snackbar.Add(L["Category"] + $" { category.CategoryNamePl} { L["unchecked[F]"]}", Severity.Success);
                }
            }
            await LoadData();
            return category.IsSelected;
        }
    }
}