using Ganss.Excel;

using System.Collections.Generic;

namespace ProfileMatch.Models.Models
{
    public class AnswerOption
    {
        public int Id { get; set; }
        public int ClosedQuestionId { get; set; }
        public int Level { get; set; }
        [Ignore]
        public ClosedQuestion ClosedQuestion { get; set; }
        public string Description { get; set; }
        public string DescriptionPl { get; set; }
        [Ignore]
        public List<UserClosedAnswer> UserClosedAnswers { get; set; } = new();
    }
}