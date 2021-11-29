using System.Collections.Generic;

namespace ProfileMatch.Models.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual List<Question> Questions { get; set; }
        public virtual List<UserCategory> UserNeedCategories { get; set; }
    }
}