using Ganss.Excel;

namespace ProfileMatch.Models.Entities
{
    public class UserCategory
    {
        public bool IsSelected { get; set; }
        public string ApplicationUserId { get; set; }
        public int CategoryId { get; set; }
        [Ignore]
        public Category Category { get; set; }
        [Ignore]
        public ApplicationUser ApplicationUser { get; set; }
    }
}