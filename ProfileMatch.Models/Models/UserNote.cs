using Ganss.Excel;

namespace ProfileMatch.Models.Models
{
    public class UserNote
    {
        public string ApplicationUserId { get; set; }
        [Ignore]
        public ApplicationUser ApplicationUser { get; set; }
        [Ignore]
        public Note Note { get; set; }
        public int NoteId { get; set; }
        public bool IsDisplayed { get; set; }
        public string Description { get; set; }
    }
}