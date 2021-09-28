using System.Collections.Generic;

namespace ProfileMatch.Models.ViewModels
{
    public class QuestionVM
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
        public CategoryVM Category { get; set; }
        public List<AnswerOptionVM> AnswerOptions { get; set; }
    }
}
