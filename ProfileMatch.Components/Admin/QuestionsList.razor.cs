using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;

using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using ProfileMatch.Models;
using ProfileMatch.Models.Models;
using ProfileMatch.Models.Profiles;
using ProfileMatch.Models.ViewModels;
using ProfileMatch.Components;
using ProfileMatch.Services;
using ProfileMatch.Contracts;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.JSInterop;
using ProfileMatch;
using ProfileMatch.Components.Culture;
using ProfileMatch.Components.Layout;
using ProfileMatch.Components.Theme;
using Microsoft.Extensions.Localization;
using ProfileMatch.Models.Enumerations;
using Microsoft.AspNetCore.Components;
using ProfileMatch.Repositories;
using System.Threading.Tasks;

namespace ProfileMatch.Components.Admin
{
    public partial class QuestionsList : ComponentBase
    {
        bool loading;
        [Parameter] public int id { get; set; }
        IEnumerable<QuestionVM> questions;
        IEnumerable<QuestionVM> questions1;
        IEnumerable<CategoryVM> categories;
        private HashSet<string> options { get; set; } = new HashSet<string>() { };
        private string value { get; set; } = "Nothing selected";
        public bool ShowDetails { get; set; }
        protected override async Task OnInitializedAsync()
{
loading = true;
            categories = await categoryService.GetCategories();
            questions = await questionService.GetQuestionsWithCategories();
            questions1 = questions;
            loading = false;
        }
        bool dense = true;
        bool hover = true;
        bool bordered = true;
        bool striped = false;
        private string searchString1 = "";
        private QuestionVM selectedItem1 = null;
        private HashSet<QuestionVM> selectedItems = new HashSet<QuestionVM>();
        private bool FilterFunc1(QuestionVM question) => FilterFunc(question, searchString1);

        private bool FilterFunc(QuestionVM question, string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (question.Category.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (question.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            return false;
        }
        IEnumerable<QuestionVM> GetQuestions()
        {
            if (options.Count() == 0)
            {
                questions1 = questions;
            }
            else
            {


                questions1 = from q in questions
                             from o in options
                             where q.Category.Name == o
                             select q;
            }
            //return questions1;
        }
        IEnumerable<Category> GetCategories()
        {

            questions1 = questions.GroupBy(x => x.Category.Name).Select(y => y.First()).Distinct();
            categories = from q in questions1
                         where q.Category.Name != null
                         select q.Category;

            return categories;
        }
    }
}
