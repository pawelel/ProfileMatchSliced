using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProfileMatch.Models.Models
{

    public class Note
    {
      
        public int Id { get; set; }
        public string Name { get; set; }
        public List<UserNote> UserNotes { get; set; }
    }
}