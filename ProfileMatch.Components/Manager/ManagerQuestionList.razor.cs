using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;

using MudBlazor;

using ProfileMatch.Components.Dialogs;
using ProfileMatch.Contracts;
using ProfileMatch.Models.Models;

namespace ProfileMatch.Components.Manager
{
    public partial class ManagerQuestionList : ComponentBase
    {
        [Inject]
        IDialogService DialogService { get; set; }
        [Inject]
        private ICategoryRepository CategoryRepository { get; set; }

        [Inject]
        private IQuestionRepository QuestionRepository { get; set; }

        private bool loading;
        [Parameter] public int Id { get; set; }
        private List<Question> questions=new();
        private List<Question> questions1;
        private List<Category> categories;
        private HashSet<string> Cats { get; set; } = new () { };
        private HashSet<string> Quests { get; set; } = new() { };
        public bool ShowDetails { get; set; }

        protected override async Task OnInitializedAsync()
        {
            loading = true;
            categories = await CategoryRepository.GetCategories();
            questions = await QuestionRepository.GetQuestionsWithCategoriesAndOptions();
            questions1 = questions;
            loading = false;
        }

        private bool dense = true;
        private bool hover = true;
        private bool bordered = true;
        private bool striped = false;
        private string searchString1 = "";
        private Question selectedItem1 = null;
        private bool FilterFunc1(Question question) => FilterFunc(question, searchString1);

        private static bool FilterFunc(Question question, string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (question.Category.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (question.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            return false;
        }

        private List<Question> GetQuestions()
        {
            if (Cats.Count == 0)
            {
                questions1 = questions;
            }
            else
            {
                questions1 = (from q in questions
                              from c in Cats
                              where q.Category.Name == c
                              select q).ToList();
            }
            if (Quests.Count!=0)
            {
                questions1 = (from q in questions1
                             from a in Quests
                             where q.Name == a
                             select q).ToList();
            }

            return questions1;
        }
        private async Task QuestionDialog(Question question)
        {
            var parameters = new DialogParameters { ["Q"] = question };
            var dialog = DialogService.Show<AdminQuestionDialog>($"Edit Question {question.Name}", parameters);
            await dialog.Result;
            
        }
        private async Task QuestionDisplay(Question question)
        {
            DialogOptions maxWidth = new() { MaxWidth=MaxWidth.Large, FullWidth = true };
            var parameters = new DialogParameters { ["Q"] = question };
            var dialog = DialogService.Show<AdminQuestionDisplay>($"{question.Name}", parameters, maxWidth);
            await dialog.Result;
        }
    }
}