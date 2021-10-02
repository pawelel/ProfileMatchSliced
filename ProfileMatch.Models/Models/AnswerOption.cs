using System.Collections.Generic;

namespace ProfileMatch.Models.Models
{
    public class AnswerOption
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public int Level { get; set; }
        public virtual Question Question { get; set; }
        public string Description { get; set; }
        public virtual List<UserAnswer> UserAnswers { get; set; }
    }
}