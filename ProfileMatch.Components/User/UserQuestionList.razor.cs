using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;

using MudBlazor;

using ProfileMatch.Components.Dialogs;
using ProfileMatch.Contracts;
using ProfileMatch.Models.Models;
using ProfileMatch.Repositories;

namespace ProfileMatch.Components.User
{
    public partial class UserQuestionList : ComponentBase
    {
        [Inject]
        IDialogService DialogService { get; set; }
        [Inject]
        private ICategoryRepository CategoryRepository { get; set; }
        [Inject]
        private IQuestionRepository QuestionRepository { get; set; }
        private bool loading;
        private Question Q = new();
        [Parameter] public int Id { get; set; }
        [Parameter] public string UserId { get; set; }
        private List<Question> questions = new();
        private List<Question> questions1;
        private List<Category> categories;
        private HashSet<string> Options { get; set; } = new HashSet<string>() { };

        protected override async Task OnParametersSetAsync()
        {
            loading = true;

            if (UserId is not null)
            {
            questions = await QuestionRepository.GetActiveQuestionsWithCategoriesAndOptions();
            }
            questions1 = questions;
            loading = false;
        }
        protected override async Task OnInitializedAsync()
        {
            loading = true;
            categories = await CategoryRepository.GetCategories();
            loading = false;
        }

        private bool dense = true;
        private bool hover = true;
        private bool bordered = true;
        private bool striped = false;
        private int Level;
        private string searchString1 = "";
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
        private async Task UserAnswerDialog(Question question)
        {
            
            DialogOptions maxWidth = new() { MaxWidth = MaxWidth.Large, FullWidth = true };
            var parameters = new DialogParameters
            {
                ["Q"] = question,
                ["UserId"] = UserId
            };
            var dialog = DialogService.Show<UserQuestionDialog>($"{question.Name}", parameters, maxWidth);
           var data = (await dialog.Result).Data;
            var answer = (UserAnswer)data;
            var query = question.AnswerOptions.Where(o => o.Id == answer.AnswerOptionId).FirstOrDefault();

            Level = query.Level;
        }
        int ShowLevel(Question question)
        {
            if (Level!=0)
            {
                return Level;
            }
            //find user answer
            // select level for answer option and user answer
            var query1 = question.UserAnswers.Where(a => a.ApplicationUserId == UserId).FirstOrDefault();
            var query2 = question.AnswerOptions.Where(o => o.Id == query1.AnswerOptionId).FirstOrDefault();
            Level = query2.Level;
            return Level;
        }
    }
}