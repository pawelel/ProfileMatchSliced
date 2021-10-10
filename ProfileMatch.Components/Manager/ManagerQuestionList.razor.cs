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
    public partial class ManagerQuestionList : ComponentBase
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
        private List<QuestionUserLevelVM> questions1;
        private List<Question> questions2;
        private List<Question> questions2Filtered;
        private List<Category> categories;
        private List<QuestionUserLevelVM> users;
        private HashSet<string> Cats { get; set; } = new() { };
        private HashSet<string> Quests { get; set; } = new() { };
        private HashSet<string> Ppl { get; set; } = new() { };
        public bool ShowDetails { get; set; }

        protected override async Task OnInitializedAsync()
        {
            loading = true;
            users = await UserRepository.GetUsersWithQuestionAnswerLevel();
            categories = await CategoryRepository.GetCategories();
            questions = await UserRepository.GetUsersWithQuestionAnswerLevel();
            questions2 = await QuestionRepository.GetQuestionsWithCategories();
            questions1 = questions;
            loading = false;
        }

        private bool dense = true;
        private bool hover = true;
        private bool bordered = true;
        private bool striped = false;
        private string searchString1 = "";

        private bool FilterFunc1(QuestionUserLevelVM question) => FilterFunc(question, searchString1);

        private static bool FilterFunc(QuestionUserLevelVM question, string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (question.CategoryName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (question.QuestionName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            return false;
        }

        private List<QuestionUserLevelVM> GetQuestions()
        {
            if (Cats.Count==0)
            {
                questions2Filtered = questions2;
            }
            questions2Filtered = (from q in questions2
                          from c in Cats
                          where q.Category.Name == c
                          select q).ToList();
            if (Cats.Count == 0)
            {
                questions1 = questions;
            }
            else
            {
                questions1 = (from q in questions
                              from c in Cats
                              where q.CategoryName == c
                              select q).ToList();
            }
            if (Quests.Count != 0)
            {
                questions1 = (from q in questions1
                              from a in Quests
                              where q.QuestionName == a
                              from c in Cats
                              where q.CategoryName == c
                              select q).ToList();
            }

            return questions1;
        }

        private async Task QuestionDisplay(int id)
        {

            var question1 = await QuestionRepository.FindById(id);
            DialogOptions maxWidth = new() { MaxWidth = MaxWidth.Large, FullWidth = true };
            var parameters = new DialogParameters { ["Q"] = question1 };
            var dialog = DialogService.Show<ManagerQuestionDisplay>($"{question1.Name}", parameters, maxWidth);
        }

        private readonly TableGroupDefinition<QuestionUserLevelVM> _groupDefinition = new()
        {
            GroupName = "Category",
            Indentation = false,
            Expandable = true,
            IsInitiallyExpanded = false,
            Selector = (e) => e.CategoryName,
            InnerGroup = new TableGroupDefinition<QuestionUserLevelVM>()
            {
                GroupName = "Person",
                Expandable = true,
                Selector = (e)=>e.FullName
            }
        };
    }
}