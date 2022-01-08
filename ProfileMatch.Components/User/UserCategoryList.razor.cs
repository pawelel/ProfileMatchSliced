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
        [Inject] DataManager<UserCategory, ApplicationDbContext> UserCategoryManager { get; set; }
        [Inject] DataManager<Category, ApplicationDbContext> CategoryManager { get; set; }
        [Parameter] public ApplicationUser CurrentUser { get; set; }
        List<UserCategory> userCategories;
        List<Category> categories;
        List<UserCategoryVM> publicCategoriesVM=new();
        protected override async Task OnInitializedAsync()
        {
            userCategories = await UserCategoryManager.Get(uc => uc.ApplicationUserId == CurrentUser.Id);
            categories = await CategoryManager.Get();
            UserCategoryVMs = (from uc in userCategories
                               join c in categories on uc.CategoryId equals c.Id
                               select new UserCategoryVM()
                               {
                                   CategoryName = c.Name,
                                   CategoryDescription = c.Description,
                                   CategoryId = uc.CategoryId,
                                   IsSelected = uc.Want,
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
                        CategoryDescription = cat.Description,
                        CategoryId = cat.Id,
                        IsSelected = false,
                        UserId = CurrentUser.Id
                    };
                    UserCategory uc = new()
                    {
                        ApplicationUserId = CurrentUser.Id,
                        CategoryId = cat.Id,
                        Want = false
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
                data.Want = category.IsSelected;
                await UserCategoryManager.Update(data);
            }
            if (category.IsSelected)
            {
            Snackbar.Add(L["Category"] + $" {L[category.CategoryName]} {L["checked"]}", Severity.Success);
            }
            else { Snackbar.Add(L["Category"] + $" { category.CategoryName} { L["unchecked"]}", Severity.Success);    
            }

          return  data.Want;
        }
    }
}