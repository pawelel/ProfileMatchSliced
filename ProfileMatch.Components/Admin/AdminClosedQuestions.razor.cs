using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

using MudBlazor;

using ProfileMatch.Components.Dialogs;
using ProfileMatch.Contracts;
using ProfileMatch.Data;
using ProfileMatch.Models.Models;
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

        [Inject] DataManager<Category, ApplicationDbContext> CategoryRepository { get; set; }
        [Inject] DataManager<ClosedQuestion, ApplicationDbContext> ClosedQuestionRepository { get; set; }
        bool deleteEnabled;
        private bool loading;
        [Parameter] public int Id { get; set; }
        private List<Category> categories;
        private List<ClosedQuestion> questions;
        List<ClosedQuestionVM> questionVMs;
        private IEnumerable<string> Cats { get; set; } = new HashSet<string> { };


        protected override async Task OnInitializedAsync()
        {
            await LoadData();

        }
        protected override void OnAfterRender(bool firstRender)
        {

            base.OnAfterRender(firstRender);
        }
        private async Task LoadData()
        {
            loading = true;
            categories = await CategoryRepository.Get();
            questions = await ClosedQuestionRepository.Get();
            questionVMs = (from c in categories
                           join q in questions on c.Id equals q.CategoryId
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
            foreach (var cat in categories)
            {
                if (!questionVMs.Any(q => q.CategoryId == cat.Id && q.CategoryId != 0))
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
                    questionVMs.Add(vm);
                }
            }
            Cats = new HashSet<string>() { };
            loading = false;
        }

        private async Task CategoryUpdate(string category)
        {
            var Cat = await CategoryRepository.GetOne(c => c.Name == category);
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

        private string searchString = "";
        private ClosedQuestionVM selectedItem1 = null;

        private Func<ClosedQuestionVM, bool> QuickFilter => question =>
       {
           if (string.IsNullOrWhiteSpace(searchString))
               return true;
           if (ShareResource.IsEn())
           {
               if (question.CategoryName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                   return true;
               if (question.QuestionName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                   return true;
           }
           else
           {
               if (question.CategoryNamePl.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                   return true;
               if (question.QuestionNamePl.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                   return true;
           }
           return false;
       };

        private List<ClosedQuestionVM> GetQuestions()
        {
            if (!Cats.Any())
            {
                return questionVMs;
            }
            else
            {
                if (ShareResource.IsEn())
                {
                    return (from q in questionVMs
                            from c in Cats
                            where q.CategoryName == c
                            select q).ToList();
                }
                else
                {
                    return (from q in questionVMs
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
                deleteEnabled = true;
            }
            else
            {
                title = create;
                deleteEnabled = false;
            }
            var parameters = new DialogParameters { ["Q"] = cqVM, ["DeleteEnabled"]=deleteEnabled };


            var dialog = DialogService.Show<AdminClosedQuestionDialog>(title, parameters);
            await dialog.Result;

        }
        [Inject] private IStringLocalizer<LanguageService> L { get; set; }
    }
}