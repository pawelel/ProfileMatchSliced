namespace ProfileMatch.Models.Models
{
    public class UserNote
    {
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual Note Note { get; set; }
        public int NoteId { get; set; }

        public string Description { get; set; }
    }
}