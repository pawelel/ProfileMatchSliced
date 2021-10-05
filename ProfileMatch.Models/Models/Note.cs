using System.Collections.Generic;

namespace ProfileMatch.Models.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public  List<UserNote> UserNotes { get; set; }
    }
}