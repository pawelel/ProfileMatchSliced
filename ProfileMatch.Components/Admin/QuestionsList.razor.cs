using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;

using ProfileMatch.Models.Responses;


namespace ProfileMatch.Components.Admin
{
    public partial class QuestionsList : ComponentBase
    {
        bool loading;
        [Parameter] public int id { get; set; }
        List<Question> questions;
        List<Question> questions1;
        List<Category> categories;
        private HashSet<string> options { get; set; } = new HashSet<string>() { };
        private string value { get; set; } = "Nothing selected";
        public bool ShowDetails { get; set; }
        ServiceResponse<List<Category>> responseCategories = new();
        ServiceResponse<List<Question>> responseQuestions = new();
        public DateTime? _dob;
        protected override async Task OnInitializedAsync()
        {
            loading = true;
            responseCategories = await categoryService.GetCategories();
            responseQuestions = await questionService.GetQuestionsWithCategories();
            questions1 = questions;
            categories = responseCategories.Data;
            questions = responseQuestions.Data;
            loading = false;
        }
        bool dense = true;
        bool hover = true;
        bool bordered = true;
        bool striped = false;
        private string searchString1 = "";
        private Question selectedItem1 = null;
        private HashSet<Question> selectedItems = new HashSet<Question>();
        private bool FilterFunc1(Question question) => FilterFunc(question, searchString1);

        private bool FilterFunc(Question question, string searchString)
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
            if (options.Count() == 0)
            {
                questions1 = questions;
            }
            else
            {


                questions1 = (List<Question>)(from q in questions
                                                from o in options
                                                where q.Category.Name == o
                                                select q);
            }
            return questions1;
        }
        IEnumerable<Category> GetCategories()
        {

            questions1 = (List<Question>)questions.GroupBy(x => x.Category.Name).Select(y => y.First()).Distinct();
            categories = (List<Category>)(from q in questions1
                                            where q.Category.Name != null
                                            select q.Category);

            return categories;
        }
    }
}
