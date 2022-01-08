using Ganss.Excel;

namespace ProfileMatch.Models.Models
{
    public class UserOpenAnswer
    {
        public string ApplicationUserId { get; set; }
        [Ignore]
        public ApplicationUser ApplicationUser { get; set; }
        [Ignore]
        public OpenQuestion Note { get; set; }
        public int NoteId { get; set; }
        public bool IsDisplayed { get; set; }
        public string Description { get; set; }
    }
}