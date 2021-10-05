namespace ProfileMatch.Models.Models
{
    public class UserNote
    {
        public string ApplicationUserId { get; set; }
        public  ApplicationUser ApplicationUser { get; set; }
        public  Note Note { get; set; }
        public int NoteId { get; set; }

        public string Description { get; set; }
    }
}