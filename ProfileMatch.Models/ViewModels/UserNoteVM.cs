using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileMatch.Models.ViewModels
{
    public class UserNoteVM
    {
        public string UserId { get; set; }
        public string NoteName { get; set; }
        public string NoteDescription { get; set; }
        public int NoteId { get; set; }
        public string UserDescription { get; set; }
        public bool IsDisplayed { get; set; }
    }
}
