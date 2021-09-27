using ProfileMatch.Models.Models;

namespace ProfileMatch.Models.ViewModels
{
    public class AnswerOptionVM
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public int Level { get; set; }
        public QuestionVM Question { get; set; }
        public string Description { get; set; }
    }
}