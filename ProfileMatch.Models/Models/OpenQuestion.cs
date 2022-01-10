using Ganss.Excel;

using System.Collections.Generic;

namespace ProfileMatch.Models.Models
{
    public class OpenQuestion
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NamePl { get; set; }
        public string Description { get; set; }
        public string DescriptionPl { get; set; }
        [Ignore]
        public List<UserOpenAnswer> UserOpenAnswers { get; set; }
    }
}