using Ganss.Excel;

using System;

namespace ProfileMatch.Models.Models
{
    public class UserAnswer
    {
        public string ApplicationUserId { get; set; }
        [Ignore]
        public ApplicationUser ApplicationUser { get; set; }
        [Ignore]
        public AnswerOption AnswerOption { get; set; }
        public int? AnswerOptionId { get; set; }
        public bool IsConfirmed { get; set; }
        public DateTime? LastModified { get; set; }
        public int QuestionId { get; set; }
        [Ignore]
        public Question Question { get; set; }
    }

}