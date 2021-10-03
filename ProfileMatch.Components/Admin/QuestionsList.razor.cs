using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;

using MudBlazor;

using ProfileMatch.Components.Dialogs;
using ProfileMatch.Contracts;
using ProfileMatch.Models.Models;

namespace ProfileMatch.Components.Admin
{
    public partial class QuestionsList : ComponentBase
    {
        [Inject]
        IDialogService DialogService { get; set; }
        [Inject]
        private ICategoryRepository CategoryRepository { get; set; }

        [Inject]
        private IQuestionRepository QuestionRepository { get; set; }

        [Inject]
        private IAnswerOptionRepository AnswerOptionRepository { get; set; }

        private bool loading;
        [Parameter] public int Id { get; set; }
        private List<Question> questions=new();
        private List<Question> questions1;
        private List<Category> categories;
        string Edit { get; set; } = "";
        private HashSet<string> Options { get; set; } = new HashSet<string>() { };
        private string Value { get; set; } = "Nothing selected";
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
        private readonly HashSet<Question> selectedItems = new();

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
            if (Options.Count == 0)
            {
                questions1 = questions;
            }
            else
            {
                questions1 = (from q in questions
                              from o in Options
                              where q.Category.Name == o
                              select q).ToList();
            }
            return questions1;
        }
        private async Task EditQuestionDialog(Question question)
        {
            var parameters = new DialogParameters { ["Q"] = question };
            var dialog = DialogService.Show<EditQuestionDialog>($"Edit Question {question.Name}", parameters);
            await dialog.Result;
            
        }
    }
}