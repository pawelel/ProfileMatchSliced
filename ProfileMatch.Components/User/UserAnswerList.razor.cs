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
    public partial class UserAnswerList : ComponentBase
    {
        [Inject]
        IDialogService DialogService { get; set; }
        [Inject]
        private ICategoryRepository CategoryRepository { get; set; }
        [Inject]
        private IQuestionRepository QuestionRepository { get; set; }
        private bool loading;
        [Parameter] public int Id { get; set; }
       [Parameter] public string UserId { get; set; }
        private List<Question> questions=new();
        private List<Question> questions1;
        private List<Category> categories;
        private HashSet<string> Options { get; set; } = new HashSet<string>() { };

        protected override async Task OnInitializedAsync()
        {
            loading = true;
            categories = await CategoryRepository.GetCategories();
            questions = await QuestionRepository.GetActiveQuestionsWithCategoriesAndOptionsForUser(UserId);
            questions1 = questions;
            loading = false;
            Console.WriteLine(UserId);
        }

        private bool dense = true;
        private bool hover = true;
        private bool bordered = true;
        private bool striped = false;
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
        private async Task QuestionDetailsDialog(Question question)
        {
            DialogOptions maxWidth = new() { MaxWidth=MaxWidth.Large, FullWidth = true };
            var parameters = new DialogParameters { ["Q"] = question,
                ["UserId"]=UserId
            };
            var dialog = DialogService.Show<UserQuestionDetails>($"{question.Name}", parameters, maxWidth);
            await dialog.Result;
        }
        int ShowLevel(Question question)
        {
            //select answerOption.Level from question.AnswerOptions where answerOption.QuestionId == question.Id and userAnswer.UserId == UserId from question.UserAnswers
           return (from a in question.UserAnswers
            where a.QuestionId == question.Id && a.ApplicationUserId == UserId
            from o in question.AnswerOptions
            where o.QuestionId == question.Id
            select o.Level).FirstOrDefault();
            
        }
    }
}