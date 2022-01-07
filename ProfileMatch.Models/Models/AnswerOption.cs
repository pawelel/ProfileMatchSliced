using Ganss.Excel;

using System.Collections.Generic;

namespace ProfileMatch.Models.Models
{
    public class AnswerOption
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public int Level { get; set; }
        [Ignore]
        public Question Question { get; set; }
        public string Description { get; set; }
        [Ignore]
        public List<UserAnswer> UserAnswers { get; set; } = new();
    }
}