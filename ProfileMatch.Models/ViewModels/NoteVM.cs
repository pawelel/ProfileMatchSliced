using System.Collections.Generic;

namespace ProfileMatch.Models.ViewModels
{
    class NoteVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<UserNoteVM> UserNotes { get; set; }
    }
}
