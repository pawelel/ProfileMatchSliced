using Ganss.Excel;

using System.Collections.Generic;

namespace ProfileMatch.Models.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [Ignore]
        public virtual List<ClosedQuestion> ClosedQuestions { get; set; }
        [Ignore]
        public virtual List<UserCategory> UserCategories { get; set; }
    }
}