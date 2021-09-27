using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProfileMatch.Models.Models;

namespace ProfileMatch.Models.ViewModels
{
    class NoteVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<UserNoteVM> UserNotes { get; set; }
    }
}
