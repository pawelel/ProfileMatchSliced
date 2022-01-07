using Ganss.Excel;

using System.Collections.Generic;

namespace ProfileMatch.Models.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [Ignore]
        public List<ApplicationUser> ApplicationUsers { get; set; }
    }
}