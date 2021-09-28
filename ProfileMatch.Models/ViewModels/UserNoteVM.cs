
using ProfileMatch.Models.Models;

namespace ProfileMatch.Models.ViewModels
{
    public class UserNoteVM
    {
        public string ApplicationUserId { get; set; }
        public ApplicationUserVM ApplicationUser { get; set; }
        public Note Note { get; set; }
        public int NoteId { get; set; }

        public string Description { get; set; }
    }
}
