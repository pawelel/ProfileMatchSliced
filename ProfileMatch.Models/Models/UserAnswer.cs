using System;

namespace ProfileMatch.Models.Models
{
    public class UserAnswer
    {
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public AnswerOption AnswerOption { get; set; }
        public int? AnswerOptionId { get; set; }
        public bool IsConfirmed { get; set; }
        public DateTime? LastModified { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }
}