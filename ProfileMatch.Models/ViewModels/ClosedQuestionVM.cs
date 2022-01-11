using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileMatch.Models.ViewModels
{
    public class ClosedQuestionVM
    {
        public int ClosedQuestionId { get; set; }
        public string QuestionName { get; set; }
        public string QuestionNamePl { get; set; }
        public string Description { get; set; }
        public string DescriptionPl { get; set; }
        public string CategoryName { get; set; }
        public string CategoryNamePl { get; set; }
        public int CategoryId { get; set; }
        public bool IsActive { get; set; }
    }
}
