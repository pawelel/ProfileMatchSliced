using System.Collections.Generic;

namespace ProfileMatch.Models.ViewModels
{
    public class CategoryVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<QuestionVM> Questions { get; set; }
    }
}