using AutoMapper;

using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

using MudBlazor;

using ProfileMatch.Components.Admin.Dialogs;
using ProfileMatch.Data;
using ProfileMatch.Models.Entities;
using ProfileMatch.Models.ViewModels;
using ProfileMatch.Repositories;
using ProfileMatch.Services;

using Serilog;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileMatch.Components.Admin
{
    public partial class AdminClosedQuestions : ComponentBase
    {
        [Inject] ISnackbar Snackbar { get; set; }
        [Inject] ILogger Logger { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        [Inject] private IDialogService DialogService { get; set; }
        [Inject] IMapper Mapper { get; set; }
        [Inject] IUnitOfWork UnitOfWork { get; set; }
        private bool _loading;
        [Parameter] public int Id { get; set; }

        private List<Category> _categories;
        private List<ClosedQuestion> _questions;
        List<ClosedQuestionVM> _questionVMs;
        private IEnumerable<string> Cats { get; set; } = new HashSet<string> { };


        protected override async Task OnInitializedAsync()
        {
            await LoadData();

        }

        private async Task LoadData()
        {
            _loading = true;
            _categories = await UnitOfWork.Categories.Get();
            _questions = await UnitOfWork.ClosedQuestions.Get(include: c => c.Include(q => q.Category));
            _questionVMs = Mapper.Map<List<ClosedQuestionVM>>(_questions);
            
            
            Cats = new HashSet<string>() { };
            _loading = false;
        }


        async Task HandleSelection(int id, string selection)
        {
            switch (id)
            {
                case 0:
                    await CategoryUpdate(selection);
                    break;
                case 1:
                    await QuestionDialog(null, selection);
                    break;
                default:
                    await CategoryUpdate(selection);
                    break;
            }
        }

        private async Task CategoryUpdate(string category)
        {
            int _catId;
            if (ShareResource.IsEn())
            {
                _catId = (await UnitOfWork.Categories.GetOne(c => c.Name == category)).Id;
            }
            else
            {
                _catId = (await UnitOfWork.Categories.GetOne(c => c.NamePl == category)).Id;
            }
            try
            {
                var parameters = new DialogParameters { ["CategoryId"] = _catId };
                var dialog = DialogService.Show<AdminCategoryDialog>(L["Edit Category"], parameters);
                await dialog.Result;
                await LoadData();
            }
            catch (Exception ex)
            {
                Snackbar.Add($"{L["Error"]}: {ex.Message}");
                Logger.Error("{ex}", ex);

            }
        }
        private async Task CategoryCreate()
        {
            var dialog = DialogService.Show<AdminCategoryDialog>(L["Create Category"]);
            await dialog.Result;
            await LoadData();
        }

        private string _searchString = "";
        private ClosedQuestionVM _selectedItem1 = null;

        private Func<ClosedQuestionVM, bool> QuickFilter => question =>
       {
           if (string.IsNullOrWhiteSpace(_searchString))
               return true;
           if (ShareResource.IsEn())
           {
               if (question.CategoryName.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                   return true;
               if (question.Name.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                   return true;
           }
           else
           {
               if (question.CategoryNamePl.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                   return true;
               if (question.NamePl.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                   return true;
           }
           return false;
       };

        private List<ClosedQuestionVM> GetQuestions()
        {
            try
            {
                if (!Cats.Any())
                {
                    return _questionVMs;
                }
                else
                {
                    if (ShareResource.IsEn())
                    {
                        return (from q in _questionVMs
                                from c in Cats
                                where q.CategoryName == c
                                select q).ToList();
                    }
                    else
                    {
                        return (from q in _questionVMs
                                from c in Cats
                                where q.CategoryNamePl == c
                                select q).ToList();
                    }
                }
            }
            catch (Exception ex)
            {

                Log.Warning("ex", ex);
            }
            return null;

        }

        private async Task QuestionDialog(ClosedQuestionVM cqVM = null, string category = "")
        {
            string create;
            string update;
            DialogParameters parameters;
            if (cqVM != null && cqVM.Id > 0)
            {
                update = ShareResource.IsEn()
                    ? $"Edit Question: {cqVM.CategoryName}: {cqVM.Name} "
                    : $"Edytuj pytanie: {cqVM.CategoryNamePl}: {cqVM.NamePl}";
                parameters = new DialogParameters { ["QuestionId"] = cqVM.Id };
                var dialog = DialogService.Show<AdminClosedQuestionDialog>(update, parameters);
                await dialog.Result;
                return;
            }
            if (!string.IsNullOrWhiteSpace(category))
            {
                if (ShareResource.IsEn())
                {
                    create = $"Create Question for: {category}";
                }
                else
                {
                    create = $"Nowe pytanie dla: {category}";
                }

                parameters = new DialogParameters { ["CategoryName"] = category };
                var dialog = DialogService.Show<AdminClosedQuestionDialog>(create, parameters);
                await dialog.Result;
                NavigationManager.NavigateTo("admin/dashboard");
            }
        }
    }
}