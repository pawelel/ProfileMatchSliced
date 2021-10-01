using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;

using ProfileMatch.Contracts;
using ProfileMatch.Models.Models;
using ProfileMatch.Services;


namespace ProfileMatch.Components.Admin
{
    public partial class QuestionsList : ComponentBase
    {
        [Inject]
        ICategoryRepository CategoryRepository { get; set; }
        [Inject]
        IQuestionRepository QuestionRepository { get; set; }
        [Inject]
        IAnswerOptionRepository AnswerOptionRepository { get; set; }

        bool loading;
        [Parameter] public int Id { get; set; }
        List<Question> questions;
        List<Question> questions1;
        List<Category> categories;
        private HashSet<string> Options { get; set; } = new HashSet<string>() { };
        private string Value { get; set; } = "Nothing selected";
        public bool ShowDetails { get; set; }
        protected override async Task OnInitializedAsync()
        {
            loading = true;
            categories = await CategoryRepository.GetCategories();
            questions = await QuestionRepository.GetQuestionsWithCategories();
            questions1 = questions;
            loading = false;
        }
        bool dense = true;
        bool hover = true;
        bool bordered = true;
        bool striped = false;
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

        List<Question> GetQuestions()
        {
            if (Options.Count == 0)
            {
                questions1 = questions;
            }
            else
            {


                questions1 = (List<Question>)(from q in questions
                                              from o in Options
                                              where q.Category.Name == o
                                              select q);
            }
            return questions1;
        }
        
    }
}
