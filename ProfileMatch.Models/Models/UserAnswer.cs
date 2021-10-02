namespace ProfileMatch.Models.Models
{
    public class UserAnswer
    {
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual AnswerOption AnswerOption { get; set; }
        public int AnswerOptionId { get; set; }
        public bool IsConfirmed { get; set; }
        public string SupervisorId { get; set; }
    }
}