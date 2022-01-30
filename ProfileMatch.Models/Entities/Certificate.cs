using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileMatch.Models.Entities
{
    public class Certificate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string DescriptionPl { get; set; }
        public string Image { get; set; }
        public string Url { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime ValidToDate { get; set; }

    }
}
