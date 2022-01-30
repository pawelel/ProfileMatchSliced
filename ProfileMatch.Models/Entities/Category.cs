using Ganss.Excel;

using System.Collections.Generic;

namespace ProfileMatch.Models.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NamePl { get; set; }
        public string Description { get; set; }
        public string DescriptionPl { get; set; }
        [Ignore]
        public virtual List<ClosedQuestion> ClosedQuestions { get; set; }
        [Ignore]
        public virtual List<UserCategory> UserCategories { get; set; }
    }
}