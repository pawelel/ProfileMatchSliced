using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;

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
        [Inject] private ISnackbar Snackbar { get; set; }
        [Parameter] public string UserID { get; set; }

        private List<UserCategoryVM> _userCategoryVMs;

        [Inject] IUnitOfWork UnitOfWork { get; set; }
        
        [Parameter] public ApplicationUser CurrentUser { get; set; }

        private List<UserCategory> _userCategories;
        private List<Category> _categories;
        private bool _edit;

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
            _userCategoryVMs = new();
            _userCategories = await UnitOfWork.UserCategories.Get(uc => uc.ApplicationUserId == CurrentUser.Id);
            if (_userCategories == null)
            {
                _userCategories = new();
            }

            _categories = await UnitOfWork.Categories.Get();
            if (_categories == null)
            {
                _categories = new();
                return;
            }
            foreach ((Category c, UserCategoryVM u) in from Category c in _categories
                                                       let u = new UserCategoryVM()
                                                       {
                                                           CategoryName = c.Name,
                                                           CategoryNamePl = c.NamePl,
                                                           CategoryDescription = c.Description,
                                                           CategoryDescriptionPl = c.DescriptionPl,
                                                           CategoryId = c.Id,
                                                           IsSelected = _userCategories.Any(q => q.CategoryId == c.Id && q.IsSelected),
                                                           UserId = CurrentUser.Id
                                                       }
                                                       select (c, u))
            {
                _userCategoryVMs.Add(u);
                if (!_userCategories.Any(a => a.CategoryId == c.Id))
                {
                    UserCategory uc = new()
                    {
                        ApplicationUserId = CurrentUser.Id,
                        CategoryId = c.Id,
                        IsSelected = false
                    };
                    await UnitOfWork.UserCategories.Insert(uc);
                }
            }
        }

        //updates list of categories
        private async Task SetCategoryAsync(UserCategoryVM userCategory)
        {
            UserCategory data = await UnitOfWork.UserCategories.GetOne(c => c.ApplicationUserId == CurrentUser.Id && c.CategoryId == userCategory.CategoryId);
            if (data != null && data.IsSelected != userCategory.IsSelected)
            {
                data.IsSelected = userCategory.IsSelected;
                await UnitOfWork.UserCategories.Update(data);
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