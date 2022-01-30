using Ganss.Excel;

using System;

namespace ProfileMatch.Models.Entities
{
    public class UserClosedAnswer
    {
        public string ApplicationUserId { get; set; }
        [Ignore]
        public ApplicationUser ApplicationUser { get; set; }
        [Ignore]
        public AnswerOption AnswerOption { get; set; }
        public int? AnswerOptionId { get; set; }
        public bool IsConfirmed { get; set; }
        public DateTime? LastModified { get; set; }
        public int ClosedQuestionId { get; set; }
        [Ignore]
        public ClosedQuestion ClosedQuestion { get; set; }
    }

}