using Ganss.Excel;

using System.Collections.Generic;

namespace ProfileMatch.Models.Models
{
    public class Question
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        [Ignore]
        public Category Category { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
        [Ignore]
        public List<AnswerOption> AnswerOptions { get; set; } = new();
        [Ignore]
        public List<UserAnswer> UserAnswers { get; set; } = new();
    }
}