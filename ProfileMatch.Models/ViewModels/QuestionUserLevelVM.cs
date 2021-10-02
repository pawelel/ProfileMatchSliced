namespace ProfileMatch.Models.ViewModels
{
    public class QuestionUserLevelVM
    {
        public int QuestionId { get; set; }
        public string QuestionName { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Level { get; set; }
    }
}