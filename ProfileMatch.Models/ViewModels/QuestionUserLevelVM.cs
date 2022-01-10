namespace ProfileMatch.Models.ViewModels
{
    public class QuestionUserLevelVM
    {
        public int ClosedQuestionId { get; set; }
        public string QuestionName { get; set; }
        public string QuestionNamePl { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string  Description { get; set; }
        public string  DescriptionPl { get; set; }
        public int Level { get; set; }
        public string FullName => $"{LastName}, {FirstName}";
        public string CategoryName { get; set; }
        public string CategoryNamePl { get; set; }
        public int IsUserCategory { get; set; }
        public int CategoryId { get; set; }
    }
}