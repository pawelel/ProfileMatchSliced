using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;

using MudBlazor;

using ProfileMatch.Components.Dialogs;
using ProfileMatch.Contracts;
using ProfileMatch.Models.Models;
using ProfileMatch.Models.ViewModels;

namespace ProfileMatch.Components.Manager
{
    public partial class ManagerTestTable : ComponentBase
    {
        [Inject]
        private IDialogService DialogService { get; set; }

        [Inject]
        private ICategoryRepository CategoryRepository { get; set; }

        [Inject]
        private IUserRepository UserRepository { get; set; }

        [Inject]
        private IQuestionRepository QuestionRepository { get; set; }

        private bool loading;
        [Parameter] public int Id { get; set; }
        private List<QuestionUserLevelVM> questions = new();
        private List<Category> categories;
        private List<ApplicationUser> users;
        private HashSet<string> Cats { get; set; } = new() { };
        private HashSet<string> Ppl { get; set; } = new() { };

        protected override async Task OnInitializedAsync()
        {
            loading = true;
            categories = await CategoryRepository.GetCategories();
            users = await UserRepository.GetAll();
            questions = await UserRepository.GetUsersWithQuestionAnswerLevel();
            loading = false;
        }

        private bool dense = true;
        private bool hover = true;
        private bool bordered = true;
        private bool striped = false;
        private string searchString1 = "";

        //private bool FilterFunc2(QuestionUserLevelVM question) => FilterFunc(question, searchString1);
        private bool FilterFunc1(QuestionUserLevelVM question) => FilterFunc(question, searchString1);

        private static bool FilterFunc(QuestionUserLevelVM question, string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (question.CategoryName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (question.QuestionName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (question.FullName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (question.Level.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            return false;
        }

        private async Task QuestionDisplay(int id)
        {
            var question1 = await QuestionRepository.FindById(id);
            DialogOptions maxWidth = new() { MaxWidth = MaxWidth.Large, FullWidth = true };
            var parameters = new DialogParameters { ["Q"] = question1 };
            DialogService.Show<ManagerQuestionDisplay>($"{question1.Name}", parameters, maxWidth);
        }

        private readonly TableGroupDefinition<QuestionUserLevelVM> _groupDefinition = new()
        {
            GroupName = "Category",
            Indentation = true,
            Expandable = true,
            IsInitiallyExpanded = true,
            Selector = (e) => e.CategoryName,
            InnerGroup = new TableGroupDefinition<QuestionUserLevelVM>()
            {
                GroupName = "Question",
                Expandable = true,
                Selector = (e) => e.QuestionName
            }
        };

        private List<QuestionUserLevelVM> GetPpl(List<QuestionUserLevelVM> questions)
        {
            List<QuestionUserLevelVM> ppl = new();
            if (Ppl.Count != 0)
            {
                ppl = (from q in questions
                       from p in Ppl
                       where q.FullName == p
                       select q).ToList();
            }
            else
            {
                ppl = questions;
            }
            return ppl;
        }

        private List<QuestionUserLevelVM> GetCategoriesAndQuestions()
        {
            List<QuestionUserLevelVM> qs = new();

            
           
                qs = (from q in questions
                              from c in Cats
                              where q.CategoryName == c
                              select q).ToList();
            qs = GetPpl(qs);
            return qs;
        }
    }
}