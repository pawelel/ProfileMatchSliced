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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileMatch.Components.Admin
{
    public partial class AdminClosedQuestions : ComponentBase
    {
        [Inject] ISnackbar Snackbar { get; set; }
        [Inject] private IDialogService DialogService { get; set; }

[Inject] IUnitOfWork UnitOfWork { get; set; }
        bool _deleteEnabled;
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
            _questions = await UnitOfWork.ClosedQuestions.Get();
            _questionVMs = (from c in _categories
                           join q in _questions on c.Id equals q.CategoryId
                           select new ClosedQuestionVM()
                           {
                               CategoryId = c.Id,
                               CategoryName = c.Name,
                               CategoryNamePl = c.NamePl,
                               IsActive = q.IsActive,
                               ClosedQuestionId = q.Id,
                               QuestionNamePl = q.NamePl,
                               QuestionName = q.Name,
                               Description = q.Description,
                               DescriptionPl = q.DescriptionPl
                           }).ToList();
            string nodata = "No questions yet";
            ClosedQuestionVM vm;
            string nodataPl = "Nie ma jeszcze pytań";
            foreach (var cat in _categories)
            {
                if (!_questionVMs.Any(q => q.CategoryId == cat.Id && q.CategoryId != 0))
                {
                    vm = new ClosedQuestionVM()
                    {
                        QuestionName = nodata,
                        QuestionNamePl = nodataPl,
                        IsActive = false,
                        CategoryId = cat.Id,
                        CategoryName = cat.Name,
                        CategoryNamePl = cat.NamePl
                    };
                    _questionVMs.Add(vm);
                }
            }
            Cats = new HashSet<string>() { };
            _loading = false;
        }

        private async Task CategoryUpdate(string category)
        {
            var Cat = await UnitOfWork.Categories.GetOne(c => c.Name == category);
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
               if (question.QuestionName.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                   return true;
           }
           else
           {
               if (question.CategoryNamePl.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                   return true;
               if (question.QuestionNamePl.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                   return true;
           }
           return false;
       };

        private List<ClosedQuestionVM> GetQuestions()
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

        private async Task QuestionDialog(ClosedQuestionVM cqVM)
        {
            string create;
            string update;
            string title;
            if (ShareResource.IsEn())
            {
                update = $"Edit Question {cqVM.CategoryName}: {cqVM.QuestionName} ";
                create = $"Create Question for {cqVM.CategoryName}";
            }
            else
            {
                update = $"Edytuj pytanie {cqVM.CategoryNamePl}: {cqVM.QuestionNamePl}";
                create = $"Nowe pytanie dla {cqVM.CategoryNamePl}";
            }
            if(cqVM.ClosedQuestionId > 0)
            {
                title = update;
                _deleteEnabled = true;
            }
            else
            {
                title = create;
                _deleteEnabled = false;
            }
            var parameters = new DialogParameters { ["Q"] = cqVM, ["DeleteEnabled"]=_deleteEnabled };


            var dialog = DialogService.Show<AdminClosedQuestionDialog>(title, parameters);
            await dialog.Result;

        }

    }
}